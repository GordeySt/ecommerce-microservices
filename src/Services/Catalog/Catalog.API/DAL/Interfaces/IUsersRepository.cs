using Catalog.API.DAL.Entities;
using System;
using System.Threading.Tasks;

namespace Catalog.API.DAL.Interfaces
{
    public interface IUsersRepository : IAsyncBaseRepository<User>
    {
        public Task<User> GetUserByIdAsync(Guid id, bool disableTracking = true);
    }
}
