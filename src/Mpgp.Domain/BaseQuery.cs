// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

using Mpgp.Abstract;

namespace Mpgp.Domain
{
    public abstract class BaseQuery : IQuery
    {
        protected BaseQuery(IAppUnitOfWork uow) => Uow = uow;

        protected IAppUnitOfWork Uow { get; private set; }
    }
}