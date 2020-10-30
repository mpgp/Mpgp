// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Mpgp.Shared
{
    public static class Utils
    {
        public static readonly JsonSerializerSettings JsonSettings = new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver { NamingStrategy = new CamelCaseNamingStrategy() },
            Formatting = Formatting.None,
            NullValueHandling = NullValueHandling.Ignore,
        };

        public static readonly Random Random = new Random();

        public static string HashString(string text) => string.Concat(
            System.Security.Cryptography.SHA256.Create()
                .ComputeHash(Encoding.Unicode.GetBytes($"s3cr3t++{text}::s@lt"))
                .Select(b => b.ToString("x2")));

        public static int GetAccountIdFromJwt(string jwtEncodedString)
        {
            var parts = jwtEncodedString.Split('.');
            if (parts.Length != 3)
            {
                throw new ArgumentException("Invalid JWT");
            }

            var token = new JwtSecurityToken(jwtEncodedString);

            return token.Claims.GetAccountId();
        }

        public static int GetAccountId(this IEnumerable<Claim> claims)
        {
            return int.Parse(claims.First(c => c.Type == "Id").Value);
        }
    }
}
