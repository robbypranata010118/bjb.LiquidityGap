using Bjb.LiquidityGap.Application.Features.Timebuckets.Queries.Get;
using Bjb.LiquidityGap.Base.Parameters;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;

namespace Bjb.LiquidityGap.WebApi.Examples.Timebuckets
{
    public class GetPaginationExample : IExamplesProvider<GetTimebucketQuery>
    {
        public GetTimebucketQuery GetExamples()
        {
            List<RequestFilterParameter> filterParameters = new List<RequestFilterParameter>();
            filterParameters.Add(new RequestFilterParameter
            {
                Field = "Code",
                ComparisonOperator = "like",
                Type = "string",
                Value = "2M"
            });
            return new GetTimebucketQuery
            {
                Page = 1,
                Length = 10,
                Filters = filterParameters,
                Orders = new List<string> { "Code" },
                SortType = "ASC"
            };
        }
    }
}
