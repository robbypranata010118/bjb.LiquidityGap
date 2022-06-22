using Bjb.LiquidityGap.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bjb.LiquidityGap.Domain.Entities
{
    public class SheetItemTimebucket : BaseEntity<int>, IAuditable, IDeactivable
    {
        [ForeignKey(nameof(SheetItem))]
        public int SheetItemId { get; set; }
        [ForeignKey(nameof(Timebucket))]
        public int TimebucketId { get; set; }
        [NotMapped]
        public string ModuleName { get; set; } = "Master Data";
        [NotMapped]
        public string FeatureName { get; set; } = "Sheet Item Timebucket";
        public virtual SheetItem SheetItem { get; set; }
        public virtual Timebucket Timebucket { get; set; }
    }
}
