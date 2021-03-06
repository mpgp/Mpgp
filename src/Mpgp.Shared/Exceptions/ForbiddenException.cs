﻿// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

namespace Mpgp.Shared.Exceptions
{
    public class ForbiddenException : DomainException
    {
        public ForbiddenException()
            : base("Forbidden")
        {
        }
    }
}
