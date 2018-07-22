// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mpgp.Abstract;
using Mpgp.Domain.Accounts.Commands;
using Mpgp.Domain.Accounts.Entities;
using Mpgp.RestApiServer.Utils;
using Mpgp.Shared;

namespace Mpgp.RestApiServer.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ManageController : ControllerBase
    {
        private readonly ICommandFactory commandFactory;
        private readonly IQueryFactory queryFactory;

        public ManageController(ICommandFactory commandFactory, IQueryFactory queryFactory)
        {
            this.commandFactory = commandFactory;
            this.queryFactory = queryFactory;
        }

        [Authorize]
        [HttpPatch]
        public async Task UpdateAccount(
            UpdateAccountCommand command,
            CancellationToken token = default(CancellationToken))
        {
            ModelState.ThrowValidationExceptionIfInvalid<Account.Errors>();

            command.AccountId = User.Claims.GetAccountId();
            await commandFactory.Execute(command);
        }

        [Authorize]
        [HttpPatch("password")]
        public async Task UpdatePassword(
            UpdatePasswordCommand command,
            CancellationToken token = default(CancellationToken))
        {
            ModelState.ThrowValidationExceptionIfInvalid<Account.Errors>();

            command.AccountId = User.Claims.GetAccountId();
            await commandFactory.Execute(command);
        }
    }
}