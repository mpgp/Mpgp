// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Mpgp.Abstract;
using Mpgp.Domain.Accounts.Commands;
using Mpgp.Domain.Accounts.Dtos;
using Mpgp.Domain.Accounts.Entities;
using Mpgp.Domain.Accounts.Queries;
using Mpgp.RestApiServer.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

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
        public async Task Authorize(
            AuthorizeAccountCommand command,
            CancellationToken token = default(CancellationToken))
        {
            ModelState.ThrowValidationExceptionIfInvalid<Account.Errors>();

            var account = await queryFactory.ResolveQuery<AccountByLoginAndPasswordQuery>()
                .Execute(command.Login, command.Password);
            Response.ContentType = "application/json";
            Response.StatusCode = 200;
            await Response.WriteAsync(GetTokenData(account), token);
        }

        [HttpGet("{accountId}")]
        public async Task<IActionResult> GetInfo(int accountId, CancellationToken token = default(CancellationToken))
        {
            var response = await queryFactory.ResolveQuery<AccountByIdQuery>().Execute(accountId);
            return Ok(AutoMapper.Mapper.Map<Account, AccountDto>(response));
        }

        [HttpPut]
        public async Task Register(
            RegisterAccountCommand command,
            CancellationToken token = default(CancellationToken))
        {
            ModelState.ThrowValidationExceptionIfInvalid<Account.Errors>();

            await commandFactory.Execute(command);
            var account = await queryFactory.ResolveQuery<AccountByLoginAndPasswordQuery>()
                .Execute(command.Login, command.Password);
            Response.ContentType = "application/json";
            Response.StatusCode = 201;
            await Response.WriteAsync(GetTokenData(account), token);
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
                new Claim("AccountId", account.AccountId.ToString()),
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
            var serializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }
    }
}