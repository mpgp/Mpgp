// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Mpgp.WebSocketServer.Abstract;
using Mpgp.WebSocketServer.Handlers;

namespace Mpgp.WebSocketServer.Core
{
    public class WebSocketRouter
    {
        private readonly ConnectionManager connectionManager;
        private readonly ILogger<WebSocketRouter> logger;
        private readonly HandlerFactory handlerFactory;
        private readonly IServiceProvider serviceProvider;

        public WebSocketRouter(
            ConnectionManager connectionManager,
            ILogger<WebSocketRouter> logger,
            HandlerFactory handlerFactory,
            IServiceProvider serviceProvider)
        {
            this.connectionManager = connectionManager;
            this.logger = logger;
            this.handlerFactory = handlerFactory;
            this.serviceProvider = serviceProvider;
        }

        public virtual async Task OnDisconnected(WebSocket socket) =>
            await connectionManager.RemoveSocketAsync(socket);

        public async Task ReceiveAsync(WebSocket socket, WebSocketReceiveResult result, byte[] buffer)
        {
            var json = string.Empty;
            IMessageHandler handler;
            try
            {
                json = Encoding.UTF8.GetString(buffer, 0, result.Count);
                logger.LogDebug("Receive message: '{@json}'", json);

                var messageName = Newtonsoft.Json.JsonConvert
                    .DeserializeObject<Messages.ClientMessage<Message>>(json)
                    .Type
                    .Replace("_", string.Empty);

                handler = handlerFactory.CreateHandler(messageName)
                    .Initialize(json, socket, connectionManager);
                await handler.Validate();
                await handler.CheckAuth();
                await handler.Handle();
            }
            catch (Shared.Exceptions.ValidationException ex)
            {
                logger.LogError(ex, "An error occurred while validating message: '{@json}'", json);
            }
            catch (Shared.Exceptions.UnauthorizedException ex)
            {
                logger.LogError(ex, "An error occurred while authorizing; message: '{@json}'", json);
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "An error occurred while handling message: '{@json}'", json);
                handler = serviceProvider.GetService<BadRequestHandler>();
                handler.Initialize(null, socket, connectionManager);
                await handler.Handle();
            }
        }
    }
}
