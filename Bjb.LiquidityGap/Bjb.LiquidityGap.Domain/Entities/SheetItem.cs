using Bjb.LiquidityGap.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bjb.LiquidityGap.Domain.Entities
{
    public class SheetItem : BaseEntity<int>, IAuditable, IDeactivable
    {
        [ForeignKey(nameof(SubCategory))]
        public int SubCategoryId { get; set; }

        [ForeignKey(nameof(DataSource))]
        public int? DataSourceId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        [ForeignKey(nameof(SheetItemParent))]
        public int? SheetItemParentId { get; set; }
        public bool MarkToCalculate { get; set; }
        public string Statement { get; set; }
        public bool IsManualInput { get; set; }
        [NotMapped]
        public string ModuleName { get; set; } = "Master Data";
        [NotMapped]
        public string FeatureName { get; set; } = "Sheet Item";
        public virtual SubCategory SubCategory { get; set; }
        public virtual DataSource DataSource { get; set; }
        [ForeignKey("SheetItemParentId")]
        public virtual ICollection<SheetItem> SheetChildItems { get; set; }
        public virtual ICollection<SheetItemCharacteristic> SheetItemCharacteristics { get; set; }
        public virtual ICollection<SheetItemTimebucket> SheetItemTimebuckets { get; set; }
        public virtual SheetItem SheetItemParent { get; set; }
    }
}
