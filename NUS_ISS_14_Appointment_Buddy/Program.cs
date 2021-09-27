using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NUS_ISS_14_Appointment_Buddy
{
    public class Program
    {
        public static readonly string Namespace = typeof(Program).Namespace;
        public static readonly string AppName = Namespace.Split('.')[0];

        public static int Main(string[] args)
        {
            var configuration = GetConfiguration();

            try
            {
                var host = CreateWebHostBuilder(configuration, args).Build();
                host.Run();

                return 0;
            }
            catch (Exception ex)
            {
                return 1;
            }
            finally
            {
            }
        }

        public static IHostBuilder CreateWebHostBuilder(IConfiguration configuration, string[] args) =>
           Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(builder => builder.AddConfiguration(configuration))
                .ConfigureAppConfiguration((context, config) =>
                {
                    config.AddEnvironmentVariables(prefix: "AB_");
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            return builder.Build();
        }
    }
}
