using Bjb.LiquidityGap.Application.Features.SubCategories.Quries.Get;
using Bjb.LiquidityGap.Base.Parameters;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;

namespace Bjb.LiquidityGap.WebApi.Examples.SubCagories
{
    public class GetPaginationExample : IExamplesProvider<GetSubCategoryQuery>
    {
        public GetSubCategoryQuery GetExamples()
        {
            List<RequestFilterParameter> filterParameters = new List<RequestFilterParameter>();
            filterParameters.Add(new RequestFilterParameter
            {
                Field = "Name",
                ComparisonOperator = "like",
                Type = "string",
                Value = "a"
            });
            return new GetSubCategoryQuery
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
