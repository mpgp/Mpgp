// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;

using Mpgp.WebSocketServer.Abstract;
using Mpgp.WebSocketServer.Messages;

namespace Mpgp.WebSocketServer.Core
{
    public abstract class MessageHandler<T> : IMessageHandler
        where T : IMessage, new()
    {
        protected HandlerContext<T> Context { get; set; }

        public virtual async Task CheckAuth()
        {
            if (Context.ConnectionManager.IsConnected(Context.Socket))
            {
                return;
            }

            await Disconnect(401, "Unauthorized");
            throw new Shared.Exceptions.UnauthorizedException();
        }

        public abstract Task Handle();

        public virtual IMessageHandler Initialize(string json, WebSocket socket, ConnectionManager connectionManager)
        {
            var clientMessage = Newtonsoft.Json.JsonConvert.DeserializeObject<ClientMessage<T>>(json);
            Context = new HandlerContext<T>(clientMessage, socket, connectionManager);
            return this;
        }

        public async Task Validate()
        {
            var error = ValidateModel(Context.Message.Payload);
            if (!string.IsNullOrEmpty(error))
            {
                await Disconnect(400, error);
                throw new Shared.Exceptions.ValidationException(error);
            }
        }

        protected async Task Disconnect(int code, string message)
        {
            var error = new ErrorInfo()
            {
                ErrorCode = code,
                Message = message,
            };
            await Context.ConnectionManager.SendMessageAsync(Context.Socket, new T(), error);
            await Context.ConnectionManager.RemoveSocketAsync(Context.Socket);
        }

        private static string ValidateModel(T model)
        {
            return model.GetType()
                .GetProperties()
                .Select(prop => ValidateProperty(prop.Name, prop.GetValue(model)))
                .FirstOrDefault(res => res != null);
        }

        private static string ValidateProperty(string propertyName, object value)
        {
            var context = new ValidationContext(value, null, null);
            var results = new List<ValidationResult>();
            var attributes = typeof(T)
                .GetProperty(propertyName)
                .GetCustomAttributes(false)
                .OfType<ValidationAttribute>()
                .ToArray();

            return Validator.TryValidateValue(value, context, results, attributes)
                ? null
                : results.First().ErrorMessage;
        }
    }
}
