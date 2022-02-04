using Bjb.LiquidityGap.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bjb.LiquidityGap.Domain.Entities
{
    public class Timebucket : BaseEntity<int>, IAuditable, IDeactivable
    {
        public string Code { get; set; }
        public string Label { get; set; }
        public int Sequence { get; set; }
        [NotMapped]
        public string ModuleName { get; set; } = "Master Data";
        [NotMapped]
        public string FeatureName { get; set; } = "Timebucket";
        public virtual ICollection<CharacteristicTimebucket> CharacteristicTimebuckets { get; set; }
        public virtual ICollection<LiquidityGapBucket> LiquidityGapBuckets { get; set; }
    }
}
