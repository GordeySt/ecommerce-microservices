using FluentAssertions;
using Identity.Application.ApplicationRoles.Commands.RevokeRoleFromUser;
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
    public class RevokeRoleFromUserCommandTests
    {
        private readonly Mock<IRoleStore<ApplicationRole>> _roleStoreStub = new();
        private readonly Mock<IUserStore<ApplicationUser>> _userStoreStub = new();

        [Test]
        public async Task ShouldNotRevokeRoleIfRoleNotFound()
        {
            // Arrange
            var roleManagerStub = TestData.CreateRoleManagerMoqStub(_roleStoreStub);
            var userManagerStub = TestData.CreateUserManagerMoqStub(_userStoreStub);
            var command = new RevokeRoleFromUserCommand(Guid.NewGuid(),
                UserId: Guid.NewGuid());

            var revokeRoleHandler = new RevokeRoleFromUserCommandHandler(userManagerStub.Object, 
                roleManagerStub.Object);

            roleManagerStub
                .Setup(t => t.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync((ApplicationRole)null);

            // Act
            var result = await revokeRoleHandler.Handle(command, default);

            // Assert
            result.Result.Should().Be(ServiceResultType.NotFound);
            result.Message.Should().Be(NotFoundExceptionMessageConstants.NotFoundRoleMessage);

            roleManagerStub.Verify(t => t.FindByIdAsync(It.IsAny<string>()));
        }

        [Test]
        public async Task ShouldNotRevokeRoleIfUserNotFound()
        {
            // Arrange
            var roleManagerStub = TestData.CreateRoleManagerMoqStub(_roleStoreStub);
            var userManagerStub = TestData.CreateUserManagerMoqStub(_userStoreStub);
            var expectedRole = TestData.CreateAppRole();
            var command = new RevokeRoleFromUserCommand(Guid.NewGuid(),
                UserId: Guid.NewGuid());

            var revokeRoleHandler = new RevokeRoleFromUserCommandHandler(userManagerStub.Object, 
                roleManagerStub.Object);

            roleManagerStub
                .Setup(t => t.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(expectedRole);

            userManagerStub
                .Setup(t => t.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync((ApplicationUser)null);

            // Act
            var result = await revokeRoleHandler.Handle(command, default);

            // Assert
            result.Result.Should().Be(ServiceResultType.NotFound);
            result.Message.Should().Be(NotFoundExceptionMessageConstants.NotFoundUserMessage);

            roleManagerStub.Verify(t => t.FindByIdAsync(It.IsAny<string>()));
            userManagerStub.Verify(t => t.FindByIdAsync(It.IsAny<string>()));
        }

        [Test]
        public async Task ShouldNotRevokeRoleIfUserIsNotInRole()
        {
            // Arrange
            var roleManagerStub = TestData.CreateRoleManagerMoqStub(_roleStoreStub);
            var userManagerStub = TestData.CreateUserManagerMoqStub(_userStoreStub);
            var expectedRole = TestData.CreateAppRole();
            var expectedUser = TestData.CreateAppUser();
            var command = new RevokeRoleFromUserCommand(Guid.NewGuid(),
                UserId: Guid.NewGuid());

            var revokeRoleHandler = new RevokeRoleFromUserCommandHandler(userManagerStub.Object, 
                roleManagerStub.Object);

            roleManagerStub
                .Setup(t => t.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(expectedRole);

            userManagerStub
                .Setup(t => t.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(expectedUser);

            userManagerStub
                .Setup(t => t.IsInRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(false);

            // Act
            var result = await revokeRoleHandler.Handle(command, default);

            // Assert
            result.Result.Should().Be(ServiceResultType.BadRequest);
            result.Message.Should().Be(BadRequestExceptionMessageConstants.UserIsNotInRoleMessage);

            roleManagerStub.Verify(t => t.FindByIdAsync(It.IsAny<string>()));
            userManagerStub.Verify(t => t.FindByIdAsync(It.IsAny<string>()));
            userManagerStub.Verify(t => t.IsInRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()));
        }

        [Test]
        public async Task ShouldNotRevokeRoleForUnhandledProblems()
        {
            // Arrange
            var roleManagerStub = TestData.CreateRoleManagerMoqStub(_roleStoreStub);
            var userManagerStub = TestData.CreateUserManagerMoqStub(_userStoreStub);
            var expectedRole = TestData.CreateAppRole();
            var expectedUser = TestData.CreateAppUser();
            var expectedError = TestData.ErrorMessage;
            var command = new RevokeRoleFromUserCommand(Guid.NewGuid(),
                UserId: Guid.NewGuid());

            var revokeRoleHandler = new RevokeRoleFromUserCommandHandler(userManagerStub.Object, 
                roleManagerStub.Object);

            roleManagerStub
                .Setup(t => t.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(expectedRole);

            userManagerStub
                .Setup(t => t.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(expectedUser);

            userManagerStub
                .Setup(t => t.IsInRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(true);

            userManagerStub
                .Setup(t => t.RemoveFromRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(TestData.CreateFailedIdentityResult(expectedError));

            // Act
            var result = await revokeRoleHandler.Handle(command, default);

            // Assert
            result.Result.Should().Be(ServiceResultType.InternalServerError);
            result.Message.Should().Be(expectedError);

            roleManagerStub.Verify(t => t.FindByIdAsync(It.IsAny<string>()));
            userManagerStub.Verify(t => t.FindByIdAsync(It.IsAny<string>()));
            userManagerStub.Verify(t => t.IsInRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()));
            userManagerStub.Verify(t => t.RemoveFromRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()));
        }

        [Test]
        public async Task ShouldRevokeRole()
        {
            // Arrange
            var roleManagerStub = TestData.CreateRoleManagerMoqStub(_roleStoreStub);
            var userManagerStub = TestData.CreateUserManagerMoqStub(_userStoreStub);
            var expectedRole = TestData.CreateAppRole();
            var expectedUser = TestData.CreateAppUser();
            var command = new RevokeRoleFromUserCommand(Guid.NewGuid(),
                UserId: Guid.NewGuid());

            var revokeRoleHandler = new RevokeRoleFromUserCommandHandler(userManagerStub.Object, 
                roleManagerStub.Object);

            roleManagerStub
                .Setup(t => t.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(expectedRole);

            userManagerStub
                .Setup(t => t.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(expectedUser);

            userManagerStub
                .Setup(t => t.IsInRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(true);

            userManagerStub
                .Setup(t => t.RemoveFromRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await revokeRoleHandler.Handle(command, default);

            // Assert
            result.Result.Should().Be(ServiceResultType.Success);

            roleManagerStub.Verify(t => t.FindByIdAsync(It.IsAny<string>()));
            userManagerStub.Verify(t => t.FindByIdAsync(It.IsAny<string>()));
            userManagerStub.Verify(t => t.IsInRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()));
            userManagerStub.Verify(t => t.RemoveFromRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()));
        }
    }
}
