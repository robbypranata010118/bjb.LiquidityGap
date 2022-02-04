using Bjb.LiquidityGap.Application.Features.SubCategories.Commands.Create;
using Bjb.LiquidityGap.Application.Features.SubCategories.Commands.Delete;
using Bjb.LiquidityGap.Application.Features.SubCategories.Commands.Update;
using Bjb.LiquidityGap.Application.Features.SubCategories.Quries.Get;
using Bjb.LiquidityGap.Application.Features.SubCategories.Quries.GetById;
using Bjb.LiquidityGap.Base.Dtos.SubCategories;
using Bjb.LiquidityGap.Base.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bjb.LiquidityGap.WebApi.Controllers
{
    public class SubCategoryController : BaseApiController
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
        [ProducesResponseType(type: typeof(PagedResponse<IEnumerable<SubCategoryResponse>>), statusCode: StatusCodes.Status200OK)]
        public async Task<IActionResult> GetData(GetSubCategoryQuery filter)
        {
            return Ok(await Mediator.Send(filter));
        }
        
        [HttpGet("{id:int}")]
        [ProducesResponseType(type: typeof(Response<SubCategoryResponse>), statusCode: StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new GetSubCategoryByIdQuery { Id = id }));
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateSubCategoryCommand command)
        {

            return Ok(await Mediator.Send(command));
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdateSubCategoryCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteSubCategoryCommand { Id = id }));
        }
    }
}
