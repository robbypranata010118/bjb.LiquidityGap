using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bjb.LiquidityGap.Base.Dtos.SheetItems
{
    public class UpdateSheetItemRequest : AddSheetItemRequest
    {
        public int Id { get; set; }
    }
}
