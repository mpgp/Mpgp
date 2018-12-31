// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

using System;
using System.Threading.Tasks;

namespace Mpgp.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChanges();
    }
}
