using AppointmentBuddy.Core.Common.Config;
using AppointmentBuddy.Core.Common.Helper;
using AppointmentBuddy.Core.Common.Http;
using AppointmentBuddy.Infrastructure.Repository;
using AppointmentBuddy.Service.Identity.API.Core.Interface;
using AppointmentBuddy.Service.Identity.API.Infrastructure.Repository;
using AppointmentBuddy.Service.Identity.API.Infrastructure.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentBuddy.Service.Identity.API
{
    public class Startup
    {
        private IWebHostEnvironment _webHostEnvironment;

        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration["APPTBUDDY_DB_CONNECTION_STRING"];
            if (string.IsNullOrEmpty(connectionString))
            {
                connectionString = Configuration.GetConnectionString("ApptBddyDB");
            }

            if (!string.IsNullOrEmpty(connectionString))
            {
                services.AddDbContext<AppointmentBuddyDBContext>(options =>
                {
                    options.UseSqlServer(connectionString);
                });
            }

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddHealthChecks();

            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            services.Configure<ServiceUrls>(Configuration.GetSection("ServiceUrls"));

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var fileUtility = new FileUtility();

            var s3KeyStoreBucketName = Configuration["AppointmentBuddy_S3_KeyStoreBucketName"];
            if (!String.IsNullOrEmpty(s3KeyStoreBucketName))
            {
                appSettings.S3KeyStoreBucketName = s3KeyStoreBucketName;
            }

            var s3KeyStoreFilePath = Configuration["AppointmentBuddy_S3_KeyStoreFilePath"];
            if (!String.IsNullOrEmpty(s3KeyStoreBucketName))
            {
                appSettings.S3KeyStoreFilePath = s3KeyStoreFilePath;
            }

            var publicKeyName = Configuration["AppointmentBuddy_S3_PublicKeyName"];
            if (!String.IsNullOrEmpty(s3KeyStoreBucketName))
            {
                appSettings.PublicKeyName = publicKeyName;
            }

            var streamResult = Task.Run<Stream>(async () => await fileUtility.DownloadFileFromS3AsStream("", "", appSettings.S3KeyStoreBucketName, appSettings.S3KeyStoreFilePath, appSettings.PublicKeyName)).Result;

            string line = string.Empty;
            StringBuilder sb = new StringBuilder();
            using (StreamReader sr = new StreamReader(streamResult))
            {
                int intC;
                while ((intC = sr.Read()) != -1)
                {
                    char c = (char)intC;
                    if (c == '\n')
                    {
                        sb.Append(Environment.NewLine);
                    }
                    if (sb.Length >= 2000)
                    {
                        throw new Exception("input too long");
                    }
                    sb.Append(c);
                }

                line = sb.ToString();
            }

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                using (TextReader publicKeyTextReader = new StringReader(line))
                {
                    RsaKeyParameters publicKeyParam = (RsaKeyParameters)new PemReader(publicKeyTextReader).ReadObject();

                    using (RSA cryptoServiceProvider = RSA.Create())
                    {
                        RSAParameters parms = new RSAParameters();

                        parms.Modulus = publicKeyParam.Modulus.ToByteArrayUnsigned();
                        parms.Exponent = publicKeyParam.Exponent.ToByteArrayUnsigned();

                        cryptoServiceProvider.ImportParameters(parms);

                        RsaSecurityKey signingKey = new RsaSecurityKey(cryptoServiceProvider.ExportParameters(false));

                        x.RequireHttpsMetadata = false;
                        x.SaveToken = true;
                        x.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = signingKey,
                            ValidateIssuer = false,
                            ValidateAudience = false
                        };
                    }
                }
            });

            services.AddApiVersioning(o =>
            {
                o.ReportApiVersions = true;
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
            });

            var redisConnectionString = Configuration["APPTBDDY_REDIS_CONNECTION_STRING"];
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

            services.AddHttpContextAccessor();
            services.AddTransient<StandardHeaderHandler>();
            services.AddHttpClient<IIdentityService, IdentityService>()
                .AddHttpMessageHandler<StandardHeaderHandler>();
            services.AddTransient<IIdentityRepositoryService, IdentityRepositoryService>();
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
                //app.ConfigureCustomExceptionMiddleware();
            }
            app.UseStatusCodePages();

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });
        }
    }
}
