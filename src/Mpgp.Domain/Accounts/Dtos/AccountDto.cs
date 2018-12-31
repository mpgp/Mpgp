// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

using System;

namespace Mpgp.Domain.Accounts.Dtos
{
    public class AccountDto
    {
        public int Id { get; set; }

        public string Avatar { get; set; }

        public string Languages { get; set; }

        public DateTime LastOnline { get; set; } = DateTime.Now;

        public string Nickname { get; set; }

        public DateTime RegisterDate { get; set; } = DateTime.Now;

        public string StatusInfo { get; set; }
    }
}
