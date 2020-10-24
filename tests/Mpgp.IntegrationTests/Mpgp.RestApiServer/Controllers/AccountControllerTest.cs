// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using Mpgp.Abstract;
using Mpgp.DataAccess;
using Mpgp.Domain.Accounts.Commands;
using Mpgp.Domain.Accounts.Entities;
using Mpgp.Domain.Accounts.Handlers;
using Mpgp.Domain.Accounts.Queries;
using Mpgp.RestApiServer.Controllers;
using NUnit.Framework;

namespace Mpgp.IntegrationTests.Mpgp.RestApiServer.Controllers
{
    public class AccountControllerTest
    {
        private AppUnitOfWork uow;

        [SetUp]
        public void Setup()
        {
            var dbContext = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(System.Guid.NewGuid().ToString())
                .Options);

            uow = new AppUnitOfWork(dbContext);
        }

        [TearDown]
        public void TearDown()
        {
            uow.Dispose();
        }

        [Test]
        public async Task Authorize_ExpectSuccessResponse()
        {
            // Arrange
            await uow.AccountRepository.Add(new Account()
            {
                Login = "admin2018",
                Nickname = "AlexAnder",
                Password = Shared.Utils.HashString("12345678asdf"),
                Role = Account.Roles.User
            });
            await uow.SaveChanges();

            var command = new AuthorizeAccountCommand()
            {
                Login = "admin2018",
                Password = "12345678asdf"
            };

            var mockQueryFactory = new Mock<IQueryFactory>();
            mockQueryFactory.Setup(repo => repo.ResolveQuery<AccountByLoginAndPasswordQuery>())
                .Returns(() => new AccountByLoginAndPasswordQuery(uow));

            // Act
            var controller = new AccountController(null, mockQueryFactory.Object);
            await controller.Authorize(command);

            // Assert
            Assert.Pass();
        }

        [Test]
        public async Task GetInfo_ExpectSuccessResponse()
        {
            // Arrange
            await uow.AccountRepository.Add(new Account()
            {
                Avatar = "29.jpg",
                Login = "admin2018",
                Nickname = "AlexAnder",
                Password = Shared.Utils.HashString("12345678asdf")
            });
            await uow.SaveChanges();

            var mockQueryFactory = new Mock<IQueryFactory>();
            mockQueryFactory.Setup(repo => repo.ResolveQuery<AccountByIdQuery>())
                .Returns(() => new AccountByIdQuery(uow));

            // Act
            var account = await uow.AccountRepository.GetByLogin("admin2018");
            var controller = new AccountController(null, mockQueryFactory.Object);
            var model = await controller.GetInfo(account.Id);

            // Assert
            Assert.NotNull(model);
            Assert.AreEqual("AlexAnder", model.Nickname);
            Assert.AreEqual("29.jpg", model.Avatar);
        }

        [Test]
        public async Task Register_ExpectSuccessResponse()
        {
            // Arrange
            var command = new RegisterAccountCommand()
            {
                Login = "admin2018",
                Nickname = "AlexAnder",
                Password = "12345678asdf",
                PasswordRepeat = "12345678asdf"
            };

            var mockCommandFactory = new Mock<ICommandFactory>();
            mockCommandFactory.Setup(repo => repo.Execute(command))
                .Returns(async () =>
                {
                    var handler = new RegisterAccountCommandHandler(uow);
                    await handler.Execute(command);
                });

            var mockQueryFactory = new Mock<IQueryFactory>();
            mockQueryFactory.Setup(repo => repo.ResolveQuery<AccountByLoginAndPasswordQuery>())
                .Returns(() => new AccountByLoginAndPasswordQuery(uow));

            // Act
            var controller = new AccountController(mockCommandFactory.Object, mockQueryFactory.Object);
            await controller.Register(command);

            // Assert
            Assert.Pass();
        }
    }
}
