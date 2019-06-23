// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Mpgp.Domain.Accounts.Dtos;
using Mpgp.Domain.Accounts.Entities;
using Mpgp.RestApiServer.Utils;

namespace Mpgp.RestApiServer.ApiServices
{
    public static class AccountApiService
    {
        public static AuthInfoDto CreateAuthInfo(Account account)
        {
            return new AuthInfoDto
            {
                AuthToken = BuildJwt(GetIdentity(account)),
                User = AutoMapper.Mapper.Map<Account, AccountDto>(account)
            };
        }

        private static string BuildJwt(ClaimsIdentity identity)
        {
            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                AuthOptions.ISSUER,
                AuthOptions.AUDIENCE,
                identity.Claims,
                now,
                now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        private static ClaimsIdentity GetIdentity(Account account)
        {
            var claims = new List<Claim>
            {
                new Claim("Id", account.Id.ToString()),
                new Claim(ClaimsIdentity.DefaultNameClaimType, account.Nickname),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, account.Role)
            };
            return new ClaimsIdentity(
                claims,
                "Token",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
        }
    }
}