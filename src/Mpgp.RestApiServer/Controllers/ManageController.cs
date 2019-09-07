// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mpgp.Abstract;
using Mpgp.Domain.Accounts.Commands;
using Mpgp.Domain.Accounts.Entities;
using Mpgp.Infrastructure.Filters;
using Mpgp.Shared;

namespace Mpgp.RestApiServer.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ServiceFilter(typeof(ValidationFilterAttribute<Account.Errors>))]
    public class ManageController : ControllerBase
    {
        private readonly ICommandFactory commandFactory;

        public ManageController(ICommandFactory commandFactory)
        {
            this.commandFactory = commandFactory;
        }

        [Authorize]
        [HttpPatch]
        public async Task UpdateAccount(UpdateAccountCommand command)
        {
            command.Id = User.Claims.GetAccountId();
            await commandFactory.Execute(command);
        }

        [Authorize]
        [HttpPatch("password")]
        public async Task UpdatePassword(UpdatePasswordCommand command)
        {
            command.Id = User.Claims.GetAccountId();
            await commandFactory.Execute(command);
        }
    }
}
