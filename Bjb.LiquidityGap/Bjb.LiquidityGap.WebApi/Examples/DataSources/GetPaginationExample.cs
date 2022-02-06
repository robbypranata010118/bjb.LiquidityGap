using Bjb.LiquidityGap.Application.Features.DataSources.Queries.Get;
using Bjb.LiquidityGap.Base.Parameters;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;

namespace Bjb.LiquidityGap.WebApi.Examples.DataSources
{
    public class GetPaginationExample : IExamplesProvider<GetDataSourceQuery>
    {
        public GetDataSourceQuery GetExamples()
        {
            List<RequestFilterParameter> filterParameters = new List<RequestFilterParameter>();
            filterParameters.Add(new RequestFilterParameter
            {
                Field = "Name",
                ComparisonOperator = "like",
                Type = "string",
                Value = "dwh"
            });
            return new GetDataSourceQuery
            {
                Page = 1,
                Length = 10,
                Filters = filterParameters,
                Orders = new List<string> { "Name" },
                SortType = "ASC"
            };
        }
    }
}
