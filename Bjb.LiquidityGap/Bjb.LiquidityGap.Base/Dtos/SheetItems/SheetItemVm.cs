using Bjb.LiquidityGap.Base.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bjb.LiquidityGap.Base.Dtos.SheetItems
{
    public class SheetItemVm
    {
        public List<SheetItemResponse> Results { get; set; }
        public PagedInfoRepositoryResponse Info { get; set; }
    }
}
