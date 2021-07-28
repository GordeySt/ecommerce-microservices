using Catalog.API.DAL.Entities;
using Catalog.API.PL.Models.DTOs.Users;
using Identity.Grpc.Protos;
using Services.Common.ResultWrappers;
using System;
using System.Threading.Tasks;

namespace Catalog.API.BL.Interfaces
{
    public interface IUsersService
    {
        public Task<ServiceResult<User>> AddUserAsync(ApplicationUserModel userModel);

        public Task<UserDto> GetUserByIdAsync(Guid id);
    }
}
