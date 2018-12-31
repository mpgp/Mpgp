// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

using System;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Mpgp.Domain;

namespace Mpgp.DataAccess.Core
{
    public class EfRepository<TEntity, TContext> : IRepository<TEntity>
        where TEntity : class
        where TContext : DbContext
    {
        protected EfRepository(TContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            Set = Context.Set<TEntity>();
        }

        protected TContext Context { get; }

        private DbSet<TEntity> Set { get; }

        public virtual async Task Add(TEntity entity)
        {
            await Set.AddAsync(entity);
        }

        public virtual void Remove(TEntity entity)
        {
            Set.Remove(entity);
        }
    }
}
