// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Mpgp.Abstract;
using Mpgp.Domain.Accounts.Commands;
using Mpgp.Domain.Accounts.Dtos;
using Mpgp.Domain.Accounts.Entities;
using Mpgp.Domain.Accounts.Queries;
using Mpgp.RestApiServer.Utils;
using Newtonsoft.Json;

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
        public async Task<IActionResult> Authorize(
            AuthorizeAccountCommand command)
        {
            ModelState.ThrowValidationExceptionIfInvalid<Account.Errors>();

            var account = await queryFactory.ResolveQuery<AccountByLoginAndPasswordQuery>()
                .Execute(command.Login, command.Password);
            return Ok(GetTokenData(account));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetInfo(int id)
        {
            var response = await queryFactory.ResolveQuery<AccountByIdQuery>().Execute(id);
            return Ok(AutoMapper.Mapper.Map<Account, AccountDto>(response));
        }

        [HttpPut]
        public async Task<IActionResult> Register(
            RegisterAccountCommand command)
        {
            ModelState.ThrowValidationExceptionIfInvalid<Account.Errors>();

            await commandFactory.Execute(command);
            var account = await queryFactory.ResolveQuery<AccountByLoginAndPasswordQuery>()
                .Execute(command.Login, command.Password);
            return Ok(GetTokenData(account));
        }

        private static string BuildJwt(ClaimsIdentity identity)
        {
            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                AuthOptions.ISSUER,
                AuthOptions.AUDIENCE,
                identity.Claims,
                now,
                now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        private static ClaimsIdentity GetIdentity(Account account)
        {
            var claims = new List<Claim>
            {
                new Claim("Id", account.Id.ToString()),
                new Claim(ClaimsIdentity.DefaultNameClaimType, account.Nickname),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, account.Role)
            };
            return new ClaimsIdentity(
                claims,
                "Token",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
        }

        private static string GetTokenData(Account account)
        {
            var response = new
            {
                access_token = BuildJwt(GetIdentity(account)),
                user = AutoMapper.Mapper.Map<Account, AccountDto>(account)
            };

            return JsonConvert.SerializeObject(response, Shared.Utils.JsonSettings);
        }
    }
}
