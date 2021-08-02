using Catalog.API.DAL.Entities;
using Catalog.API.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Catalog.API.DAL.Repositories
{
    public class UsersRepository : AsyncBaseRepository<User>,
        IUsersRepository
    {
        public UsersRepository(ApplicationDbContext databaseContext) : base(databaseContext)
        { }

        public async Task<User> GetUserByIdAsync(Guid id, bool disableTracking = true)
        {
            if (disableTracking)
            {
               return await DatabaseContext.Users
                    .Include(t => t.Ratings)
                    .FirstOrDefaultAsync(x => x.Id == id);
            }

            return await DatabaseContext.Users
                .Include(t => t.Ratings)
                .ThenInclude(t => t.Product)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
