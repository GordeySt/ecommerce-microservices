using Identity.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;

namespace Identity.Tests.UnitTests.Shared
{
    public static class TestData
    {
        public const string ErrorMessage = "TestError";

        public static ApplicationRole CreateAppRole() =>
            new()
            {
                Id = Guid.NewGuid(),
                Name = "TestName"
            };

        public static ApplicationUser CreateAppUser() =>
            new()
            {
                Id = Guid.NewGuid(),
                Email = "Test Email",
                UserName = "Test Email"
            };

        public static IdentityResult CreateFailedIdentityResult(string error) =>
            IdentityResult.Failed(new IdentityError
            {
                Description = error
            });

        public static Mock<RoleManager<ApplicationRole>> CreateRoleManagerMoqStub
            (Mock<IRoleStore<ApplicationRole>> _roleStoreStub) => 
            new(_roleStoreStub.Object, null, null, null, null);

        public static Mock<UserManager<ApplicationUser>> CreateUserManagerMoqStub
            (Mock<IUserStore<ApplicationUser>> _userStoreStub) =>
            new(_userStoreStub.Object, null, null, null, null, null, null, null, null);
    }
}
