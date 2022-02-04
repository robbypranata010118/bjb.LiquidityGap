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
    public class Currency : BaseEntity<int>, IAuditable, IDeactivable
    {
        [MaxLength(3)]
        public string Code { get; set; }
        [MaxLength(150)]
        public string Name { get; set; }
        [Column(TypeName = "decimal(18,0)")]
        public decimal Rate { get; set; }
        [NotMapped]
        public string ModuleName { get; set; } = "Master Data";
        [NotMapped]
        public string FeatureName { get; set; } = "Currency";
    }
}
