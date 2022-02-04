using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bjb.LiquidityGap.Base.Dtos.SubCategories
{
    public class AddSubCategoryRequest
    {
        public int CategoryId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
