using FluentAssertions;
using FluentValidation;
using Identity.Application.ApplicationRoles.Commands.UpdateRoles;
using Identity.Application.Common;
using Identity.Domain.Entities;
using Identity.Tests.UnitTests.Shared;
using Microsoft.AspNetCore.Identity;
using Moq;
using NUnit.Framework;
using Services.Common.Enums;
using System;
using System.Threading.Tasks;

namespace Identity.Tests.UnitTests.ApplicationRoles.Commands
{
    public class UpdateRoleCommandTests
    {
        private readonly Mock<IRoleStore<ApplicationRole>> _roleStoreStub = new();

        [Test]
        public async Task ShouldNotUpdateRoleIfItNotFound()
        {
            // Arrange
            var roleManagerStub = TestData.CreateRoleManagerMoqStub(_roleStoreStub);
            var command = new UpdateRoleCommand(RoleId: Guid.NewGuid(), 
                RoleName: Guid.NewGuid().ToString());

            var updateRoleHandler = new UpdateRoleCommandHandler(roleManagerStub.Object);

            roleManagerStub
                .Setup(t => t.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync((ApplicationRole)null);

            // Act
            var result = await updateRoleHandler.Handle(command, default);

            // Assert
            result.Result.Should().Be(ServiceResultType.NotFound);
            result.Message.Should().Be(NotFoundExceptionMessageConstants.NotFoundRoleMessage);

            roleManagerStub.Verify(t => t.FindByIdAsync(It.IsAny<string>()));
        }

        [Test]
        public async Task ShouldNotUpdateRoleIfUnhandledProblems()
        {
            // Arrange
            var roleManagerStub = TestData.CreateRoleManagerMoqStub(_roleStoreStub);
            var expectedRole = TestData.CreateAppRole();
            var expectedErrorMessage = TestData.ErrorMessage;
            var command = new UpdateRoleCommand(RoleId: Guid.NewGuid(),
                RoleName: Guid.NewGuid().ToString());

            var updateRoleHandler = new UpdateRoleCommandHandler(roleManagerStub.Object);

            roleManagerStub
                .Setup(t => t.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(expectedRole);

            roleManagerStub
                .Setup(t => t.UpdateAsync(It.IsAny<ApplicationRole>()))
                .ReturnsAsync(TestData.CreateFailedIdentityResult(expectedErrorMessage));

            // Act
            var result = await updateRoleHandler.Handle(command, default);

            // Assert
            result.Result.Should().Be(ServiceResultType.InternalServerError);
            result.Message.Should().Be(expectedErrorMessage);

            roleManagerStub.Verify(t => t.FindByIdAsync(It.IsAny<string>()));
            roleManagerStub.Verify(t => t.UpdateAsync(It.IsAny<ApplicationRole>()));
        }

        [Test]
        public async Task ShouldUpdateRole()
        {
            // Arrange
            var roleManagerStub = TestData.CreateRoleManagerMoqStub(_roleStoreStub);
            var expectedRole = TestData.CreateAppRole();
            var command = new UpdateRoleCommand(RoleId: Guid.NewGuid(),
                RoleName: Guid.NewGuid().ToString());

            var updateRoleHandler = new UpdateRoleCommandHandler(roleManagerStub.Object);

            roleManagerStub
                .Setup(t => t.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(expectedRole);

            roleManagerStub
                .Setup(t => t.UpdateAsync(It.IsAny<ApplicationRole>()))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await updateRoleHandler.Handle(command, default);

            // Assert
            result.Result.Should().Be(ServiceResultType.Success);
            expectedRole.Name.Should().Be(command.RoleName);

            roleManagerStub.Verify(t => t.FindByIdAsync(It.IsAny<string>()));
            roleManagerStub.Verify(t => t.UpdateAsync(It.IsAny<ApplicationRole>()));
        }
    }
}
