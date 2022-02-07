using Bjb.LiquidityGap.Application.Features.Characteristics.Commands.Create;
using Bjb.LiquidityGap.Application.Features.Characteristics.Commands.Delete;
using Bjb.LiquidityGap.Application.Features.Characteristics.Commands.Update;
using Bjb.LiquidityGap.Application.Features.Characteristics.Queries.Get;
using Bjb.LiquidityGap.Application.Features.Characteristics.Queries.GetById;
using Bjb.LiquidityGap.Base.Dtos.Characteristics;
using Bjb.LiquidityGap.Base.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bjb.LiquidityGap.WebApi.Controllers
{
    [Authorize]
    public class CharacteristicController : BaseApiController
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
        [ProducesResponseType(type: typeof(PagedResponse<IEnumerable<CharacteristicResponse>>), statusCode: StatusCodes.Status200OK)]
        public async Task<IActionResult> GetData(GetCharacteristicQuery filter)
        {
            return Ok(await Mediator.Send(filter));
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(type: typeof(Response<CharacteristicResponse>), statusCode: StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new GetCharacteristicByIdQuery { Id = id }));
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateCharacteristicCommand command)
        {

            return Ok(await Mediator.Send(command));
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdateCharacteristicCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteCharacteristicCommand { Id = id }));
        }
    }
}
