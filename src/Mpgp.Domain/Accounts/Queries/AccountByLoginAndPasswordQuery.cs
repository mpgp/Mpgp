// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

using System.Threading.Tasks;

using Mpgp.Domain.Accounts.Entities;

namespace Mpgp.Domain.Accounts.Queries
{
    public class AccountByLoginAndPasswordQuery : BaseQuery
    {
        public AccountByLoginAndPasswordQuery(IAppUnitOfWork uow)
            : base(uow)
        {
        }

        public async Task<Account> Execute(string login, string password) =>
            await Uow.AccountRepository.GetByLoginAndPassword(login, Shared.Utils.HashString(password))
            ?? throw new Shared.Exceptions.NotFoundException();
    }
}
