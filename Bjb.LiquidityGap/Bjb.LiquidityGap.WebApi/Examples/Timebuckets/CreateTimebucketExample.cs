using Bjb.LiquidityGap.Application.Features.TimeBuckets.Commands.Create;
using Bjb.LiquidityGap.Base.Dtos.TimeBuckets;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;

namespace Bjb.LiquidityGap.WebApi.Examples.TimeBuckets
{
    public class CreateTimeBucketExample : IExamplesProvider<CreateTimeBucketCommand>
    {
        public CreateTimeBucketCommand GetExamples()
        {
            return new CreateTimeBucketCommand
            {
                Code = "2M",
                Label = "Overnight",
                Sequence = 1,
                CharacteristicTimebuckets = new List<AddCharacteristicTimeBucketRequest>()
                {
                    new AddCharacteristicTimeBucketRequest
                    {
                    CharacteristicId = 1,
                    UsePercentage = true,
                    DayRange = 1,
                    Percentage = 13,
                    }
                }
            };
        }
    }
}
