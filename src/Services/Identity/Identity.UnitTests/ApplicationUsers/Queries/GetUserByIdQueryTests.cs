using FluentAssertions;
using Identity.Application.ApplicationUsers.Queries.GetUserById;
using Identity.Application.Common;
using Identity.Domain.Entities;
using Identity.UnitTests.Shared;
using Microsoft.AspNetCore.Identity;
using MockQueryable.Moq;
using Moq;
using NUnit.Framework;
using Services.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.UnitTests.ApplicationUsers.Queries
{
    public class GetUserByIdQueryTests
    {
        private readonly Mock<IUserStore<ApplicationUser>> _userStoreStub = new();

        [Test]
        public async Task ShouldNotReturnUserByIdIfInvalidId()
        {
            // Arrange
            var userManagerStub = TestData.CreateUserManagerMoqStub(_userStoreStub);
            var users = new List<ApplicationUser>
            {
                TestData.CreateAppUser(),
                TestData.CreateAppUser(),
                TestData.CreateAppUser()
            };
            var expectedUserId = Guid.NewGuid();
            var mockUsers = users.AsQueryable().BuildMock();

            var query = new GetUserByIdQuery(expectedUserId);

            var handler = new GetUserByIdQueryHandler(userManagerStub.Object);

            userManagerStub
                .Setup(t => t.Users)
                .Returns(mockUsers.Object);

            // Act
            var result = await handler.Handle(query, default);

            // Assert
            result.Result.Should().Be(ServiceResultType.NotFound);
            result.Message.Should().Be(NotFoundExceptionMessageConstants.NotFoundUserMessage);

            userManagerStub.Verify(t => t.Users);
        }

        [Test]
        public async Task ShouldReturnCurrentUser()
        {
            // Arrange
            var userManagerStub = TestData.CreateUserManagerMoqStub(_userStoreStub);
            var users = new List<ApplicationUser>
            {
                TestData.CreateCurrentAppUser(),
                TestData.CreateAppUser(),
                TestData.CreateAppUser()
            };
            var expectedUserId = new Guid("edbf4592-f282-4cfe-afc8-1204a8231549");
            var mockUsers = users.AsQueryable().BuildMock();

            var query = new GetUserByIdQuery(expectedUserId);

            var handler = new GetUserByIdQueryHandler(userManagerStub.Object);

            userManagerStub
                .Setup(t => t.Users)
                .Returns(mockUsers.Object);

            // Act
            var result = await handler.Handle(query, default);

            // Assert
            result.Result.Should().Be(ServiceResultType.Success);
            result.Data.Id.Should().Be(expectedUserId);
            result.Data.Email.Should().Be(users.First().Email);

            userManagerStub.Verify(t => t.Users);
        }
    }
}
