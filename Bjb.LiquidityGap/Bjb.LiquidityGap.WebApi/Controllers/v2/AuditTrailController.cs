using Bjb.LiquidityGap.Application.Features.AuditTrails.Queries.Get;
using Bjb.LiquidityGap.Application.Features.AuditTrails.Queries.GetById;
using Bjb.LiquidityGap.Base.Dtos.AuditTrails;
using Bjb.LiquidityGap.Base.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bjb.LiquidityGap.WebApi.Controllers.v2
{
    [Authorize]
    public class AuditTrailController : BaseApiController
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
        ///       Module,
        ///       Feature,
        ///       UserId,
        ///       RoleId,
        ///       RoleName,
        ///       Action,
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
        [ProducesResponseType(type: typeof(PagedResponse<IEnumerable<AuditTrailResponse>>), statusCode: StatusCodes.Status200OK)]
        public async Task<IActionResult> GetData(GetAuditTrailQuery filter)
        {
            return Ok(await Mediator.Send(filter));
        }

        [HttpGet("{id:Guid}")]
        [ProducesResponseType(type: typeof(Response<AuditTrailResponse>), statusCode: StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await Mediator.Send(new GetAuditTrailByIdQuery { Id = id }));
        }
    }
}
