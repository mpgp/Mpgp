// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

using System;
using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

namespace Mpgp.WebSocketServer.Core
{
    public class DiConfig
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<HandlerFactory>();
            services.AddSingleton<ConnectionManager>();
            services.AddSingleton<WebSocketRouter>();

            // Register all handlers.
            var assembly = Assembly.GetAssembly(Type.GetType("Mpgp.WebSocketServer.Core.DiConfig"));
            foreach (var type in assembly.GetTypes())
            {
                if (type.FullName.StartsWith("Mpgp.WebSocketServer.Handlers") && type.FullName.EndsWith("Handler"))
                {
                    services.AddTransient(Type.GetType(type.FullName));
                }
            }
        }
    }
}