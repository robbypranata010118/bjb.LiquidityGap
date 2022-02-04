using Bjb.LiquidityGap.Application.Features.Categories.Commands.Update;
using Bjb.LiquidityGap.Application.Features.SubCategories.Commands.Update;
using Swashbuckle.AspNetCore.Filters;

namespace Bjb.LiquidityGap.WebApi.Examples.SubCagories
{
    public class UpdateSubCategoryExample : IExamplesProvider<UpdateSubCategoryCommand>
    {
        public UpdateSubCategoryCommand GetExamples()
        {
            return new UpdateSubCategoryCommand()
            {
                Id = 1,
                CategoryId = 1,
                Code = "001",
                Name = "Product"
            };
        }
    }
}
