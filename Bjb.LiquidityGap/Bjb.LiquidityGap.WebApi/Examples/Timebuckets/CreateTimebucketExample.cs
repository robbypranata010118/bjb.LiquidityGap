using Bjb.LiquidityGap.Application.Features.Timebuckets.Commands.Create;
using Swashbuckle.AspNetCore.Filters;

namespace Bjb.LiquidityGap.WebApi.Examples.Timebuckets
{
    public class CreateTimebucketExample : IExamplesProvider<CreateTimebucketCommand>
    {
        public CreateTimebucketCommand GetExamples()
        {
            return new CreateTimebucketCommand
            {
                Code = "2M",
                Label = "Overnight",
                Sequence = 1
            };
        }
    }
}
