// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

using System.Threading.Tasks;
using Mpgp.Domain.Accounts.Commands;
using Mpgp.Shared.Exceptions;

namespace Mpgp.Domain.Accounts.Handlers
{
    public class UpdateAccountCommandHandler
        : CommandHandlerBase<UpdateAccountCommand>
    {
        public UpdateAccountCommandHandler(IAppUnitOfWork uow)
            : base(uow)
        {
        }

        public override async Task<int> Execute(UpdateAccountCommand command)
        {
            var foundAccount = await Uow.AccountRepository.GetById(command.AccountId)
                               ?? throw new NotFoundException();

            foundAccount.Avatar = command.Avatar;
            foundAccount.Languages = command.Languages;
            foundAccount.Nickname = command.Nickname;
            foundAccount.StatusInfo = command.StatusInfo;

            return await Uow.SaveChangesAsync();
        }
    }
}