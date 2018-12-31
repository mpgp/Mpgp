// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

namespace Mpgp.Shared.Exceptions
{
    public class ValidationException : DomainException
    {
        public ValidationException(string message)
            : base(message)
        {
        }

        public ValidationException(string message, string errorCode)
            : base(message)
        {
            ErrorCode = errorCode;
        }

        public string ErrorCode { get; }
    }
}
