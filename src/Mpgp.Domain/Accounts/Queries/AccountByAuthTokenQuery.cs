// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

using System.Threading.Tasks;

using Mpgp.Domain.Accounts.Entities;

namespace Mpgp.Domain.Accounts.Queries
{
    public class AccountByAuthTokenQuery : BaseQuery
    {
        public AccountByAuthTokenQuery(IAppUnitOfWork uow)
            : base(uow)
        {
        }

        public async Task<Account> Execute(string authToken) =>
            await Uow.AccountRepository.GetByAuthToken(authToken)
            ?? throw new Shared.Exceptions.NotFoundException();
    }
}
