// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

using System.ComponentModel.DataAnnotations;

using Mpgp.Abstract;
using Mpgp.Domain.Accounts.Entities;

namespace Mpgp.Domain.Accounts.Commands
{
    public class UpdatePasswordCommand : ICommand
    {
        public int AccountId { get; set; }

        [Required(ErrorMessage = Account.Errors.PasswordRequired)]
        [MinLength(8, ErrorMessage = Account.Errors.PasswordMinLength)]
        [MaxLength(249, ErrorMessage = Account.Errors.PasswordMaxLength)]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = Account.Errors.PasswordRequired)]
        [MinLength(8, ErrorMessage = Account.Errors.PasswordMinLength)]
        [MaxLength(249, ErrorMessage = Account.Errors.PasswordMaxLength)]
        public string Password { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        [System.ComponentModel.Description("Repeat the password.")]
        [Required(ErrorMessage = Account.Errors.PasswordRepeatRequired)]
        [Compare("Password", ErrorMessage = Account.Errors.PasswordRepeatMatch)]
        public string PasswordRepeat { get; set; }
    }
}