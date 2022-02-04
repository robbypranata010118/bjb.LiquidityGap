using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bjb.LiquidityGap.Base.Dtos.SubCategories
{
    public class UpdateSubCategoryRequest : AddSubCategoryRequest
    {
        public int Id { get; set; }
    }
}
