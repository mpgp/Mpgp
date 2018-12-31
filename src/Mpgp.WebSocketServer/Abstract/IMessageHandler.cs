// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

using System.Net.WebSockets;
using System.Threading.Tasks;

using Mpgp.WebSocketServer.Core;

namespace Mpgp.WebSocketServer.Abstract
{
    public interface IMessageHandler
    {
        Task CheckAuth();

        Task Handle();

        IMessageHandler Initialize(string json, WebSocket socket, ConnectionManager webSocketConnectionManager);

        Task Validate();
    }
}
