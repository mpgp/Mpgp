// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

using System.Threading.Tasks;

using Mpgp.Domain.Accounts.Entities;

namespace Mpgp.Domain.Accounts.Queries
{
    public class AccountByIdQuery : BaseQuery
    {
        public AccountByIdQuery(IAppUnitOfWork uow)
            : base(uow)
        {
        }

        public async Task<Account> Execute(int id) =>
            await Uow.AccountRepository.GetById(id)
            ?? throw new Shared.Exceptions.NotFoundException();
    }
}
