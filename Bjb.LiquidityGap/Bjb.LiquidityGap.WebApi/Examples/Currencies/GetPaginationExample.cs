using Bjb.LiquidityGap.Application.Features.Currencies.Queries.Get;
using Bjb.LiquidityGap.Base.Parameters;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;

namespace Bjb.LiquidityGap.WebApi.Examples.Currencies
{
    public class GetPaginationExample : IExamplesProvider<GetCurrencyQuery>
    {
        public GetCurrencyQuery GetExamples()
        {
            List<RequestFilterParameter> filterParameters = new List<RequestFilterParameter>();
            filterParameters.Add(new RequestFilterParameter
            {
                Field = "Name",
                ComparisonOperator = "like",
                Type = "string",
                Value = "Rupiah"
            });
            return new GetCurrencyQuery
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
