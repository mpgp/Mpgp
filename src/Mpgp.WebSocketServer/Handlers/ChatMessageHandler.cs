// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

using System.Threading.Tasks;

using Microsoft.Extensions.Logging;
using Mpgp.WebSocketServer.Core;

namespace Mpgp.WebSocketServer.Handlers
{
    public class ChatMessageHandler : MessageHandler<Messages.Client.ChatMessage>
    {
        private readonly ILogger<ChatMessageHandler> logger;

        public ChatMessageHandler(ILogger<ChatMessageHandler> logger)
        {
            this.logger = logger;
        }

        public override async Task Handle()
        {
            logger.LogDebug("Send ChatMessage to all: {@json}", Context.Message);
            var response = new Messages.Server.ChatMessage()
            {
                Message = Context.Message.Payload.Message,
                Sender = Context.Sender.Id
            };
            await Context.ConnectionManager.SendMessageToAllAsync(response);
        }
    }
}
