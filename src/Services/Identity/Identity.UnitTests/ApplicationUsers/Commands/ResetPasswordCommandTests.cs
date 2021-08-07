using FluentAssertions;
using Identity.Application.ApplicationUsers.Commands.ResetPasswords;
using Identity.Application.Common;
using Identity.Domain.Entities;
using Identity.UnitTests.Shared;
using Microsoft.AspNetCore.Identity;
using Moq;
using NUnit.Framework;
using Services.Common.Enums;
using System;
using System.Threading.Tasks;

namespace Identity.UnitTests.ApplicationUsers.Commands
{
    public class ResetPasswordCommandTests
    {
        private readonly Mock<IUserStore<ApplicationUser>> _userStoreStub = new();

        [Test]
        public async Task ShouldNotResetPasswordIfUserNotFound()
        {
            // Arrange
            var userManagerStub = TestData.CreateUserManagerMoqStub(_userStoreStub);
            var command = new ResetPasswordCommand(Token: Guid.NewGuid().ToString(),
                Email: Guid.NewGuid().ToString(), Password: Guid.NewGuid().ToString(),
                ConfirmPassword: Guid.NewGuid().ToString());

            var resetPasswordHandler = new ResetPasswordCommandHandler(userManagerStub.Object);

            userManagerStub
                .Setup(t => t.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((ApplicationUser)null);

            // Act
            var result = await resetPasswordHandler.Handle(command, default);

            // Assert
            result.Result.Should().Be(ServiceResultType.NotFound);
            result.Message.Should().Be(NotFoundExceptionMessageConstants.NotFoundUserMessage);

            userManagerStub.Verify(t => t.FindByEmailAsync(It.IsAny<string>()));
        }

        [Test]
        public async Task ShouldNotResetPasswordForUnhandledProblems()
        {
            // Arrange
            var userManagerStub = TestData.CreateUserManagerMoqStub(_userStoreStub);
            var expectedUser = TestData.CreateAppUser();
            var command = new ResetPasswordCommand(Token: Guid.NewGuid().ToString(),
                Email: Guid.NewGuid().ToString(), Password: Guid.NewGuid().ToString(),
                ConfirmPassword: Guid.NewGuid().ToString());

            var resetPasswordHandler = new ResetPasswordCommandHandler(userManagerStub.Object);

            userManagerStub
                .Setup(t => t.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(expectedUser);

            userManagerStub
                .Setup(t => t.ResetPasswordAsync(It.IsAny<ApplicationUser>(),
                It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(TestData.CreateFailedIdentityResult(TestData.ErrorMessage));

            // Act
            var result = await resetPasswordHandler.Handle(command, default);

            // Assert
            result.Result.Should().Be(ServiceResultType.BadRequest);
            result.Message.Should().Be(BadRequestExceptionMessageConstants.ProblemResetingPasswordMessage);

            userManagerStub.Verify(t => t.FindByEmailAsync(It.IsAny<string>()));
            userManagerStub.Verify(t => t.ResetPasswordAsync(It.IsAny<ApplicationUser>(),
                It.IsAny<string>(), It.IsAny<string>()));
        }

        [Test]
        public async Task ShouldConfirmEmail()
        {
            // Arrange
            var userManagerStub = TestData.CreateUserManagerMoqStub(_userStoreStub);
            var expectedUser = TestData.CreateAppUser();
            var command = new ResetPasswordCommand(Token: Guid.NewGuid().ToString(),
                Email: Guid.NewGuid().ToString(), Password: Guid.NewGuid().ToString(),
                ConfirmPassword: Guid.NewGuid().ToString());

            var resetPasswordHandler = new ResetPasswordCommandHandler(userManagerStub.Object);

            userManagerStub
                .Setup(t => t.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(expectedUser);

            userManagerStub
                .Setup(t => t.ResetPasswordAsync(It.IsAny<ApplicationUser>(),
                It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await resetPasswordHandler.Handle(command, default);

            // Assert
            result.Result.Should().Be(ServiceResultType.Success);

            userManagerStub.Verify(t => t.FindByEmailAsync(It.IsAny<string>()));
            userManagerStub.Verify(t => t.ResetPasswordAsync(It.IsAny<ApplicationUser>(),
                It.IsAny<string>(), It.IsAny<string>()));
        }
    }
}
