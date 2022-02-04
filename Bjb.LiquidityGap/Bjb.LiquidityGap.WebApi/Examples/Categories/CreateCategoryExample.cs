using Bjb.LiquidityGap.Application.Features.Categories.Commands.Create;
using Swashbuckle.AspNetCore.Filters;

namespace Bjb.LiquidityGap.WebApi.Examples.Categories
{
    public class CreateCategoryExample : IExamplesProvider<CreateCategoryCommand>
    {
        public CreateCategoryCommand GetExamples()
        {
            return new CreateCategoryCommand
            {
                Code = "001",
                Name = "Asset"
            };
        }
    }
}
