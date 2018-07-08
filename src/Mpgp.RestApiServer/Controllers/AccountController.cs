// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mpgp.Abstract;
using Mpgp.Domain.Accounts.Commands;
using Mpgp.Domain.Accounts.Dtos;
using Mpgp.Domain.Accounts.Entities;
using Mpgp.Domain.Accounts.Queries;
using Mpgp.RestApiServer.Utils;

namespace Mpgp.RestApiServer.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ICommandFactory commandFactory;
        private readonly IQueryFactory queryFactory;

        public AccountController(ICommandFactory commandFactory, IQueryFactory queryFactory)
        {
            this.commandFactory = commandFactory;
            this.queryFactory = queryFactory;
        }

        [HttpPost]
        public async Task<IActionResult> Authorize(AuthorizeAccountCommand account, CancellationToken token = default(CancellationToken))
        {
            ModelState.ThrowValidationExceptionIfInvalid<Account.Errors>();

            await commandFactory.Execute(account);
            var userAccount = await queryFactory.ResolveQuery<AccountByAuthTokenQuery>().Execute(account.AuthToken);
            var userInfo = AutoMapper.Mapper.Map<Account, AccountDto>(userAccount);
            return StatusCode(200, new AuthInfoDto(userInfo, account.AuthToken));
        }

        [HttpGet("{accountId}")]
        public async Task<IActionResult> GetInfo(int accountId, CancellationToken token = default(CancellationToken))
        {
            var response = await queryFactory.ResolveQuery<AccountByIdQuery>().Execute(accountId);
            return Ok(AutoMapper.Mapper.Map<Account, AccountDto>(response));
        }

        [HttpPut]
        public async Task<IActionResult> Register(RegisterAccountCommand account, CancellationToken token = default(CancellationToken))
        {
            ModelState.ThrowValidationExceptionIfInvalid<Account.Errors>();

            await commandFactory.Execute(account);
            var userAccount = await queryFactory.ResolveQuery<AccountByAuthTokenQuery>().Execute(account.AuthToken);
            var userInfo = AutoMapper.Mapper.Map<Account, AccountDto>(userAccount);
            return StatusCode(201, new AuthInfoDto(userInfo, account.AuthToken));
        }

        [HttpPatch]
        public async Task<IActionResult> ValidateToken(ValidateTokenCommand authData, CancellationToken token = default(CancellationToken))
        {
            ModelState.ThrowValidationExceptionIfInvalid<Account.Errors>();

            await commandFactory.Execute(authData);
            return Ok();
        }
    }
}