// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

using System.Linq;

namespace Mpgp.Shared
{
    public static class Utils
    {
        public static readonly System.Random Random = new System.Random();

        public static string HashString(string text) => string.Concat(
            System.Security.Cryptography.SHA256.Create()
                .ComputeHash(System.Text.Encoding.Unicode.GetBytes($"s3cr3t++{text}::s@lt"))
                .Select(b => b.ToString("x2")));
    }
}
