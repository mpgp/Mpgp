// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

using System.Threading.Tasks;
using Mpgp.Domain.Accounts.Commands;
using Mpgp.Shared.Exceptions;

namespace Mpgp.Domain.Accounts.Handlers
{
    public class UpdatePasswordCommandHandler
        : CommandHandlerBase<UpdatePasswordCommand>
    {
        public UpdatePasswordCommandHandler(IAppUnitOfWork uow)
            : base(uow)
        {
        }

        public override async Task<int> Execute(UpdatePasswordCommand command)
        {
            var foundAccount = await Uow.AccountRepository.GetById(command.AccountId)
                               ?? throw new NotFoundException();

            foundAccount.Password = Shared.Utils.HashString(command.Password);
            return await Uow.SaveChangesAsync();
        }
    }
}