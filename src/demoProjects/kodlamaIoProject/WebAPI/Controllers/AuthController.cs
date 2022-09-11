using Application.Features.Users.Commands.CreateUser;
using Application.Features.Users.Queries.LoginUser;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] CreateUserCommand createUserCommand)
        {
            var result = await Mediator.Send(createUserCommand);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginUserQuery loginUserQuery)
        {
            var result = await Mediator.Send(loginUserQuery);

            return Ok(result);
        }
    }
}
