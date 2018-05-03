// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

using Mpgp.Domain.Accounts.Dtos;

namespace Mpgp.WebSocketServer.Messages.Server
{
    public class UserConnectionMessage : Abstract.IMessage
    {
        public AccountDto Account { get; set; }

        public string Status { get; set; }
    }
}