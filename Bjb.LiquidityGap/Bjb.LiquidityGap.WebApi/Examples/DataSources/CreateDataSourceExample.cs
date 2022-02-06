using Bjb.LiquidityGap.Application.Features.DataSources.Commands.Create;
using Swashbuckle.AspNetCore.Filters;

namespace Bjb.LiquidityGap.WebApi.Examples.DataSources
{
    public class CreateDataSourceExample : IExamplesProvider<CreateDataSourceCommand>
    {
        public CreateDataSourceCommand GetExamples()
        {
            return new CreateDataSourceCommand
            {
                Name = "DWH",
                ConnString = "mssql://sa:SQL4dmin@10.6.225.38:1433/DWH",
                UseEtl = true
            };
        }
    }
}
