// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

using System;
using System.Net;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Mpgp.Shared.Exceptions;

namespace Mpgp.RestApiServer.Utils
{
    public class ErrorHandlingMiddleware
    {
        private readonly ILogger<ErrorHandlingMiddleware> logger;
        private readonly RequestDelegate next;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
                if (context.Response.StatusCode == (int)HttpStatusCode.InternalServerError)
                {
                    logger.LogError(ex, ex.Message);
                }
                else
                {
                    logger.LogWarning(ex, ex.Message);
                }
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var errorCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            if (exception is ConflictException)
            {
                errorCode = (int)HttpStatusCode.Conflict;
            }
            else if (exception is ForbiddenException)
            {
                errorCode = (int)HttpStatusCode.Forbidden;
            }
            else if (exception is NotFoundException)
            {
                errorCode = (int)HttpStatusCode.NotFound;
            }
            else if (exception is UnauthorizedException)
            {
                errorCode = (int)HttpStatusCode.Unauthorized;
            }
            else if (exception is ValidationException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                errorCode = int.Parse(((ValidationException)exception).ErrorCode);
                await context.Response.WriteAsync(
                    Newtonsoft.Json.JsonConvert.SerializeObject(new { errorCode, message = exception.Message, }));
                return;
            }
            else if (exception is DomainException)
            {
                errorCode = (int)HttpStatusCode.BadRequest;
            }

            var result = Newtonsoft.Json.JsonConvert.SerializeObject(new { errorCode, message = exception.Message, });
            context.Response.StatusCode = errorCode;
            await context.Response.WriteAsync(result);
        }
    }
}
