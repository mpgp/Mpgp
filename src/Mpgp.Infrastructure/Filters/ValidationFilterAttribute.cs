// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

using System;
using System.Linq;

using Microsoft.AspNetCore.Mvc.Filters;

namespace Mpgp.Infrastructure.Filters
{
    public class ValidationFilterAttribute<T> : IActionFilter
        where T : class, Abstract.IEntityErrors
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errorList = (Abstract.IEntityErrors)Activator.CreateInstance(typeof(T));
                var errorCode = context.ModelState.Values.First().Errors.First().ErrorMessage;
                if (!errorList.Messages.TryGetValue(errorCode, out var errorMessage))
                {
                    errorMessage = "Invalid data";
                    errorCode = "400";
                }

                throw new Shared.Exceptions.ValidationException(errorMessage, errorCode);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}