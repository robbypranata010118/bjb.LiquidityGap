using Bjb.LiquidityGap.Application.Features.SheetItems.Commands.Update;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;

namespace Bjb.LiquidityGap.WebApi.Examples.SheetItem
{
    public class UpdateDataSourceExample : IExamplesProvider<UpdateSheetItemCommand>
    {
        public UpdateSheetItemCommand GetExamples()
        {
            return new UpdateSheetItemCommand()
            {
                Id = 1,
                SubCategoryId = 1,
                DataSourceId = 1,
                Code = "111000",
                Name = "Pos Akun 1",
                SheetItemParentId = null,
                MarkToCalculate = true,
                Statement = "ya",
                IsManualInput = true,
                SheetItemCharacteristics = new List<int> { 1, 2 },
                SheetItemTimebuckets = new List<int> { 1, 2 },

            };
        }
    }
}
