// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

namespace Mpgp.Domain.Accounts.Dtos
{
    public class AuthInfoDto
    {
        public AuthInfoDto(AccountDto user, string authToken)
        {
            AuthToken = authToken;
            User = user;
        }

        public string AuthToken { get; set; }

        public AccountDto User { get; set; }
    }
}
