using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Persistence.Configurations
{
    public class AppUserRoleConfiguration : IEntityTypeConfiguration<ApplicationUserRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserRole> builder)
        {
             builder
                .HasKey(pc => new { pc.RoleId, pc.UserId });

            builder
                .HasOne(x => x.AppRole)
                .WithMany(x => x.AppUserRoles)
                .HasForeignKey(x => x.RoleId)
                .HasPrincipalKey(x => x.Id)
                .IsRequired();

            builder
                .HasOne(x => x.AppUser)
                .WithMany(x => x.AppUserRoles)
                .HasForeignKey(x => x.UserId)
                .HasPrincipalKey(x => x.Id)
                .IsRequired();
        }
    }
}
