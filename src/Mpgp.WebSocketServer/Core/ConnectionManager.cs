// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Mpgp.Abstract;
using Mpgp.Domain.Accounts.Commands;
using Mpgp.Domain.Accounts.Dtos;
using Mpgp.Shared;

namespace Mpgp.WebSocketServer.Core
{
    public class ConnectionManager
    {
        private readonly ICommandFactory commandFactory;
        private readonly ILogger<WebSocketRouter> logger;

        public ConnectionManager(ICommandFactory commandFactory, ILogger<WebSocketRouter> logger)
        {
            this.commandFactory = commandFactory;
            this.logger = logger;
            ConnectedSockets = new ConcurrentDictionary<AccountDto, WebSocket>();
        }

        private ConcurrentDictionary<AccountDto, WebSocket> ConnectedSockets { get; }

        public void AddSocket(AccountDto account, WebSocket socket)
        {
            ConnectedSockets.TryAdd(account, socket);
            Metrics.UsersOnline.Set(ConnectedSockets.Count());
        }

        public AccountDto GetAccount(WebSocket socket) => ConnectedSockets.FirstOrDefault(x => x.Value == socket).Key;

        public ICollection<AccountDto> GetOnlineUsers() => ConnectedSockets.Keys;

        public WebSocket FindSocket(int accountId)
            => ConnectedSockets.FirstOrDefault(p => p.Key.Id == accountId).Value;

        public bool IsConnected(int accountId) => FindSocket(accountId) != null;

        public bool IsConnected(WebSocket socket) => ConnectedSockets.Values.FirstOrDefault(x => x == socket) != null;

        public async Task RemoveSocketAsync(WebSocket socket)
        {
            var account = GetAccount(socket);
            if (account != null)
            {
                await RemoveSocketAsync(account);
            }

            try
            {
                await socket.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, null, CancellationToken.None);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public async Task SendMessageAsync(WebSocket socket, string message)
        {
            if (socket.State != WebSocketState.Open)
            {
                return;
            }

            try
            {
                await socket.SendAsync(
                    buffer: new ArraySegment<byte>(
                        array: Encoding.ASCII.GetBytes(message),
                        offset: 0,
                        count: message.Length),
                    messageType: WebSocketMessageType.Text,
                    endOfMessage: true,
                    cancellationToken: CancellationToken.None);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while sending the message: '{@message}'", message);
            }
        }

        public async Task SendMessageToAllAsync(string message)
        {
            foreach (var pair in ConnectedSockets)
            {
                await SendMessageAsync(pair.Value, message);
            }
        }

        public async Task SendMessageToAllExcludeOneAsync(WebSocket excludedSocket, string message)
        {
            var filteredSockets = ConnectedSockets.Where(client => client.Value != excludedSocket);

            foreach (var pair in filteredSockets)
            {
                await SendMessageAsync(pair.Value, message);
            }
        }

        private async Task RemoveSocketAsync(AccountDto account)
        {
            try
            {
                ConnectedSockets.TryRemove(account, out var socket);

                var connectionMessage = new Messages.Server.UserConnectionMessage()
                {
                    Account = account,
                    Status = Messages.ConnectionStatus.Disconnect,
                };
                await this.SendMessageToAllExcludeOneAsync(socket, connectionMessage);
                await commandFactory.Execute(new UpdateLastOnlineCommand(account.Id));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while removing the socket.");
            }
            finally
            {
                Metrics.UsersOnline.Set(ConnectedSockets.Count());
            }
        }
    }
}
