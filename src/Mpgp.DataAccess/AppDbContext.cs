// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

using Microsoft.EntityFrameworkCore;
using Mpgp.Domain.Accounts.Entities;

namespace Mpgp.DataAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Account>().HasIndex(e => e.Login).IsUnique();
            modelBuilder.Entity<Account>().Property(e => e.Password).HasMaxLength(64);
        }
    }
}
