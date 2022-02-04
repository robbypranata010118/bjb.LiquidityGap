using Bjb.LiquidityGap.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bjb.LiquidityGap.Domain.Entities
{
    public class LiquidityGapBucket : BaseEntity<int>, IAuditable, IDeactivable
    {
        [ForeignKey(nameof(LiquidityGap))]
        public int LiquidityGapId { get; set; }
        [ForeignKey(nameof(Timebucket))]
        public int TimeBucketId { get; set; }
        public float ActualPercentage { get; set; }
        public string ActualCalc { get; set; }
        public string ScenarioCalc { get; set; }
        public float ScenarioPercentage { get; set; }
        [NotMapped]
        public string ModuleName { get; set; } = "Transactional";
        [NotMapped]
        public string FeatureName { get; set; } = "Liquidity Gap Bucket";
        public virtual LiquidityGap LiquidityGap { get; set; }
        public virtual Timebucket Timebucket { get; set; }
    }
}
