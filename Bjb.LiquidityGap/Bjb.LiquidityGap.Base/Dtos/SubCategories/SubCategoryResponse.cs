using Bjb.LiquidityGap.Base.Dtos.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bjb.LiquidityGap.Base.Dtos.SubCategories
{
    public class SubCategoryResponse
    {
        public string Id { get; set; }
        public CategoryResponse Category { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string UserIn { get; set; }
        public DateTime DateIn { get; set; }
        public string UserUp { get; set; }
        public DateTime? DateUp { get; set; }
        public bool IsActive { get; set; }


    }
}
