// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

namespace Mpgp.Abstract
{
    public interface IQueryFactory
    {
        T ResolveQuery<T>()
            where T : class, IQuery;
    }
}