using Catalog.API.DAL.Entities;
using Catalog.API.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Services.Common.ResultWrappers;
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
                return await DatabaseContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            }

            return await DatabaseContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
