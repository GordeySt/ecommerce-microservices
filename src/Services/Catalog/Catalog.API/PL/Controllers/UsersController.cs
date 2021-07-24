using Catalog.API.PL.GrpcServices;
using Identity.Grpc.Protos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Catalog.API.PL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserGrpcService _userGrpcService;

        public UsersController(UserGrpcService userGrpcService)
        {
            _userGrpcService = userGrpcService;
        }

        [HttpGet("{id:guid}/createuser")]
        public async Task<ActionResult<ApplicationUserModel>> CreateUser(Guid id) => 
            await _userGrpcService.GetUserById(id);
    }
}
