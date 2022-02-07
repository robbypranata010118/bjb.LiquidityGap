using Bjb.LiquidityGap.Application.Features.Characteristics.Queries.Get;
using Bjb.LiquidityGap.Base.Parameters;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;

namespace Bjb.LiquidityGap.WebApi.Examples.Characteristic
{
    public class GetPaginationExample : IExamplesProvider<GetCharacteristicQuery>
    {
        public GetCharacteristicQuery GetExamples()
        {
            List<RequestFilterParameter> filterParameters = new List<RequestFilterParameter>();
            filterParameters.Add(new RequestFilterParameter
            {
                Field = "Name",
                ComparisonOperator = "like",
                Type = "string",
                Value = "a"
            });
            return new GetCharacteristicQuery
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
