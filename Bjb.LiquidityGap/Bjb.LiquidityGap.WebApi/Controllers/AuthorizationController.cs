using Bjb.LiquidityGap.Application.Features.Auth.Commands.LoginUIM;
using Bjb.LiquidityGap.Base.Dtos.Auth;
using Bjb.LiquidityGap.Base.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bjb.LiquidityGap.WebApi.Controllers
{
    [AllowAnonymous]
    public class AuthorizationController : BaseApiController
    {
        [ProducesResponseType(type: typeof(Response<AuthResponse>), statusCode: StatusCodes.Status200OK)]
        [HttpPost(template: "login")]
        public async Task<IActionResult> Login([FromBody] LoginUIMCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost(template: "logout")]
        public IActionResult Logout()
        {
            return Ok();
        }
    }
}
