// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

using System.Threading.Tasks;

using Mpgp.Domain.Accounts.Commands;
using Mpgp.Domain.Accounts.Entities;
using Mpgp.Shared.Exceptions;

namespace Mpgp.Domain.Accounts.Handlers
{
    public class ValidateTokenCommandHandler
        : CommandHandlerBase<ValidateTokenCommand>
    {
        public ValidateTokenCommandHandler(IAppUnitOfWork uow)
            : base(uow)
        {
        }

        public override async Task<int> Execute(ValidateTokenCommand command)
        {
            var account = AutoMapper.Mapper.Map<ValidateTokenCommand, Account>(command);
            var foundAccount = await Uow.AccountRepository.GetByAuthToken(command.AuthToken)
                ?? throw new NotFoundException();

            foundAccount.LastOnline = System.DateTime.Now;
            return await Uow.SaveChangesAsync();
        }
    }
}