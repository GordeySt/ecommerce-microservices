using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Identity.Domain.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public bool IsDeleted { get; set; }
        public ICollection<ApplicationUserRole> AppUserRoles { get; set; }
    }
}
