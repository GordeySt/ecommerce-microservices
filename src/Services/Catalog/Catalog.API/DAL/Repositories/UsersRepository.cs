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

        public async Task<User> GetUserByIdAsync(Guid id) =>
            await DatabaseContext
            .Users
            .AsNoTracking()
            .Include(t => t.Ratings)
            .ThenInclude(t => t.Product)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}
