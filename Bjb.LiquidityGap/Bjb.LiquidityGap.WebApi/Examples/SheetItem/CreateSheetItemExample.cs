using Bjb.LiquidityGap.Application.Features.SheetItems.Commands.Create;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;

namespace Bjb.LiquidityGap.WebApi.Examples.SheetItem
{
    public class CreateSheetItemExample : IExamplesProvider<CreateSheetItemCommand>
    {
        public CreateSheetItemCommand GetExamples()
        {
            return new CreateSheetItemCommand
            {
                Name = "DWH",
                Code = "C1",
                SubCategoryId = 1,
                DataSourceId = 1,
                SheetItemParentId = 0,
                MarkToCalculate = true,
                Statement = "ya",
                IsManualInput = true,
                SheetItemCharacteristics = new List<int> {1,2},
            };
        }
    }
}
