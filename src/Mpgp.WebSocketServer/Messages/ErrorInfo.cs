// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

namespace Mpgp.WebSocketServer.Messages
{
    public class ErrorInfo
    {
        public int ErrorCode { get; set; }

        public string Message { get; set; }
    }
}
