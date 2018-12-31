// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

using System;

namespace Mpgp.Shared.Exceptions
{
    public class ClientException : Exception
    {
        public ClientException()
            : base("Client exception")
        {
        }
    }
}
