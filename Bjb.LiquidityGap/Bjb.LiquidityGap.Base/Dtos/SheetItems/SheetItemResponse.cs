using Bjb.LiquidityGap.Base.Dtos.Characteristics;
using Bjb.LiquidityGap.Base.Dtos.DataSources;
using Bjb.LiquidityGap.Base.Dtos.SheetItemCharacteriastic;
using Bjb.LiquidityGap.Base.Dtos.SheetitemTimebucket;
using Bjb.LiquidityGap.Base.Dtos.SubCategories;
using Bjb.LiquidityGap.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bjb.LiquidityGap.Base.Dtos.SheetItems
{
    public class SheetItemResponse
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool MarkToCalculate { get; set; }
        public string Statement { get; set; }
        public bool IsManualInput { get; set; }
        public SheetItemSimpleResponse SheetItemParent { get; set; }
        public SubCategoryResponse SubCategory { get; set;   }
        public DataSourceResponse DataSource { get; set; }
        public List<SheetItemCharacteristicResponse> SheetItemCharacteristics { get; set; }
        public List<SheetItemTimebucketResponse> SheetItemTimebuckets { get; set; }
        public string Characteristics { get; set; }
        public List<SheetItemSimpleResponse> SheetChildItems { get; set; }
        public string UserIn { get; set; }
        public DateTime DateIn { get; set; }
        public string UserUp { get; set; }
        public DateTime? DateUp { get; set; }
        public bool IsActive { get; set; }
    }
}
