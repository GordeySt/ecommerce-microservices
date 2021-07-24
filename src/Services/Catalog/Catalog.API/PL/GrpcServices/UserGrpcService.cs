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

        public async Task<ApplicationUserModel> GetUserById(Guid id)
        {
            var userRequest = new GetUserByIdRequest { Id = id.ToString() };

            return await _userProtoService.GetUserByIdAsync(userRequest);
        }
    }
}

