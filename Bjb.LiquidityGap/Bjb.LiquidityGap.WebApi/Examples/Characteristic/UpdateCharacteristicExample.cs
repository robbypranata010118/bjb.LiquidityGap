using Bjb.LiquidityGap.Application.Features.Characteristics.Commands.Update;
using Swashbuckle.AspNetCore.Filters;

namespace Bjb.LiquidityGap.WebApi.Examples.Characteristic
{
    public class UpdateCharacteristicExample : IExamplesProvider<UpdateCharacteristicCommand>
    {
        public UpdateCharacteristicCommand GetExamples()
        {
            return new UpdateCharacteristicCommand()
            {
                Id = 1,
                Code = "BEHAV",
                Name = "Behavioral",
                Description = "Description"
            };
        }
    }
}
