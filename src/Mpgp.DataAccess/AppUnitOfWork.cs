// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

using System.Linq;

using Mpgp.DataAccess.Core;
using Mpgp.DataAccess.Repositories;
using Mpgp.Domain;
using Mpgp.Domain.Accounts.Entities;
using Mpgp.Domain.Accounts.Repositories;

namespace Mpgp.DataAccess
{
    public class AppUnitOfWork : EfUnitOfWork<AppDbContext>, IAppUnitOfWork
    {
        private IAccountRepository accountRepository;

        public AppUnitOfWork(AppDbContext context)
            : base(context)
        {
        }

        public IQueryable<Account> Accounts => Context.Accounts;

        public IAccountRepository AccountRepository
        {
            get
            {
                if (accountRepository == null)
                {
                    accountRepository = new AccountRepository(Context);
                }

                return accountRepository;
            }
        }
    }
}
