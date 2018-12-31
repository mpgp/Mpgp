// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

using System.Net.WebSockets;

using Mpgp.Domain.Accounts.Dtos;
using Mpgp.WebSocketServer.Abstract;
using Mpgp.WebSocketServer.Messages;

namespace Mpgp.WebSocketServer.Core
{
    public class HandlerContext<T>
        where T : IMessage
    {
        public HandlerContext(ClientMessage<T> message, WebSocket socket, ConnectionManager connectionManager)
        {
            Message = message;
            Socket = socket;
            ConnectionManager = connectionManager;
        }

        public ClientMessage<T> Message { get;  }

        public WebSocket Socket { get; }

        public AccountDto Sender => ConnectionManager.GetAccount(Socket);

        public ConnectionManager ConnectionManager { get; }
    }
}
