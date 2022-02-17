using Bjb.LiquidityGap.Base.Dtos.SheetItems;
using Bjb.LiquidityGap.Domain.Entities;
using System.Threading.Tasks;

namespace Bjb.LiquidityGap.Base.Interfaces
{
    public interface ISheetItem
    {
        Task<int> CreateSheetItem(AddSheetItemRequest sheetItem);
        Task<int> EditSheetItem(UpdateSheetItemRequest sheetItem);
    }
}
