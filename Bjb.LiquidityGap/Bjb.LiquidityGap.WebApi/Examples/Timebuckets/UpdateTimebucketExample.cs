using Bjb.LiquidityGap.Application.Features.TimeBuckets.Commands.Update;
using Bjb.LiquidityGap.Base.Dtos.TimeBuckets;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;

namespace Bjb.LiquidityGap.WebApi.Examples.TimeBuckets
{
    public class UpdateTimeBucketExample : IExamplesProvider<UpdateTimeBucketCommand>
    {
        public UpdateTimeBucketCommand GetExamples()
        {
            return new UpdateTimeBucketCommand()
            {
                Id = 1,
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
