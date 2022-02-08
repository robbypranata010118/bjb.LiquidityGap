using Bjb.LiquidityGap.Application.Features.Timebuckets.Commands.Update;
using Swashbuckle.AspNetCore.Filters;

namespace Bjb.LiquidityGap.WebApi.Examples.Timebuckets
{
    public class UpdateTimebucketExample : IExamplesProvider<UpdateTimebucketCommand>
    {
        public UpdateTimebucketCommand GetExamples()
        {
            return new UpdateTimebucketCommand()
            {
                Id = 1,
                Code = "2M",
                Label = "Overnight",
                Sequence = 1
            };
        }
    }
}
