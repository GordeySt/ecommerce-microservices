using FluentAssertions;
using Identity.Application.ApplicationRoles.Commands.DeleteRoles;
using Identity.Domain.Entities;
using Identity.UnitTests.Shared;
using Microsoft.AspNetCore.Identity;
using Moq;
using NUnit.Framework;
using Services.Common.Constatns;
using Services.Common.Enums;
using System;
using System.Threading.Tasks;

namespace Identity.UnitTests.ApplicationRoles.Commands
{
    public class DeleteRoleCommandTests
    {
        private readonly Mock<IRoleStore<ApplicationRole>> _roleStoreStub = new();

        [Test]
        public async Task ShouldNotDeleteRoleIfItDoesNotExist()
        {
            // Arrange
            var roleManagerStub = TestData.CreateRoleManagerMoqStub(_roleStoreStub);
            var command = new DeleteRoleCommand(Guid.NewGuid());

            var deleteRoleHandler = new DeleteRoleCommandHandler(roleManagerStub.Object);

            roleManagerStub
                .Setup(t => t.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync((ApplicationRole)null);

            // Act
            var result = await deleteRoleHandler.Handle(command, default);

            // Assert
            result.Result.Should().Be(ServiceResultType.NotFound);
            result.Message.Should().Be(ExceptionConstants.NotFoundItemMessage);

            roleManagerStub.Verify(t => t.FindByIdAsync(It.IsAny<string>()));
        }

        [Test]
        public async Task ShouldNotDeleteRoleForUnhandledProblems()
        {
            // Arrange
            var roleManagerStub = TestData.CreateRoleManagerMoqStub(_roleStoreStub);
            var command = new DeleteRoleCommand(Guid.NewGuid());
            var expectedErrorMessage = TestData.ErrorMessage;
            var expectedRole = TestData.CreateAppRole();

            var deleteRoleHandler = new DeleteRoleCommandHandler(roleManagerStub.Object);

            roleManagerStub
                .Setup(t => t.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(expectedRole);

            roleManagerStub
                .Setup(t => t.DeleteAsync(It.IsAny<ApplicationRole>()))
                .ReturnsAsync(TestData.CreateFailedIdentityResult(expectedErrorMessage));

            // Act
            var result = await deleteRoleHandler.Handle(command, default);

            // Assert
            result.Result.Should().Be(ServiceResultType.InternalServerError);
            result.Message.Should().Be(expectedErrorMessage);

            roleManagerStub.Verify(t => t.FindByIdAsync(It.IsAny<string>()));
            roleManagerStub.Verify(t => t.DeleteAsync(It.IsAny<ApplicationRole>()));
        }

        [Test]
        public async Task ShouldDeleteRole()
        {
            // Arrange
            var roleManagerStub = TestData.CreateRoleManagerMoqStub(_roleStoreStub);
            var command = new DeleteRoleCommand(Guid.NewGuid());
            var expectedRole = TestData.CreateAppRole();

            var deleteRoleHandler = new DeleteRoleCommandHandler(roleManagerStub.Object);

            roleManagerStub
                .Setup(t => t.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(expectedRole);

            roleManagerStub
                .Setup(t => t.DeleteAsync(It.IsAny<ApplicationRole>()))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await deleteRoleHandler.Handle(command, default);

            // Assert
            result.Result.Should().Be(ServiceResultType.Success);

            roleManagerStub.Verify(t => t.FindByIdAsync(It.IsAny<string>()));
            roleManagerStub.Verify(t => t.DeleteAsync(It.IsAny<ApplicationRole>()));
        }
    }
}
