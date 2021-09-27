using AppointmentBuddy.Core.Common.Config;
using AppointmentBuddy.Core.Common.Http;
using CF = NUS_ISS_14_Appointment_Buddy.WEB.Config;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NUS_ISS_14_Appointment_Buddy.Interface;
using NUS_ISS_14_Appointment_Buddy.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCompression;
using System.Net;
using System.Net.Security;
using Microsoft.AspNetCore.CookiePolicy;
using StackExchange.Redis;

namespace NUS_ISS_14_Appointment_Buddy
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var redisConnectionString = Configuration["AB_REDIS_CONNECTION_STRING"];
            if (string.IsNullOrEmpty(redisConnectionString))
            {
                redisConnectionString = Configuration.GetConnectionString("ApptBddyREDIS");
            }
            if (!string.IsNullOrEmpty(redisConnectionString))
            {
                ConfigurationOptions option = new ConfigurationOptions
                {
                    AbortOnConnectFail = false,
                    EndPoints = { redisConnectionString }
                };
                services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(option));
            }

            string loginPath = Environment.GetEnvironmentVariable("GEMS2_LOGIN_PATH");
            string logoutPath = Environment.GetEnvironmentVariable("GEMS2_LOGOUT_PATH");
            var cookieSecurePolicy = String.IsNullOrEmpty(loginPath) ? CookieSecurePolicy.None : CookieSecurePolicy.Always;

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.Strict;
                options.Secure = cookieSecurePolicy;
            });

            services.AddResponseCompression(options =>
            {
                options.Providers.Add<BrotliCompressionProvider>();
                options.Providers.Add<GzipCompressionProvider>();
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                    .AddXmlSerializerFormatters()
                    .AddXmlDataContractSerializerFormatters();

            services.AddHealthChecks();

            services.AddRazorPages().AddRazorRuntimeCompilation();

            services.Configure<ServiceUrls>(Configuration.GetSection("ServiceUrls"));
            services.Configure<CF.AppSettings>(Configuration.GetSection("AppSettings"));

            services.AddAntiforgery(o =>
            {
                o.SuppressXFrameOptionsHeader = true;
            });

            var authBuilder = services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie(options =>
            {
                if (string.IsNullOrEmpty(loginPath) && string.IsNullOrEmpty(logoutPath))
                {
                    options.LoginPath = "/account/login";
                    options.LogoutPath = "/account/logout";
                }
                else
                {
                    options.Events.OnRedirectToLogin = (context) =>
                    {
                        if (!string.IsNullOrEmpty(loginPath))
                        {
                            context.RedirectUri = loginPath;

                            if (IsAjaxRequest(context.Request))
                            {
                                context.Response.Headers["Location"] = context.RedirectUri;
                                context.Response.StatusCode = 401;
                            }
                            else
                            {
                                context.Response.Redirect(context.RedirectUri);
                            }
                        }

                        return Task.CompletedTask;
                    };
                    options.Events.OnRedirectToLogout = (context) =>
                    {
                        if (!string.IsNullOrEmpty(logoutPath))
                        {
                            context.RedirectUri = logoutPath;

                            if (IsAjaxRequest(context.Request))
                            {
                                context.Response.Headers["Location"] = context.RedirectUri;
                                context.Response.StatusCode = 401;
                            }
                            else
                            {
                                context.Response.Redirect(context.RedirectUri);
                            }
                        }

                        return Task.CompletedTask;
                    };
                    options.LoginPath = "/account/login";
                    options.LogoutPath = "/account/logout";
                }
            });

            services.AddHttpClientServices(Configuration);

            services.AddDataProtection(opts => opts.ApplicationDiscriminator = Program.AppName)
            .SetApplicationName(Program.AppName)
            .DisableAutomaticKeyGeneration();
        }

        private static bool IsAjaxRequest(HttpRequest request)
        {
            if (!string.Equals(request.Query["X-Requested-With"], "XMLHttpRequest", StringComparison.Ordinal))
            {
                return string.Equals(request.Headers["X-Requested-With"], "XMLHttpRequest", StringComparison.Ordinal);
            }

            return true;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            ServicePointManager.ServerCertificateValidationCallback = (sender, cert, chain, errors) =>
            {
                if (env.IsDevelopment())
                {
                    return true;
                }

                return errors == SslPolicyErrors.None
                || errors == SslPolicyErrors.RemoteCertificateNotAvailable
                || errors == SslPolicyErrors.RemoteCertificateNameMismatch
                || errors == SslPolicyErrors.RemoteCertificateChainErrors;
            };

            var cookieSecurePolicy = CookieSecurePolicy.None;

            app.UseCookiePolicy(new CookiePolicyOptions()
            {
                MinimumSameSitePolicy = SameSiteMode.Strict,
                HttpOnly = HttpOnlyPolicy.Always,
                Secure = cookieSecurePolicy,
            });

            IdentityModelEventSource.ShowPII = true;

            if (env.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStatusCodePagesWithReExecute("/Home/Error/{0}");

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapHealthChecks("/health");
            });
        }
    }
    static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddHttpClientServices(this IServiceCollection services, IConfiguration configuration)
        {

            //add http client services
            services.AddHttpContextAccessor();
            services.AddTransient<StandardHeaderHandler>();
            services.AddHttpClient<IAppointmentService, AppointmentService>().AddHttpMessageHandler<StandardHeaderHandler>();
            services.AddHttpClient<IIdentityService, IdentityService>().AddHttpMessageHandler<StandardHeaderHandler>();

            return services;
        }
    }
}
