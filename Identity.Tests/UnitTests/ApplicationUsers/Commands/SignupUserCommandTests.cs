using FluentAssertions;
using Identity.Application.ApplicationUsers.Commands.SignupUsers;
using Identity.Application.Common;
using Identity.Application.Common.Interfaces;
using Identity.Domain.Entities;
using Identity.Tests.UnitTests.Shared;
using Microsoft.AspNetCore.Identity;
using Moq;
using NUnit.Framework;
using Services.Common.Enums;
using System;
using System.Threading.Tasks;

namespace Identity.Tests.UnitTests.ApplicationUsers.Commands
{
    public class SignupUserCommandTests
    {
        private readonly Mock<IUserStore<ApplicationUser>> _userStoreStub = new();
        private readonly Mock<IEmailService> _emailServiceStub = new();

        [Test]
        public async Task ShouldNotSignUpUserIfUserAlreadyExists()
        {
            // Arrange
            var userManagerStub = TestData.CreateUserManagerMoqStub(_userStoreStub);
            var expectedUser = TestData.CreateAppUser();
            var command = new SignupUserCommand
            {
                Email = Guid.NewGuid().ToString(),
                Origin = Guid.NewGuid().ToString(),
                Password = Guid.NewGuid().ToString()
            };

            var signupUserHandler = new SignupUserCommandHandler(userManagerStub.Object,
                _emailServiceStub.Object);

            userManagerStub
                .Setup(t => t.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(expectedUser);

            // Act
            var result = await signupUserHandler.Handle(command, default);

            // Assert
            result.Result.Should().Be(ServiceResultType.BadRequest);
            result.Message.Should().Be(ExceptionMessageConstants.ExistedEmailMessage);

            userManagerStub.Verify(t => t.FindByEmailAsync(It.IsAny<string>()));
        }

        [Test]
        public async Task ShouldNotSignUpUserIfUnhandledProblems()
        {
            // Arrange
            var userManagerStub = TestData.CreateUserManagerMoqStub(_userStoreStub);
            var expectedUser = TestData.CreateAppUser();
            var expectedErrorMessage = TestData.ErrorMessage;
            var command = new SignupUserCommand
            {
                Email = Guid.NewGuid().ToString(),
                Origin = Guid.NewGuid().ToString(),
                Password = Guid.NewGuid().ToString()
            };

            var signupUserHandler = new SignupUserCommandHandler(userManagerStub.Object,
                _emailServiceStub.Object);

            userManagerStub
                .Setup(t => t.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((ApplicationUser)null);

            userManagerStub
                .Setup(t => t.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(TestData.CreateFailedIdentityResult(expectedErrorMessage));

            // Act
            var result = await signupUserHandler.Handle(command, default);

            // Assert
            result.Result.Should().Be(ServiceResultType.InternalServerError);
            result.Message.Should().Be(expectedErrorMessage);

            userManagerStub.Verify(t => t.FindByEmailAsync(It.IsAny<string>()));
            userManagerStub.Verify(t => t.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()));
        }

        [Test]
        public async Task ShouldSignUpUserSuccessfuly()
        {
            // Arrange
            var userManagerStub = TestData.CreateUserManagerMoqStub(_userStoreStub);
            var expectedUser = TestData.CreateAppUser();
            var expectedErrorMessage = TestData.ErrorMessage;
            var expectedConfirmationToken = "TestToken";
            var command = new SignupUserCommand
            {
                Email = Guid.NewGuid().ToString(),
                Origin = Guid.NewGuid().ToString(),
                Password = Guid.NewGuid().ToString()
            };

            var signupUserHandler = new SignupUserCommandHandler(userManagerStub.Object,
                _emailServiceStub.Object);

            userManagerStub
                .Setup(t => t.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((ApplicationUser)null);

            userManagerStub
                .Setup(t => t.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            userManagerStub
                .Setup(t => t.AddToRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            userManagerStub
                .Setup(t => t.GenerateEmailConfirmationTokenAsync(It.IsAny<ApplicationUser>()))
                .ReturnsAsync(expectedConfirmationToken);

            // Act
            var result = await signupUserHandler.Handle(command, default);

            // Assert
            result.Result.Should().Be(ServiceResultType.Success);

            userManagerStub.Verify(t => t.FindByEmailAsync(It.IsAny<string>()));
            userManagerStub.Verify(t => t.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()));
            userManagerStub.Verify(t => t.AddToRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()));
            userManagerStub.Verify(t => t.GenerateEmailConfirmationTokenAsync(It.IsAny<ApplicationUser>()));
        }
    }
}
