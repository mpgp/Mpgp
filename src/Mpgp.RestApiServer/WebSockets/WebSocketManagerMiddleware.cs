// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Mpgp.WebSocketServer.Core;

namespace Mpgp.RestApiServer.WebSockets
{
    public class WebSocketManagerMiddleware
    {
        private readonly ILogger<WebSocketManagerMiddleware> logger;

        private readonly RequestDelegate next;

        private readonly WebSocketRouter webSocketRouter;

        public WebSocketManagerMiddleware(
            RequestDelegate next,
            WebSocketRouter webSocketRouter,
            ILogger<WebSocketManagerMiddleware> logger)
        {
            this.logger = logger;
            this.next = next;
            this.webSocketRouter = webSocketRouter;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.WebSockets.IsWebSocketRequest)
            {
                return;
            }

            var socket = await context.WebSockets.AcceptWebSocketAsync();

            await Receive(socket, async (result, buffer) =>
            {
                switch (result.MessageType)
                {
                    case WebSocketMessageType.Text:
                        await webSocketRouter.ReceiveAsync(socket, result, buffer);
                        break;
                    case WebSocketMessageType.Close:
                        await webSocketRouter.OnDisconnected(socket);
                        break;
                    case WebSocketMessageType.Binary: break;
                    default: return;
                }
            });

            // TODO: investigate the Kestrel exception thrown when this is the last middleware
            // TODO: remove condition
            if (socket == null)
            {
                await next.Invoke(context);
            }
        }

        private async Task Receive(WebSocket socket, Action<WebSocketReceiveResult, byte[]> handleMessage)
        {
            try
            {
                var buffer = new byte[4096];
                while (socket.State == WebSocketState.Open)
                {
                    var result = await socket.ReceiveAsync(
                        buffer: new ArraySegment<byte>(buffer),
                        cancellationToken: CancellationToken.None);

                    handleMessage(result, buffer);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while receiving the message.");
            }
        }
    }
}
