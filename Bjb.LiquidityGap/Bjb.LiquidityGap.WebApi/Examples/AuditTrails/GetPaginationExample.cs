using Bjb.LiquidityGap.Application.Features.AuditTrails.Queries.Get;
using Bjb.LiquidityGap.Base.Parameters;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;

namespace Bjb.LiquidityGap.WebApi.Examples.AuditTrails
{
    public class GetPaginationExample : IExamplesProvider<GetAuditTrailQuery>
    {
        public GetAuditTrailQuery GetExamples()
        {
            List<RequestFilterParameter> filterParameters = new List<RequestFilterParameter>();
            filterParameters.Add(new RequestFilterParameter
            {
                Field = "Feature",
                ComparisonOperator = "like",
                Type = "string",
                Value = "a"
            });
            return new GetAuditTrailQuery
            {
                Page = 1,
                Length = 10,
                Filters = filterParameters,
                Orders = new List<string> { "Feature" },
                SortType = "ASC"
            };
        }
    }
}
