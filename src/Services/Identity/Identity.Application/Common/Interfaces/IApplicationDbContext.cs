using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading;
using System.Threading.Tasks;

namespace Identity.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<ApplicationUser> Users { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        EntityEntry<TEntity> Remove<TEntity>(TEntity entity) where TEntity : class;
    }
}
