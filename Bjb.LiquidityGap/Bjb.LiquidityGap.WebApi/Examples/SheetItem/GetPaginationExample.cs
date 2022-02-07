using Bjb.LiquidityGap.Application.Features.SheetItems.Queries.Get;
using Bjb.LiquidityGap.Base.Parameters;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;

namespace Bjb.LiquidityGap.WebApi.Examples.SheetItem
{
    public class GetPaginationExample : IExamplesProvider<GetSheetItemQuery>
    {
        public GetSheetItemQuery GetExamples()
        {
            List<RequestFilterParameter> filterParameters = new List<RequestFilterParameter>();
            filterParameters.Add(new RequestFilterParameter
            {
                Field = "Name",
                ComparisonOperator = "like",
                Type = "string",
                Value = "Pos Akun 1"
            });
            return new GetSheetItemQuery
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
