using AppointmentBuddy.Core.Common.Config;
using AppointmentBuddy.Core.Common.Http;
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                    .AddXmlSerializerFormatters()
                    .AddXmlDataContractSerializerFormatters();

            services.AddHealthChecks();

            services.Configure<ServiceUrls>(Configuration.GetSection("ServiceUrls"));

            services.AddAntiforgery(o =>
            {
                o.SuppressXFrameOptionsHeader = true;
            });

            var authBuilder = services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            });

            services.AddHttpClientServices(Configuration);

            services.AddDataProtection(opts => opts.ApplicationDiscriminator = Program.AppName)
            .SetApplicationName(Program.AppName)
            .DisableAutomaticKeyGeneration();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

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

            return services;
        }
    }
}
