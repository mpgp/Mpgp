// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mpgp.Domain.Accounts.Entities
{
    public class Account
    {
        [Key]
        public int AccountId { get; set; }

        [MaxLength(249)]
        public string Avatar { get; set; }

        [MaxLength(64)]
        public string AuthToken { get; set; }

        [MaxLength(249, ErrorMessage = Errors.LanguagesMaxLength)]
        public string Languages { get; set; }

        [DataType(DataType.Date)]
        public DateTime LastOnline { get; set; } = DateTime.Now;

        [Required(ErrorMessage = Errors.LoginRequired)]
        [MinLength(3, ErrorMessage = Errors.LoginMinLength)]
        [MaxLength(20, ErrorMessage = Errors.LoginMaxLength)]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = Errors.LoginRegex)]
        public string Login { get; set; }

        [Required(ErrorMessage = Errors.NicknameRequired)]
        [MinLength(3, ErrorMessage = Account.Errors.NicknameMinLength)]
        [MaxLength(20, ErrorMessage = Account.Errors.NicknameMaxLength)]
        [RegularExpression(@"^[^\s].{1,18}[^\s]$", ErrorMessage = Account.Errors.NicknameRegex)]
        public string Nickname { get; set; }

        [Required(ErrorMessage = Errors.PasswordRequired)]
        [MinLength(8, ErrorMessage = Errors.PasswordMinLength)]
        [MaxLength(249, ErrorMessage = Errors.PasswordMaxLength)]
        public string Password { get; set; }

        [Required(ErrorMessage = Errors.PasswordRepeatRequired)]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        [Compare("Password", ErrorMessage = Errors.PasswordRepeatMatch)]
        [MaxLength(249)]
        public string PasswordRepeat { get; set; }

        [DataType(DataType.Date)]
        public DateTime RegisterDate { get; set; } = DateTime.Now;

        [MaxLength(249, ErrorMessage = Errors.StatusInfoMaxLength)]
        public string StatusInfo { get; set; }

        public class Errors : Abstract.IErrorList
        {
            public const string LoginRequired = "1";
            public const string LoginMinLength = "2";
            public const string LoginMaxLength = "3";
            public const string LoginRegex = "4";
            public const string NicknameRequired = "5";
            public const string NicknameMinLength = "6";
            public const string NicknameMaxLength = "7";
            public const string NicknameRegex = "8";
            public const string PasswordRequired = "9";
            public const string PasswordMinLength = "10";
            public const string PasswordMaxLength = "11";
            public const string PasswordRepeatRequired = "12";
            public const string PasswordRepeatMatch = "13";
            public const string AuthTokenRequired = "14";
            public const string StatusInfoMaxLength = "15";
            public const string LanguagesMaxLength = "16";

            public Dictionary<string, string> Messages { get; } = new Dictionary<string, string>
            {
                [LoginRequired] = "Login is required",
                [LoginMinLength] = "Login must be a minimum of 3 characters",
                [LoginMaxLength] = "Login must be a maximum of 20 characters",
                [LoginRegex] = "Login can consist only of letters and numbers",
                [NicknameRequired] = "Nickname is required",
                [NicknameMinLength] = "Nickname must be a minimum of 3 characters",
                [NicknameMaxLength] = "Nickname must be a maximum of 20 characters",
                [NicknameRegex] = "Nickname can consist only of letters and numbers",
                [PasswordRequired] = "Password is required",
                [PasswordMinLength] = "Password must be a minimum of 8 characters",
                [PasswordMaxLength] = "Password is too long",
                [PasswordRepeatRequired] = "PasswordRepeat is required",
                [PasswordRepeatMatch] = "PasswordRepeat must match to a Password",
                [AuthTokenRequired] = "AuthToken is required",
                [StatusInfoMaxLength] = "StatusInfo must be a maximum of 249 characters",
                [LanguagesMaxLength] = "Languages must be a maximum of 249 characters"
            };
        }
    }
}
