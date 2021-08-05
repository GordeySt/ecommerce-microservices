using FluentAssertions;
using Identity.Application.ApplicationUsers.Queries.ResendEmailVerifications;
using Identity.Application.Common;
using Identity.Application.Common.Interfaces;
using Identity.Domain.Entities;
using Identity.UnitTests.Shared;
using Microsoft.AspNetCore.Identity;
using Moq;
using NUnit.Framework;
using Services.Common.Enums;
using System;
using System.Threading.Tasks;

namespace Identity.UnitTests.ApplicationUsers.Queries
{
    public class ResendEmailVerificationQueryTests
    {
        private readonly Mock<IUserStore<ApplicationUser>> _userStoreStub = new();
        private readonly Mock<IEmailService> _emailServiceStub = new();

        [Test]
        public async Task ShouldNotResendEmailVerificationIfEmailNotFound()
        {
            // Arrange 
            var userManagerStub = TestData.CreateUserManagerMoqStub(_userStoreStub);

            var query = new ResendEmailVerificationQuery();

            var resendEmailVerificationHandler = new ResendEmailVerificationQueryHandler(userManagerStub.Object,
                _emailServiceStub.Object);

            userManagerStub
                .Setup(t => t.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((ApplicationUser)null);

            // Act
            var result = await resendEmailVerificationHandler.Handle(query, default);

            // Assert
            result.Result.Should().Be(ServiceResultType.NotFound);
            result.Message.Should().Be(NotFoundExceptionMessageConstants.NotFoundUserMessage);

            userManagerStub.Verify(t => t.FindByEmailAsync(It.IsAny<string>()));
        }

        [Test]
        public async Task ShouldResendEmailVerification()
        {
            // Arrange 
            var userManagerStub = TestData.CreateUserManagerMoqStub(_userStoreStub);
            var expectedUser = TestData.CreateAppUser();

            var query = new ResendEmailVerificationQuery();

            var resendEmailVerificationHandler = new ResendEmailVerificationQueryHandler(userManagerStub.Object,
                _emailServiceStub.Object);

            userManagerStub
                .Setup(t => t.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(expectedUser);

            userManagerStub
                .Setup(t => t.GenerateEmailConfirmationTokenAsync(It.IsAny<ApplicationUser>()))
                .ReturnsAsync(Guid.NewGuid().ToString());

            // Act
            var result = await resendEmailVerificationHandler.Handle(query, default);

            // Assert
            result.Result.Should().Be(ServiceResultType.Success);

            userManagerStub.Verify(t => t.FindByEmailAsync(It.IsAny<string>()));
            userManagerStub.Verify(t => t.GenerateEmailConfirmationTokenAsync(It.IsAny<ApplicationUser>()));
        }
    }
}
