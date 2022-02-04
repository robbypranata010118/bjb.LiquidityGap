using Bjb.LiquidityGap.Application.Features.Categories.Commands.Update;
using Swashbuckle.AspNetCore.Filters;

namespace Bjb.LiquidityGap.WebApi.Examples.Categories
{
    public class UpdateCategoryExample : IExamplesProvider<UpdateCategoryCommand>
    {
        public UpdateCategoryCommand GetExamples()
        {
            return new UpdateCategoryCommand()
            {
                Id = 1,
                Code = "001",
                Name = "Asset"
            };
        }
    }
}
