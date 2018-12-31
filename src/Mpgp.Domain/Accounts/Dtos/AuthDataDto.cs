// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

namespace Mpgp.Domain.Accounts.Dtos
{
    public class AuthDataDto
    {
        public AuthDataDto(string authToken)
        {
            AuthToken = authToken;
        }

        public string AuthToken { get; set; }
    }
}
