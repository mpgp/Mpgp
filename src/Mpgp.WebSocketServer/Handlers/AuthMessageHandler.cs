// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Mpgp.Abstract;
using Mpgp.Domain.Accounts.Dtos;
using Mpgp.Domain.Accounts.Queries;
using Mpgp.Shared;
using Mpgp.Shared.Exceptions;
using Mpgp.WebSocketServer.Core;
using Mpgp.WebSocketServer.Messages;

namespace Mpgp.WebSocketServer.Handlers
{
    public class AuthMessageHandler : MessageHandler<Messages.Client.AuthMessage>
    {
        private readonly ILogger<AuthMessageHandler> logger;
        private readonly IMapper mapper;
        private readonly IQueryFactory queryFactory;

        public AuthMessageHandler(ILogger<AuthMessageHandler> logger, IQueryFactory queryFactory, IMapper mapper)
        {
            this.logger = logger;
            this.queryFactory = queryFactory;
            this.mapper = mapper;
        }

        public override Task CheckAuth()
        {
            return Task.CompletedTask;
        }

        public override async Task Handle()
        {
            try
            {
                var account = await GetAccountByToken(Context.Message.Payload.AuthToken);
                var response = new Messages.Server.AuthMessage()
                {
                    UsersList = Context.ConnectionManager.GetOnlineUsers()
                };
                Context.ConnectionManager.AddSocket(account, Context.Socket);
                await Context.ConnectionManager.SendMessageAsync(Context.Socket, response);

                var connectionMessage = new Messages.Server.UserConnectionMessage()
                {
                    Account = account,
                    Status = ConnectionStatus.Connect
                };
                await Context.ConnectionManager.SendMessageToAllExcludeOneAsync(Context.Socket, connectionMessage);
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Unhandled exception in AuthMessageHandler in method 'Handle'.");
            }
        }

        private async Task<AccountDto> GetAccountByToken(string token)
        {
            try
            {
                var accountId = Utils.GetAccountIdFromJwt(token);
                if (Context.ConnectionManager.IsConnected(accountId))
                {
                    throw new ConflictException("ALREADY_CONNECTED");
                }

                var account = await queryFactory.ResolveQuery<AccountByIdQuery>().Execute(accountId);

                return this.mapper.Map<AccountDto>(account);
            }
            catch (NotFoundException ex)
            {
                await Disconnect(404, ex.Message);
                throw;
            }
            catch (ConflictException ex)
            {
                await Disconnect(409, ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Unhandled exception in AuthMessageHandler in method 'GetAccountByToken'.");
                throw;
            }
        }
    }
}
