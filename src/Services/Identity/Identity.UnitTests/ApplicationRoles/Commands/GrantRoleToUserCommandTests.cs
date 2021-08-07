using FluentAssertions;
using Identity.Application.ApplicationRoles.Commands.GrantRoleToUser;
using Identity.Application.Common;
using Identity.Domain.Entities;
using Identity.UnitTests.Shared;
using Microsoft.AspNetCore.Identity;
using Moq;
using NUnit.Framework;
using Services.Common.Enums;
using System;
using System.Threading.Tasks;

namespace Identity.UnitTests.ApplicationRoles.Commands
{
    public class GrantRoleToUserCommandTests
    {
        private readonly Mock<IRoleStore<ApplicationRole>> _roleStoreStub = new();
        private readonly Mock<IUserStore<ApplicationUser>> _userStoreStub = new();

        [Test]
        public async Task ShouldNotGrantRoleIfRoleNotFound()
        {
            // Arrange
            var roleManagerStub = TestData.CreateRoleManagerMoqStub(_roleStoreStub);
            var userManagerStub = TestData.CreateUserManagerMoqStub(_userStoreStub);
            var command = new GrantRoleToUserCommand(Guid.NewGuid(),
                UserId: Guid.NewGuid());

            var grantRoleHandler = new GrantRoleToUserCommandHandler(roleManagerStub.Object,
                userManagerStub.Object);

            roleManagerStub
                .Setup(t => t.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync((ApplicationRole)null);

            // Act
            var result = await grantRoleHandler.Handle(command, default);

            // Assert
            result.Result.Should().Be(ServiceResultType.NotFound);
            result.Message.Should().Be(NotFoundExceptionMessageConstants.NotFoundRoleMessage);

            roleManagerStub.Verify(t => t.FindByIdAsync(It.IsAny<string>()));
        }

        [Test]
        public async Task ShouldNotGrantRoleIfUserNotFound()
        {
            // Arrange
            var roleManagerStub = TestData.CreateRoleManagerMoqStub(_roleStoreStub);
            var userManagerStub = TestData.CreateUserManagerMoqStub(_userStoreStub);
            var expectedRole = TestData.CreateAppRole();
            var command = new GrantRoleToUserCommand(Guid.NewGuid(),
                UserId: Guid.NewGuid());

            var grantRoleHandler = new GrantRoleToUserCommandHandler(roleManagerStub.Object,
                userManagerStub.Object);

            roleManagerStub
                .Setup(t => t.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(expectedRole);

            userManagerStub
                .Setup(t => t.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync((ApplicationUser)null);

            // Act
            var result = await grantRoleHandler.Handle(command, default);

            // Assert
            result.Result.Should().Be(ServiceResultType.NotFound);
            result.Message.Should().Be(NotFoundExceptionMessageConstants.NotFoundUserMessage);

            roleManagerStub.Verify(t => t.FindByIdAsync(It.IsAny<string>()));
            userManagerStub.Verify(t => t.FindByIdAsync(It.IsAny<string>()));
        }

        [Test]
        public async Task ShouldNotGrantRoleIfUserIsAlreadyInRole()
        {
            // Arrange
            var roleManagerStub = TestData.CreateRoleManagerMoqStub(_roleStoreStub);
            var userManagerStub = TestData.CreateUserManagerMoqStub(_userStoreStub);
            var expectedRole = TestData.CreateAppRole();
            var expectedUser = TestData.CreateAppUser();
            var command = new GrantRoleToUserCommand(Guid.NewGuid(),
                UserId: Guid.NewGuid());

            var grantRoleHandler = new GrantRoleToUserCommandHandler(roleManagerStub.Object,
                userManagerStub.Object);

            roleManagerStub
                .Setup(t => t.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(expectedRole);

            userManagerStub
                .Setup(t => t.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(expectedUser);

            userManagerStub
                .Setup(t => t.IsInRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(true);

            // Act
            var result = await grantRoleHandler.Handle(command, default);

            // Assert
            result.Result.Should().Be(ServiceResultType.BadRequest);
            result.Message.Should().Be(BadRequestExceptionMessageConstants.UserIsInRoleMessage);

            roleManagerStub.Verify(t => t.FindByIdAsync(It.IsAny<string>()));
            userManagerStub.Verify(t => t.FindByIdAsync(It.IsAny<string>()));
            userManagerStub.Verify(t => t.IsInRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()));
        }

        [Test]
        public async Task ShouldNotGrantRoleForUnhandledProblems()
        {
            // Arrange
            var roleManagerStub = TestData.CreateRoleManagerMoqStub(_roleStoreStub);
            var userManagerStub = TestData.CreateUserManagerMoqStub(_userStoreStub);
            var expectedRole = TestData.CreateAppRole();
            var expectedUser = TestData.CreateAppUser();
            var expectedError = TestData.ErrorMessage;
            var command = new GrantRoleToUserCommand(Guid.NewGuid(),
                UserId: Guid.NewGuid());

            var grantRoleHandler = new GrantRoleToUserCommandHandler(roleManagerStub.Object,
                userManagerStub.Object);

            roleManagerStub
                .Setup(t => t.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(expectedRole);

            userManagerStub
                .Setup(t => t.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(expectedUser);

            userManagerStub
                .Setup(t => t.IsInRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(false);

            userManagerStub
                .Setup(t => t.AddToRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(TestData.CreateFailedIdentityResult(expectedError));

            // Act
            var result = await grantRoleHandler.Handle(command, default);

            // Assert
            result.Result.Should().Be(ServiceResultType.InternalServerError);
            result.Message.Should().Be(expectedError);

            roleManagerStub.Verify(t => t.FindByIdAsync(It.IsAny<string>()));
            userManagerStub.Verify(t => t.FindByIdAsync(It.IsAny<string>()));
            userManagerStub.Verify(t => t.IsInRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()));
            userManagerStub.Verify(t => t.AddToRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()));
        }

        [Test]
        public async Task ShouldGrantRole()
        {
            // Arrange
            var roleManagerStub = TestData.CreateRoleManagerMoqStub(_roleStoreStub);
            var userManagerStub = TestData.CreateUserManagerMoqStub(_userStoreStub);
            var expectedRole = TestData.CreateAppRole();
            var expectedUser = TestData.CreateAppUser();
            var command = new GrantRoleToUserCommand(Guid.NewGuid(),
                UserId: Guid.NewGuid());

            var grantRoleHandler = new GrantRoleToUserCommandHandler(roleManagerStub.Object,
                userManagerStub.Object);

            roleManagerStub
                .Setup(t => t.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(expectedRole);

            userManagerStub
                .Setup(t => t.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(expectedUser);

            userManagerStub
                .Setup(t => t.IsInRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(false);

            userManagerStub
                .Setup(t => t.AddToRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await grantRoleHandler.Handle(command, default);

            // Assert
            result.Result.Should().Be(ServiceResultType.Success);

            roleManagerStub.Verify(t => t.FindByIdAsync(It.IsAny<string>()));
            userManagerStub.Verify(t => t.FindByIdAsync(It.IsAny<string>()));
            userManagerStub.Verify(t => t.IsInRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()));
            userManagerStub.Verify(t => t.AddToRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()));
        }
    }
}
