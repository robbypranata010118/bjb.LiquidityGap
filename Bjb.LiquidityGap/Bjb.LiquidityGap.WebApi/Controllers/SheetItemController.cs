using Bjb.LiquidityGap.Application.Features.SheetItems.Commands.Create;
using Bjb.LiquidityGap.Application.Features.SheetItems.Commands.Delete;
using Bjb.LiquidityGap.Application.Features.SheetItems.Commands.Update;
using Bjb.LiquidityGap.Application.Features.SheetItems.Queries.Get;
using Bjb.LiquidityGap.Application.Features.SheetItems.Queries.GetById;
using Bjb.LiquidityGap.Base.Dtos.SheetItems;
using Bjb.LiquidityGap.Base.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bjb.LiquidityGap.WebApi.Controllers
{
    public class SheetItemController : BaseApiController
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
        [ProducesResponseType(type: typeof(PagedResponse<IEnumerable<SheetItemResponse>>), statusCode: StatusCodes.Status200OK)]
        public async Task<IActionResult> GetData(GetSheetItemQuery filter)
        {
            return Ok(await Mediator.Send(filter));
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(type: typeof(Response<SheetItemResponse>), statusCode: StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new GetSheetItemByIdQuery { Id = id }));
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateSheetItemCommand command)
        {

            return Ok(await Mediator.Send(command));
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdateSheetItemCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteSheetItemCommand { Id = id }));
        }
    }
}
