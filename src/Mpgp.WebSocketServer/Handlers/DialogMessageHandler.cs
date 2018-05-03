// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

using System.Threading.Tasks;

using Microsoft.Extensions.Logging;
using Mpgp.WebSocketServer.Core;
using Mpgp.WebSocketServer.Messages;

namespace Mpgp.WebSocketServer.Handlers
{
    public class DialogMessageHandler : MessageHandler<Messages.Client.DialogMessage>
    {
        private readonly ILogger<DialogMessageHandler> logger;

        public DialogMessageHandler(ILogger<DialogMessageHandler> logger)
        {
            this.logger = logger;
        }

        public override async Task Handle()
        {
            var receiver = Context.ConnectionManager.FindSocket(Context.Message.Payload.Receiver);
            var response = new Messages.Server.DialogMessage();
            if (receiver == null)
            {
                logger.LogDebug("Can't send DialogMessage to non-exists receiver #{@Receiver}: {@json}", Context.Message.Payload.Receiver, Context.Message.Payload);
                var error = new ErrorInfo()
                {
                    ErrorCode = 404,
                    Message = $"Receiver #{Context.Message.Payload.Receiver} not found."
                };
                await Context.ConnectionManager.SendMessageAsync(Context.Socket, response, error);
                return;
            }

            logger.LogDebug("Send DialogMessage to {@Receiver}: {@json}", Context.Message.Payload.Receiver, Context.Message.Payload);
            response.Message = Context.Message.Payload.Message;
            response.Sender = Context.Sender.AccountId;
            await Context.ConnectionManager.SendMessageAsync(receiver, response);
        }
    }
}
