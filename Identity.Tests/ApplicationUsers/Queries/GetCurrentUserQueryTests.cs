using AutoMapper;
using FluentAssertions;
using Identity.Application.ApplicationUsers.Queries.GetUsersByTokenInfo;
using Identity.Application.Common;
using Identity.Application.Common.Interfaces;
using Identity.Application.Common.Mappings;
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
    public class GetCurrentUserQueryTests
    {
        private readonly Mock<IUserStore<ApplicationUser>> _userStoreStub = new();
        private readonly Mock<ICurrentUserService> _currentUserServiceStub = new();
        private readonly IMapper _mapper;

        public GetCurrentUserQueryTests()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configuration.CreateMapper();
        }

        [Test]
        public async Task ShouldNotReturnCurrentUserIfInvalidToken()
        {
            // Arrange
            var userManagerStub = TestData.CreateUserManagerMoqStub(_userStoreStub);
            var users = new List<ApplicationUser>()
            {
                TestData.CreateAppUser(),
                TestData.CreateAppUser(),
                TestData.CreateAppUser()
            };
            var expectedUserId = Guid.NewGuid();
            var mockUsers = users.AsQueryable().BuildMock();

            var query = new GetCurrentUserQuery();

            var handler = new GetCurrentUserQueryHandler(_currentUserServiceStub.Object,
                userManagerStub.Object, _mapper);

            userManagerStub
                .Setup(t => t.Users)
                .Returns(mockUsers.Object);

            _currentUserServiceStub
                .Setup(t => t.UserId)
                .Returns(expectedUserId.ToString());

            // Act
            var result = await handler.Handle(query, default);

            // Assert
            result.Result.Should().Be(ServiceResultType.BadRequest);
            result.Message.Should().Be(ExceptionMessageConstants.InvalidTokenMessage);

            userManagerStub.Verify(t => t.Users);
            _currentUserServiceStub.Verify(t => t.UserId);
        }

        [Test]
        public async Task ShouldReturnCurrentUser()
        {
            // Arrange
            var userManagerStub = TestData.CreateUserManagerMoqStub(_userStoreStub);
            var users = new List<ApplicationUser>()
            {
                TestData.CreateCurrentAppUser(),
                TestData.CreateAppUser(),
                TestData.CreateAppUser()
            };
            var expectedUserId = new Guid("edbf4592-f282-4cfe-afc8-1204a8231549");
            var mockUsers = users.AsQueryable().BuildMock();

            userManagerStub
                .Setup(t => t.Users)
                .Returns(mockUsers.Object);

            _currentUserServiceStub
                .Setup(t => t.UserId)
                .Returns(expectedUserId.ToString());

            var query = new GetCurrentUserQuery();

            var handler = new GetCurrentUserQueryHandler(_currentUserServiceStub.Object,
                userManagerStub.Object, _mapper);

            // Act
            var result = await handler.Handle(query, default);

            // Assert
            result.Result.Should().Be(ServiceResultType.Success);
            result.Data.Id.Should().Be(expectedUserId);
            result.Data.Email.Should().Be(users.First().Email);

            userManagerStub.Verify(t => t.Users);
            _currentUserServiceStub.Verify(t => t.UserId);
        }
    }
}
