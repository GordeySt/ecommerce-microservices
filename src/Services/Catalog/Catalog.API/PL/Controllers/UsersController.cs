using Catalog.API.BL.Interfaces;
using Catalog.API.BL.Services.GrpcServices;
using Identity.Grpc.Protos;
using Microsoft.AspNetCore.Mvc;
using Services.Common.Enums;
using System;
using System.Threading.Tasks;

namespace Catalog.API.PL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserGrpcService _userGrpcService;
        private readonly IUsersService _usersService;

        public UsersController(UserGrpcService userGrpcService, IUsersService usersService)
        {
            _userGrpcService = userGrpcService;
            _usersService = usersService;
        }

        [HttpGet("{id:guid}/create-user")]
        public async Task<ActionResult<ApplicationUserModel>> CreateUser(Guid id) 
        {
            var user = await _userGrpcService.GetUserByIdAsync(id);

            var userCreationResult = await _usersService.AddUserAsync(user);

            if (userCreationResult.Result is not ServiceResultType.Success)
            {
                return StatusCode((int)userCreationResult.Result, userCreationResult.Message);
            }

            return CreatedAtAction(nameof(CreateUser), userCreationResult.Data);
        }
    }
}
