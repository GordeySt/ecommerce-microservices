using FluentAssertions;
using Identity.Application.ApplicationUsers.Commands.ConfirmEmails;
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
    public class ConfirmEmailCommandTests
    {
        private readonly Mock<IUserStore<ApplicationUser>> _userStoreStub = new();

        [Test]
        public async Task ShouldNotConfirmEmailIfUserNotFound()
        {
            // Arrange
            var userManagerStub = TestData.CreateUserManagerMoqStub(_userStoreStub);
            var command = new ConfirmEmailCommand(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());

            var confirmEmailHandler = new ConfirmEmailCommandHandler(userManagerStub.Object);

            userManagerStub
                .Setup(t => t.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((ApplicationUser)null);

            // Act
            var result = await confirmEmailHandler.Handle(command, default);

            // Assert
            result.Result.Should().Be(ServiceResultType.NotFound);
            result.Message.Should().Be(NotFoundExceptionMessageConstants.NotFoundUserMessage);

            userManagerStub.Verify(t => t.FindByEmailAsync(It.IsAny<string>()));
        }

        [Test]
        public async Task ShouldNotConfirmEmailForUnhandledProblems()
        {
            // Arrange
            var userManagerStub = TestData.CreateUserManagerMoqStub(_userStoreStub);
            var expectedUser = TestData.CreateAppUser();
            var expectedErrorMessage = TestData.ErrorMessage;
            var command = new ConfirmEmailCommand(Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString());

            var confirmEmailHandler = new ConfirmEmailCommandHandler(userManagerStub.Object);

            userManagerStub
                .Setup(t => t.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(expectedUser);

            userManagerStub
                .Setup(t => t.ConfirmEmailAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(TestData.CreateFailedIdentityResult(expectedErrorMessage));

            // Act
            var result = await confirmEmailHandler.Handle(command, default);

            // Assert
            result.Result.Should().Be(ServiceResultType.BadRequest);
            result.Message.Should().Be(BadRequestExceptionMessageConstants.ProblemVerifyingEmailMessage);

            userManagerStub.Verify(t => t.FindByEmailAsync(It.IsAny<string>()));
            userManagerStub.Verify(t => t.ConfirmEmailAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()));
        }

        [Test]
        public async Task ShouldConfirmEmail()
        {
            // Arrange
            var userManagerStub = TestData.CreateUserManagerMoqStub(_userStoreStub);
            var expectedUser = TestData.CreateAppUser();
            var command = new ConfirmEmailCommand(Guid.NewGuid().ToString(),
               Guid.NewGuid().ToString());

            var confirmEmailHandler = new ConfirmEmailCommandHandler(userManagerStub.Object);

            userManagerStub
                .Setup(t => t.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(expectedUser);

            userManagerStub
                .Setup(t => t.ConfirmEmailAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await confirmEmailHandler.Handle(command, default);

            // Assert
            result.Result.Should().Be(ServiceResultType.Success);

            userManagerStub.Verify(t => t.FindByEmailAsync(It.IsAny<string>()));
            userManagerStub.Verify(t => t.ConfirmEmailAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()));
        }
    }
}
