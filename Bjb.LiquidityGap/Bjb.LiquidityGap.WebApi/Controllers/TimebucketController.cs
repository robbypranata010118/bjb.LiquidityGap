using Bjb.LiquidityGap.Application.Features.Timebuckets.Commands.Create;
using Bjb.LiquidityGap.Application.Features.Timebuckets.Commands.Delete;
using Bjb.LiquidityGap.Application.Features.Timebuckets.Commands.Update;
using Bjb.LiquidityGap.Application.Features.Timebuckets.Queries.Get;
using Bjb.LiquidityGap.Application.Features.Timebuckets.Queries.GetById;
using Bjb.LiquidityGap.Base.Dtos.Timebuckets;
using Bjb.LiquidityGap.Base.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bjb.LiquidityGap.WebApi.Controllers
{
    [Authorize]
    public class TimebucketController : BaseApiController
    {
        /// <remarks>
        /// List Paramater:
        /// 
        ///     Avaiable Comparasion Operator
        ///     [        
        ///       like,
        ///       not like,
        ///       !=,
        ///       &lt;,
        ///       &gt;,
        ///       &lt;=,
        ///       &gt;=,
        ///       =
        ///     ]
        ///     Avaiable Column
        ///     [    
        ///       Id,
        ///       Code,
        ///       Name,
        ///       DateIn,
        ///       UserIn,
        ///       DateUp,
        ///       UserUp,
        ///       isActive
        ///     ]
        ///     Avaiable Sorting Type
        ///     [        
        ///       ASC,
        ///       DESC,
        ///     ]        
        /// </remarks>
        [HttpPost("GetData")]
        [ProducesResponseType(type: typeof(PagedResponse<IEnumerable<TimebucketResponse>>), statusCode: StatusCodes.Status200OK)]
        public async Task<IActionResult> GetData(GetTimebucketQuery filter)
        {
            return Ok(await Mediator.Send(filter));
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(type: typeof(Response<TimebucketResponse>), statusCode: StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new GetTimebucketByIdQuery { Id = id }));
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateTimebucketCommand command)
        {

            return Ok(await Mediator.Send(command));
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdateTimebucketCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteTimebucketCommand { Id = id }));
        }
    }
}
