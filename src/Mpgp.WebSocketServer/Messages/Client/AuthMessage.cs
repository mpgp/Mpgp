// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

using System.ComponentModel.DataAnnotations;

namespace Mpgp.WebSocketServer.Messages.Client
{
    public class AuthMessage : Abstract.IMessage
    {
        [Required]
        public string AuthToken { get; set; }
    }
}
