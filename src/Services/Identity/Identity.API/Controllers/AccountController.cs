using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        [HttpGet]
        [Authorize(Roles = "User")]
        public string GetName()
        {
            return "Hey";
        }
    }
}
