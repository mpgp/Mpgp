// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mpgp.Abstract;
using Mpgp.DataAccess;
using Mpgp.Domain;
using Mpgp.Domain.Accounts.Commands;
using Mpgp.Domain.Accounts.Handlers;
using Mpgp.Domain.Accounts.Queries;

namespace Mpgp.Infrastructure
{
    public static class CommonDiConfig
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddAutoMapper();
            Mapper.Initialize(cfg => cfg.AddProfile<AutoMapperProfile>());

            services.AddTransient(sp =>
            {
                var configuration = sp.GetRequiredService<IConfiguration>();

                var connectionString = configuration["Params:DefaultConnectionString"];
                if (connectionString == "psql" || connectionString == "template")
                {
                    return new AppDbContext(new DbContextOptionsBuilder<AppDbContext>()
                        .UseNpgsql(configuration.GetConnectionString(connectionString))
                        .Options);
                }

                return new AppDbContext(new DbContextOptionsBuilder<AppDbContext>()
                    .UseSqlServer(configuration.GetConnectionString(connectionString))
                    .Options);
            });
            services.AddTransient<IAppUnitOfWork, AppUnitOfWork>();
            services.AddTransient<ICommandFactory, CommandFactory>();
            services.AddTransient<IQueryFactory, QueryFactory>();

            // todo: register all handlers
            services.AddTransient<ICommandHandler<RegisterAccountCommand>, RegisterAccountCommandHandler>();
            services.AddTransient<ICommandHandler<UpdateLastOnlineCommand>, UpdateLastOnlineCommandHandler>();
            services.AddTransient<ICommandHandler<UpdatePasswordCommand>, UpdatePasswordCommandHandler>();
            services.AddTransient<ICommandHandler<UpdateAccountCommand>, UpdateAccountCommandHandler>();

            // todo: register all queries
            services.AddTransient<AccountByIdQuery>();
            services.AddTransient<AccountByLoginAndPasswordQuery>();
        }
    }
}
