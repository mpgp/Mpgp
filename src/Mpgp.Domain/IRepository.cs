// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

using System.Threading.Tasks;

namespace Mpgp.Domain
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        Task Add(TEntity entity);

        void Remove(TEntity entity);
    }
}
