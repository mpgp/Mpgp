// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;

using Mpgp.Domain.Accounts.Dtos;

namespace Mpgp.WebSocketServer.Messages.Server
{
    public class AuthMessage : Abstract.IMessage
    {
        public ICollection<AccountDto> UsersList { get; set; }
    }
}
