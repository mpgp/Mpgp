// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

using System;
using System.IO;

using Microsoft.AspNetCore.Hosting;
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

            var host = BuildWebHost(args);
            using (var scope = host.Services.CreateScope())
            {
                try
                {
                    // var context = scope.ServiceProvider.GetRequiredService<Data.Access.AppDbContext>();
                    // Data.Access.AppDbContextInitializer.Initialize(context);
                    host.Run();
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "An error occurred while seeding the database.");
                    throw;
                }
                finally
                {
                    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                    NLog.LogManager.Shutdown();
                }
            }
        }

        public static IWebHost BuildWebHost(string[] args) =>
            new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", true, true)
                        .AddJsonFile(Path.GetFullPath(Path.Combine(@"../../tools/appsettings.json")), true, true);

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
