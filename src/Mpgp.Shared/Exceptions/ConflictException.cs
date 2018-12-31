// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

namespace Mpgp.Shared.Exceptions
{
    public class ConflictException : DomainException
    {
        public ConflictException()
            : base("Conflict")
        {
        }

        public ConflictException(string message)
            : base(message)
        {
        }
    }
}
