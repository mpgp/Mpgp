// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;

using Mpgp.WebSocketServer.Abstract;
using Mpgp.WebSocketServer.Messages;
using Newtonsoft.Json;

namespace Mpgp.WebSocketServer.Core
{
    public static class ConnectionManagerExtensions
    {
        public static async Task SendMessageAsync<T>(
            this ConnectionManager connectionManager,
            WebSocket socket,
            T message,
            ErrorInfo error = null)
            where T : IMessage
        {
            var response = SerializeMessage(message, error);
            await connectionManager.SendMessageAsync(socket, response);
        }

        public static async Task SendMessageToAllAsync<T>(
            this ConnectionManager connectionManager,
            T message,
            ErrorInfo error = null)
            where T : IMessage
        {
            var response = SerializeMessage(message, error);
            await connectionManager.SendMessageToAllAsync(response);
        }

        public static async Task SendMessageToAllExcludeOneAsync<T>(
            this ConnectionManager connectionManager,
            WebSocket excludedSocket,
            T message,
            ErrorInfo error = null)
            where T : IMessage
        {
            var response = SerializeMessage(message, error);
            await connectionManager.SendMessageToAllExcludeOneAsync(excludedSocket, response);
        }

        private static string SerializeMessage<T>(T message, ErrorInfo error)
            where T : IMessage
        {
            var messageName = message.GetType().FullName
                .Split(new[] { "." }, System.StringSplitOptions.None)
                .Last();
            var messageType = string.Concat(
                messageName.Select((ch, i) => i > 0 && char.IsUpper(ch) ? "_" + ch.ToString() : ch.ToString().ToUpper()));

            var response = new ServerMessage<T>()
            {
                Type = messageType,
                Error = error,
                Payload = message
            };
            return JsonConvert.SerializeObject(response, Shared.Utils.JsonSettings);
        }
    }
}
