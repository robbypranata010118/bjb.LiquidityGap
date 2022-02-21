using Bjb.LiquidityGap.Application.Features.Characteristics.Commands.Create;
using Swashbuckle.AspNetCore.Filters;

namespace Bjb.LiquidityGap.WebApi.Examples.Characteristic
{
    public class CreateCharacteristicExample : IExamplesProvider<CreateCharacteristicCommand>
    {
        public CreateCharacteristicCommand GetExamples()
        {
            return new CreateCharacteristicCommand
            {
                Code = "BEHAV",
                Name = "Behavioral",
                Description = "Description",
                CalcDay = 5,
            };
        }
    }
}
