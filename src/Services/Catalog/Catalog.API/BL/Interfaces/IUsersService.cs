using Catalog.API.DAL.Entities;
using Identity.Grpc.Protos;
using Services.Common.ResultWrappers;
using System.Threading.Tasks;

namespace Catalog.API.BL.Interfaces
{
    public interface IUsersService
    {
        public Task<ServiceResult<User>> AddUserAsync(ApplicationUserModel userModel);
    }
}
