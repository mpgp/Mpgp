// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mpgp.Infrastructure;
using Mpgp.RestApiServer.WebSockets;
using Mpgp.WebSocketServer.Core;

namespace Mpgp.RestApiServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy("MyPolicy", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
            services.AddInfrastructure();
            services.AddWebSocketManager();
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressConsumesConstraintForFormFileParameters = false;
                options.SuppressInferBindingSourcesForParameters = false;
                options.SuppressModelStateInvalidFilter = true;
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, System.IServiceProvider serviceProvider)
        {
            app.UseCors("MyPolicy");
            app.UseDefaultFiles();
            app.UseStaticFiles();

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();

                // todo : removee
                app.UseMiddleware(typeof(Utils.ErrorHandlingMiddleware));
            }
            else
            {
                app.UseMiddleware(typeof(Utils.ErrorHandlingMiddleware));
            }

            var wsOptions = new WebSocketOptions()
            {
                KeepAliveInterval = System.TimeSpan.FromSeconds(60),
                ReceiveBufferSize = 4 * 1024
            };

            app.UseWebSockets(wsOptions);
            app.MapWebSocketManager(Configuration["Params:WebSocketPath"], serviceProvider.GetService<WebSocketRouter>());

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller}/{id?}");
            });
        }
    }
}
