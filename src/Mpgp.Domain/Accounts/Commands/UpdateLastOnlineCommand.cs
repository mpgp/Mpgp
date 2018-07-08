// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

using Mpgp.Abstract;

namespace Mpgp.Domain.Accounts.Commands
{
    public class UpdateLastOnlineCommand : ICommand
    {
        public UpdateLastOnlineCommand(int accountId)
        {
            AccountId = accountId;
        }

        public int AccountId { get; set; }
    }
}