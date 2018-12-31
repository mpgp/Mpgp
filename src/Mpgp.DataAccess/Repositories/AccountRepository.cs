// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Mpgp.DataAccess.Core;
using Mpgp.Domain.Accounts.Entities;
using Mpgp.Domain.Accounts.Repositories;

namespace Mpgp.DataAccess.Repositories
{
    public class AccountRepository : EfRepository<Account, AppDbContext>, IAccountRepository
    {
        public AccountRepository(AppDbContext context)
            : base(context)
        {
        }

        public async Task<Account> GetById(int id) =>
            await Context.Accounts.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<Account> GetByLogin(string login) =>
            await Context.Accounts.FirstOrDefaultAsync(x => x.Login == login);

        public async Task<Account> GetByLoginAndPassword(string login, string password) =>
            await Context.Accounts.FirstOrDefaultAsync(x => x.Login == login && x.Password == password);
    }
}
