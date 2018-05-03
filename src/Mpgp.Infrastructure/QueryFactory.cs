// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

using System;

using Microsoft.Extensions.DependencyInjection;
using Mpgp.Abstract;

namespace Mpgp.Infrastructure
{
    public class QueryFactory : IQueryFactory
    {
        private readonly IServiceProvider serviceProvider;

        public QueryFactory(IServiceProvider serviceProvider) => this.serviceProvider = serviceProvider;

        public T ResolveQuery<T>()
            where T : class, IQuery
            => serviceProvider.GetService<T>();
    }
}