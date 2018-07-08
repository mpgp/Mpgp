// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

using System.ComponentModel.DataAnnotations;

using Mpgp.Abstract;
using Mpgp.Domain.Accounts.Entities;

namespace Mpgp.Domain.Accounts.Commands
{
    public class AuthorizeAccountCommand : ICommand
    {
        [Required(ErrorMessage = Account.Errors.LoginRequired)]
        [MinLength(3, ErrorMessage = Account.Errors.LoginMinLength)]
        [MaxLength(20, ErrorMessage = Account.Errors.LoginMaxLength)]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = Account.Errors.LoginRegex)]
        public string Login { get; set; }

        [Required(ErrorMessage = Account.Errors.PasswordRequired)]
        [MinLength(8, ErrorMessage = Account.Errors.PasswordMinLength)]
        [MaxLength(249, ErrorMessage = Account.Errors.PasswordMaxLength)]
        public string Password { get; set; }
    }
}