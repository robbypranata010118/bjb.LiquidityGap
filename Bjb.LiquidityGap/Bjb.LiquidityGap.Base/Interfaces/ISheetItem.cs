using Bjb.LiquidityGap.Base.Dtos.SheetItems;
using Bjb.LiquidityGap.Base.Parameters;
using Bjb.LiquidityGap.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bjb.LiquidityGap.Base.Interfaces
{
    public interface ISheetItem
    {
        Task<SheetItemVm> GetSheetItem(RequestParameter request);
        Task<int> CreateSheetItem(AddSheetItemRequest sheetItem);
        Task<int> EditSheetItem(UpdateSheetItemRequest sheetItem);
    }
}
