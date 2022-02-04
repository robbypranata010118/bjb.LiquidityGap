using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bjb.LiquidityGap.Base.Dtos.Categories
{
    public class UpdateCategoryRequest : AddCategoryRequest
    {
        public int Id { get; set; }
    }
}
