using Identity.Domain.Entities;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Identity.Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedDefaultUsersAsync
            (UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            var administratorRole = new ApplicationRole { Name = "Admin" };
            var userRole = new ApplicationRole { Name = "User" };

            if (roleManager.Roles.All(r => r.Name != administratorRole.Name))
            {
                await roleManager.CreateAsync(administratorRole);
            }

            if (roleManager.Roles.All(r => r.Name != userRole.Name))
            {
                await roleManager.CreateAsync(userRole);
            }

            var administrator = new ApplicationUser
            {
                Id = Guid.NewGuid(),
                UserName = "admin",
                Email = "administrator@localhost",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var user = new ApplicationUser
            {
                Id = Guid.NewGuid(),
                UserName = "user",
                Email = "user@localhost",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            if (userManager.Users.All(u => u.UserName != administrator.UserName))
            {
                await userManager.CreateAsync(administrator, "pa$$w0rd");
                await userManager.AddToRolesAsync(administrator, new[] { administratorRole.Name });
            }

            if (userManager.Users.All(u => u.UserName != user.UserName))
            {
                var result = await userManager.CreateAsync(user, "pa$$w0rd");
                await userManager.AddToRolesAsync(user, new[] { userRole.Name });
            }
        }
    }
}
