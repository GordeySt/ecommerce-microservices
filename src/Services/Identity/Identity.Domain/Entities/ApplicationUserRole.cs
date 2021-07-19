using Microsoft.AspNetCore.Identity;
using System;

namespace Identity.Domain.Entities
{
    public class ApplicationUserRole : IdentityUserRole<Guid>
    {
        public ApplicationUser AppUser { get; set; }
        public ApplicationRole AppRole { get; set; }
    }
}
