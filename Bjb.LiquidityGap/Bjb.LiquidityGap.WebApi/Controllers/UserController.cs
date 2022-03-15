using Bjb.LiquidityGap.Application.Features.Auth.Queries;
using Bjb.LiquidityGap.Base.Dtos.Auth;
using Bjb.LiquidityGap.Base.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bjb.LiquidityGap.WebApi.Controllers
{

    [Authorize]
    public class UserController : BaseApiController
    {
        [HttpGet("Me")]
        [ProducesResponseType(type: typeof(Response<UserMeResponse>), statusCode: StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserMe()
        {
            return Ok(await Mediator.Send(new GetUserMeQuery { }));
        }
    }
}
