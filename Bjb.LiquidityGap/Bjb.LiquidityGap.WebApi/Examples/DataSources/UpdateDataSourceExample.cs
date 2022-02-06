using Bjb.LiquidityGap.Application.Features.DataSources.Commands.Update;
using Swashbuckle.AspNetCore.Filters;

namespace Bjb.LiquidityGap.WebApi.Examples.DataSources
{
    public class UpdateDataSourceExample : IExamplesProvider<UpdateDataSourceCommand>
    {
        public UpdateDataSourceCommand GetExamples()
        {
            return new UpdateDataSourceCommand()
            {
                Id = 1,
                Name = "MCB",
                ConnString = "mssql://sa:SQL4dmin@10.6.225.38:1433/MCB",
                UseEtl = true
            };
        }
    }
}
