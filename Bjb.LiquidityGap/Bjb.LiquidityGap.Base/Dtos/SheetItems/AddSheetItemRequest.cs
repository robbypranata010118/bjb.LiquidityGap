using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bjb.LiquidityGap.Base.Dtos.SheetItems
{
    public class AddSheetItemRequest
    {
        public int SubCategoryId { get; set; }
        public int? DataSourceId { get; set; }
        public int? SheetItemParentId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool MarkToCalculate { get; set; }
        public string Statement { get; set; }
        public bool IsManualInput { get; set; }
        public List<int> SheetItemCharacteristics { get; set; }
        public List<int> SheetItemTimebuckets { get; set; }

    }
}
