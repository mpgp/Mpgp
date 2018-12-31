// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

using System;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Mpgp.Abstract;

namespace Mpgp.DataAccess.Core
{
    public class EfUnitOfWork<TContext> : IUnitOfWork
        where TContext : DbContext
    {
        private bool disposed;

        protected EfUnitOfWork(TContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        protected TContext Context { get; private set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual Task<int> SaveChanges() => Context.SaveChangesAsync();

        private void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing)
            {
                if (Context != null)
                {
                    Context.Dispose();
                    Context = null;
                }
            }

            disposed = true;
        }
    }
}
