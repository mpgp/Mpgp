// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

using System.Threading.Tasks;

using Mpgp.Domain.Accounts.Commands;
using Mpgp.Domain.Accounts.Entities;
using Mpgp.Shared.Exceptions;

namespace Mpgp.Domain.Accounts.Handlers
{
    public class AuthorizeAccountCommandHandler
        : CommandHandlerBase<AuthorizeAccountCommand>
    {
        public AuthorizeAccountCommandHandler(IAppUnitOfWork uow)
            : base(uow)
        {
        }

        public override async Task<int> Execute(AuthorizeAccountCommand command)
        {
            var account = AutoMapper.Mapper.Map<AuthorizeAccountCommand, Account>(command);
            var foundAccount = await Uow.AccountRepository
                .GetByLoginAndPassword(command.Login, Shared.Utils.HashString(command.Password))
                ?? throw new NotFoundException();

            command.AuthToken = foundAccount.AuthToken;
            foundAccount.LastOnline = System.DateTime.Now;
            return await Uow.SaveChangesAsync();
        }
    }
}