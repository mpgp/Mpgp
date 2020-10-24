// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Mpgp.DataAccess;
using Mpgp.Domain;
using Mpgp.Domain.Accounts.Commands;
using Mpgp.Domain.Accounts.Handlers;
using Mpgp.Domain.Accounts.Queries;
using Mpgp.Infrastructure;
using Mpgp.Shared.Exceptions;
using NUnit.Framework;

namespace Mpgp.IntegrationTests
{
    public class AuthorizeAfterRegisterTest
    {
        private readonly List<IDisposable> disposables = new List<IDisposable>();
        private IAppUnitOfWork uow;
        private IMapper mapper;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var dbContext = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options);

            uow = new AppUnitOfWork(dbContext);
            disposables.Add(uow);

            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());

            mapper = config.CreateMapper();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            disposables.ForEach(x => x.Dispose());
        }

        [Test]
        [Order(1)]
        public void AuthorizeNonExistingAccount_ExpectNotFoundException()
        {
            // Arrange
            var account = new AuthorizeAccountCommand()
            {
                Login = "admin2018",
                Password = "12345678asdf",
            };
            var query = new AccountByLoginAndPasswordQuery(uow);

            // Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await query.Execute(account.Login, account.Password));
        }

        [Test]
        [Order(2)]
        public async Task RegisterAccount_ExpectSuccessResponse()
        {
            // Arrange
            var account = new RegisterAccountCommand()
            {
                Login = "admin2018",
                Password = "12345678asdf",
                PasswordRepeat = "12345678asdf",
            };
            var handler = new RegisterAccountCommandHandler(uow, mapper);
            disposables.Add(handler);

            // Act
            var result = await handler.Execute(account);

            // Assert
            Assert.AreEqual(1, result);
        }

        [Test]
        [Order(3)]
        public void RegisterAccount_ExpectConflictException()
        {
            // Arrange
            var account = new RegisterAccountCommand()
            {
                Login = "admin2018",
                Password = "12345678asdf",
                PasswordRepeat = "12345678asdf",
            };
            var handler = new RegisterAccountCommandHandler(uow, mapper);
            disposables.Add(handler);

            // Assert
            Assert.ThrowsAsync<ConflictException>(async () => await handler.Execute(account));
        }

        [Test]
        [Order(4)]
        public async Task AuthorizeExistingAccount_ExpectSuccessResponse()
        {
            // Arrange
            var account = new AuthorizeAccountCommand()
            {
                Login = "admin2018",
                Password = "12345678asdf",
            };
            var query = new AccountByLoginAndPasswordQuery(uow);

            // Act
            var authorizeResult = await query.Execute(account.Login, account.Password);

            // Assert
            Assert.NotNull(authorizeResult);
        }
    }
}
