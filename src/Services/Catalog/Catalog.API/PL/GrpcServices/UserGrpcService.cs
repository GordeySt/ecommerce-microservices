using Identity.Grpc.Protos;
using System;
using System.Threading.Tasks;

namespace Catalog.API.PL.GrpcServices
{
    public class UserGrpcService
    {
        private readonly UserProtoService.UserProtoServiceClient _userProtoService;

        public UserGrpcService(UserProtoService.UserProtoServiceClient userProtoService)
        {
            _userProtoService = userProtoService;
        }

        public async Task<ApplicationUserModel> GetCurrentUser(Guid id)
        {
            var userRequest = new GetCurrentUserRequest { Id = id.ToString() };

            return await _userProtoService.GetCurrentUserAsync(userRequest);
        }
    }
}

