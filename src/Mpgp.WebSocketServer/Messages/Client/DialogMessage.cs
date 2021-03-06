﻿// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

using System.ComponentModel.DataAnnotations;

namespace Mpgp.WebSocketServer.Messages.Client
{
    public class DialogMessage : Abstract.IMessage
    {
        [Required]
        [MaxLength(249)]
        public string Message { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Receiver { get; set; }
    }
}
