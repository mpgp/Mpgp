// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

using System.ComponentModel.DataAnnotations;

using Mpgp.Abstract;
using Mpgp.Domain.Accounts.Entities;

namespace Mpgp.Domain.Accounts.Commands
{
    public class UpdateAccountCommand : ICommand
    {
        public int AccountId { get; set; }

        [Required]
        [MaxLength(249)]
        public string Avatar { get; set; }

        [Required]
        [MaxLength(249, ErrorMessage = Account.Errors.LanguagesMaxLength)]
        public string Languages { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = Account.Errors.NicknameMinLength)]
        [MaxLength(20, ErrorMessage = Account.Errors.NicknameMaxLength)]
        [RegularExpression(@"^[^\s].{1,18}[^\s]$", ErrorMessage = Account.Errors.NicknameRegex)]
        public string Nickname { get; set; }

        [Required]
        [MaxLength(249, ErrorMessage = Account.Errors.StatusInfoMaxLength)]
        public string StatusInfo { get; set; }
    }
}