// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

using System;
using System.Linq;

using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Mpgp.RestApiServer.Utils
{
    public static class ModelStateExtensions
    {
        public static void ThrowValidationExceptionIfInvalid<T>(this ModelStateDictionary modelState)
            where T : class, Abstract.IErrorList
        {
            if (!modelState.IsValid)
            {
                var errorList = (Abstract.IErrorList)Activator.CreateInstance(typeof(T));
                var errorCode = modelState.Values.First().Errors.First().ErrorMessage;
                if (!errorList.Messages.TryGetValue(errorCode, out var errorMessage))
                {
                    errorMessage = "Invalid data";
                       
                }
                throw new Shared.Exceptions.ValidationException(errorMessage, errorCode);
            }
        }
    }
}
