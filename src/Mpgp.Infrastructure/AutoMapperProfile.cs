// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

using Mpgp.Domain.Accounts.Commands;
using Mpgp.Domain.Accounts.Dtos;
using Mpgp.Domain.Accounts.Entities;

namespace Mpgp.Infrastructure
{
    public class AutoMapperProfile : AutoMapper.Profile
    {
        public AutoMapperProfile()
        {
            ConfigureAccount();
        }

        private void ConfigureAccount()
        {
            CreateMap<Account, AccountDto>();
            CreateMap<AuthorizeAccountCommand, Account>();
            CreateMap<RegisterAccountCommand, Account>();
        }
    }
}