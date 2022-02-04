using Bjb.LiquidityGap.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bjb.LiquidityGap.Domain.Entities
{
    public class LiquidityGap : BaseEntity<int>, IAuditable, IDeactivable
    {
        public DateTime BussinessDate { get; set; }
        [ForeignKey(nameof(SheetItem))]
        public int SheetItemId { get; set; }
        public string Currency { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Nominal { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal ScenarioNominal { get; set; }
        [NotMapped]
        public string ModuleName { get; set; } = "Transactional";
        [NotMapped]
        public string FeatureName { get; set; } = "Liquidity Gap";
        public virtual SheetItem SheetItem { get; set; }
        public virtual ICollection<LiquidityGapBucket> LiquidityGapBuckets { get; set; }
    }
}
