// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

using System.ComponentModel.DataAnnotations;

using Mpgp.Abstract;
using Mpgp.Domain.Accounts.Entities;

namespace Mpgp.Domain.Accounts.Commands
{
    public class RegisterAccountCommand : ICommand
    {
        [MaxLength(249)]
        public string Avatar { get; set; }

        [MaxLength(249, ErrorMessage = Account.Errors.LanguagesMaxLength)]
        public string Languages { get; set; }

        [Required(ErrorMessage = Account.Errors.LoginRequired)]
        [MinLength(3, ErrorMessage = Account.Errors.LoginMinLength)]
        [MaxLength(20, ErrorMessage = Account.Errors.LoginMaxLength)]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = Account.Errors.LoginRegex)]
        public string Login { get; set; }

        [MinLength(3, ErrorMessage = Account.Errors.NicknameMinLength)]
        [MaxLength(20, ErrorMessage = Account.Errors.NicknameMaxLength)]
        [RegularExpression(@"^[^\s].{1,18}[^\s]$", ErrorMessage = Account.Errors.NicknameRegex)]
        public string Nickname { get; set; }

        [Required(ErrorMessage = Account.Errors.PasswordRequired)]
        [MinLength(8, ErrorMessage = Account.Errors.PasswordMinLength)]
        [MaxLength(249, ErrorMessage = Account.Errors.PasswordMaxLength)]
        public string Password { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        [System.ComponentModel.Description("Repeat the password.")]
        [Required(ErrorMessage = Account.Errors.PasswordRepeatRequired)]
        [Compare("Password", ErrorMessage = Account.Errors.PasswordRepeatMatch)]
        public string PasswordRepeat { get; set; }

        [MaxLength(249, ErrorMessage = Account.Errors.StatusInfoMaxLength)]
        public string StatusInfo { get; set; }
    }
}
