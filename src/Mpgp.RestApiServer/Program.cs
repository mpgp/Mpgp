// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

using System;
using System.IO;

using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace Mpgp.RestApiServer
{
    public static class Program
    {
        static Program()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile(Path.GetFullPath(Path.Combine(@"../../tools/appsettings.json")), true, true);

            Configuration = builder.Build();
        }

        public static IConfiguration Configuration { get; }

        public static void Main(string[] args)
        {
            var nlogConfigPath = Path.GetFullPath(Path.Combine(@"../../tools/nlog.config"));
            var logger = NLogBuilder
                .ConfigureNLog(File.Exists(nlogConfigPath) ? nlogConfigPath : "nlog.config")
                .GetCurrentClassLogger();
            logger.Debug("Init main...");

            try
            {
                var host = BuildWebHost(args);
                using (var scope = host.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    var context = services.GetRequiredService<DataAccess.AppDbContext>();
                    context.Database.Migrate();
                }

                host.Run();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "An error occurred while starting the host.");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                NLog.LogManager.Shutdown();
            }
        }

        private static IWebHost BuildWebHost(string[] args) =>
            new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var env = hostingContext.HostingEnvironment.EnvironmentName;
                    config.SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", true, true)
                        .AddJsonFile(Path.GetFullPath(Path.Combine("../../tools/appsettings.json")), true, true)
                        .AddJsonFile(Path.GetFullPath(Path.Combine($"../../tools/appsettings.{env}.json")), true, true)
                        .AddJsonFile($"appsettings.{env}.json", true, true);

                    config.AddEnvironmentVariables();

                    if (args != null)
                    {
                        config.AddCommandLine(args);
                    }
                })
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(LogLevel.Trace);
                })
                .UseNLog()
                .UseIISIntegration()
                .UseStartup<Startup>()
                .UseUrls(Configuration["Params:RestApiUrl"])
                .Build();
    }
}
