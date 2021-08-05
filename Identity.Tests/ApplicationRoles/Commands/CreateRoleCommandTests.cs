using FluentAssertions;
using Identity.Application.ApplicationRoles.Commands.CreateRoles;
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
    public class CreateRoleCommandTests
    {
        private readonly Mock<IRoleStore<ApplicationRole>> _roleStoreStub = new();

        [Test]
        public async Task ShouldCreateRole()
        {
            // Arrange
            var roleManagerStub = TestData.CreateRoleManagerMoqStub(_roleStoreStub);
            var command = new CreateRoleCommand(Guid.NewGuid().ToString());

            var createRoleHandler = new CreateRoleCommandHandler(roleManagerStub.Object);

            roleManagerStub
                .Setup(t => t.RoleExistsAsync(It.IsAny<string>()))
                .ReturnsAsync(false);

            roleManagerStub
                .Setup(t => t.CreateAsync(It.IsAny<ApplicationRole>()))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await createRoleHandler.Handle(command, default);

            // Assert
            result.Result.Should().Be(ServiceResultType.Success);
            result.Data.Name.Should().Be(command.RoleName);
            result.Data.Should().NotBeNull();
        }

        [Test]
        public async Task ShouldNotCreateRoleIfItExists()
        {
            // Arrange
            var roleManagerStub = TestData.CreateRoleManagerMoqStub(_roleStoreStub);
            var command = new CreateRoleCommand(Guid.NewGuid().ToString());

            var createRoleHandler = new CreateRoleCommandHandler(roleManagerStub.Object);

            roleManagerStub
                .Setup(t => t.RoleExistsAsync(It.IsAny<string>()))
                .ReturnsAsync(true);

            // Act
            var result = await createRoleHandler.Handle(command, default);

            // Assert
            result.Result.Should().Be(ServiceResultType.BadRequest);
            result.Message.Should().Be(BadRequestExceptionMessageConstants.RoleAlreadyExistsMessage);
        }

        [Test]
        public async Task ShouldNotCreateRoleForUnhandledProblem()
        {
            // Arrange
            var roleManagerStub = TestData.CreateRoleManagerMoqStub(_roleStoreStub);
            var command = new CreateRoleCommand(Guid.NewGuid().ToString());
            var expectedErrorMessage = TestData.ErrorMessage;

            var createRoleHandler = new CreateRoleCommandHandler(roleManagerStub.Object);

            roleManagerStub
                .Setup(t => t.RoleExistsAsync(It.IsAny<string>()))
                .ReturnsAsync(false);

            roleManagerStub
                .Setup(t => t.CreateAsync(It.IsAny<ApplicationRole>()))
                .ReturnsAsync(TestData.CreateFailedIdentityResult(expectedErrorMessage));

            // Act
            var result = await createRoleHandler.Handle(command, default);

            // Assert
            result.Result.Should().Be(ServiceResultType.InternalServerError);
            result.Message.Should().Be(expectedErrorMessage);
        }
    }
}
