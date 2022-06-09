using Bjb.LiquidityGap.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public string FeatureName { get; set; } = "Sheet Item";
        public virtual SheetItem SheetItem { get; set; }
        public virtual Timebucket Timebucket { get; set; }
    }
}
