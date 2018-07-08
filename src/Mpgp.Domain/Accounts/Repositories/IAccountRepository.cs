// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

using System.Threading.Tasks;
using Mpgp.Domain.Accounts.Entities;

namespace Mpgp.Domain.Accounts.Repositories
{
    public interface IAccountRepository : IRepository<Account>
    {
        Task<Account> GetById(int accountId);

        Task<Account> GetByLogin(string login);

        Task<Account> GetByLoginAndPassword(string login, string password);
    }
}
