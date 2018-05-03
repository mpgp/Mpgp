// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

using System.Linq;

using Mpgp.Abstract;
using Mpgp.Domain.Accounts.Entities;
using Mpgp.Domain.Accounts.Repositories;

namespace Mpgp.Domain
{
    public interface IAppUnitOfWork : IUnitOfWork
    {
        IQueryable<Account> Accounts { get; }

        IAccountRepository AccountRepository { get; }
    }
}