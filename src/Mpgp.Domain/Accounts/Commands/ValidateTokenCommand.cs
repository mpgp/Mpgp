// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

using System.ComponentModel.DataAnnotations;

using Mpgp.Abstract;
using Mpgp.Domain.Accounts.Entities;

namespace Mpgp.Domain.Accounts.Commands
{
    public class ValidateTokenCommand : ICommand
    {
        [Required(ErrorMessage = Account.Errors.AuthTokenRequired)]
        [MaxLength(64)]
        public string AuthToken { get; set; }
    }
}