using Identity.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;

namespace Identity.UnitTests.Shared
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

        public static ApplicationUser CreateCurrentAppUser() =>
            new()
            {
                Id = new Guid("edbf4592-f282-4cfe-afc8-1204a8231549"),
                Email = "Test Email",
                UserName = "Test Email"
            };

        public static IdentityResult CreateFailedIdentityResult(string error) =>
            IdentityResult.Failed(new IdentityError
            {
                Description = error
            });

        public static Mock<RoleManager<ApplicationRole>> CreateRoleManagerMoqStub
            (Mock<IRoleStore<ApplicationRole>> roleStoreStub) => 
            new(roleStoreStub.Object, null, null, null, null);

        public static Mock<UserManager<ApplicationUser>> CreateUserManagerMoqStub
            (Mock<IUserStore<ApplicationUser>> userStoreStub) =>
            new(userStoreStub.Object, null, null, null, null, null, null, null, null);
    }
}
