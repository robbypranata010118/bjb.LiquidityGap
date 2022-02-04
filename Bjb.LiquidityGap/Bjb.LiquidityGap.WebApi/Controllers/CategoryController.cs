using Bjb.LiquidityGap.Application.Features.Categories.Commands.Create;
using Bjb.LiquidityGap.Application.Features.Categories.Commands.Delete;
using Bjb.LiquidityGap.Application.Features.Categories.Commands.Update;
using Bjb.LiquidityGap.Application.Features.Categories.Queries.Get;
using Bjb.LiquidityGap.Application.Features.Categories.Queries.GetById;
using Bjb.LiquidityGap.Base.Dtos.Categories;
using Bjb.LiquidityGap.Base.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bjb.LiquidityGap.WebApi.Controllers
{
    public class CategoryController : BaseApiController
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
        [ProducesResponseType(type: typeof(PagedResponse<IEnumerable<CategoryResponse>>), statusCode: StatusCodes.Status200OK)]
        public async Task<IActionResult> GetData(GetCategoryQuery filter)
        {
            return Ok(await Mediator.Send(filter));
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(type: typeof(Response<CategoryResponse>), statusCode: StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new GetCategoryByIdQuery { Id = id }));
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateCategoryCommand command)
        {

            return Ok(await Mediator.Send(command));
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdateCategoryCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteCategoryCommand { Id = id }));
        }
    }
}
