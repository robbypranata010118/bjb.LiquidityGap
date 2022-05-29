using Bjb.LiquidityGap.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bjb.LiquidityGap.Domain.Entities
{
    public class SummarySource : BaseEntity<int>, IAuditable, IDeactivable
    {
        [MaxLength(10)]
        public string SourceData { get; set; }
        public DateTime? BussDate { get; set; }
        [MaxLength(10)]
        public string Sandi { get; set; }
        [Column(TypeName = "decimal(18,0)")]
        public decimal Nominal { get; set; }
        public DateTime? MaturityDate { get; set; }
        [NotMapped]
        public string ModuleName { get; set; } = "Master Data";
        [NotMapped]
        public string FeatureName { get; set; } = "Summary Source";
    }
}
