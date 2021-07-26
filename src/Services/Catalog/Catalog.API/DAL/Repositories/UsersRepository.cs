using Catalog.API.DAL.Entities;
using Catalog.API.DAL.Interfaces;

namespace Catalog.API.DAL.Repositories
{
    public class UsersRepository : AsyncBaseRepository<User>,
        IUsersRepository
    {
        public UsersRepository(ApplicationDbContext databaseContext) : base(databaseContext)
        { }
    }
}
