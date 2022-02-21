using Bjb.LiquidityGap.Application.Features.Characteristics.Commands.Update;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;

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
                Description = "Description",
                Formula = new List<Base.Dtos.Characteristics.AddCharacteristicFormula>()
                {
                    new Base.Dtos.Characteristics.AddCharacteristicFormula
                    {
                        Name = "Formula 1",
                        Formula = "A+B",
                        Sequence = 1
                    }
                }
            };
        }
    }
}
