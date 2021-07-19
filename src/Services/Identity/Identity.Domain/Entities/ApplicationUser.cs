using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Identity.Domain.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ICollection<ApplicationUserRole> AppUserRoles { get; set; }
    }
}
