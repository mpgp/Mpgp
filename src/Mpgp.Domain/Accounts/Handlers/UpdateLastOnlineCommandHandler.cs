// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

using System.Threading.Tasks;
using Mpgp.Domain.Accounts.Commands;
using Mpgp.Shared.Exceptions;

namespace Mpgp.Domain.Accounts.Handlers
{
    public class UpdateLastOnlineCommandHandler
        : CommandHandlerBase<UpdateLastOnlineCommand>
    {
        public UpdateLastOnlineCommandHandler(IAppUnitOfWork uow)
            : base(uow)
        {
        }

        public override async Task<int> Execute(UpdateLastOnlineCommand command)
        {
            var foundAccount = await Uow.AccountRepository.GetById(command.Id)
                               ?? throw new NotFoundException();

            foundAccount.LastOnline = System.DateTime.Now;
            return await Uow.SaveChanges();
        }
    }
}
