// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

using Mpgp.Domain.Accounts.Commands;

namespace Mpgp.WebSocketServer.Messages.Client
{
    public class AuthMessage : ValidateTokenCommand, Abstract.IMessage
    {
    }
}