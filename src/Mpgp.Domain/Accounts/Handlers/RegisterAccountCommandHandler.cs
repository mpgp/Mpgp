// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

using System.Threading.Tasks;

using Mpgp.Domain.Accounts.Commands;
using Mpgp.Domain.Accounts.Entities;
using Mpgp.Shared.Exceptions;

namespace Mpgp.Domain.Accounts.Handlers
{
    public class RegisterAccountCommandHandler
        : CommandHandlerBase<RegisterAccountCommand>
    {
        public RegisterAccountCommandHandler(IAppUnitOfWork uow)
            : base(uow)
        {
        }

        public override async Task<int> Execute(RegisterAccountCommand command)
        {
            var foundAccount = await Uow.AccountRepository.GetByLogin(command.Login);
            if (foundAccount != null)
            {
                throw new ConflictException();
            }

            var account = AutoMapper.Mapper.Map<RegisterAccountCommand, Account>(command);
            account.Password = Shared.Utils.HashString(command.Password);
            account.Avatar = command.Avatar ?? Shared.Utils.Random.Next(0, 99) + ".jpg";
            account.AuthToken = command.AuthToken = Shared.Utils.HashString(command.Login + account.Password + account.RegisterDate);
            account.Nickname = command.Nickname = command.Nickname ?? command.Login;

            await Uow.AccountRepository.AddAsync(account);
            return await Uow.SaveChangesAsync();
        }
    }
}