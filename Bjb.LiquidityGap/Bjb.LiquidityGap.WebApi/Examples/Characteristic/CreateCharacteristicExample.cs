using Bjb.LiquidityGap.Application.Features.Characteristics.Commands.Create;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;

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
