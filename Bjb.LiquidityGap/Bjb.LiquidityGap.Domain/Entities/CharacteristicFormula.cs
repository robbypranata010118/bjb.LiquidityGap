using Bjb.LiquidityGap.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bjb.LiquidityGap.Domain.Entities
{
    public class CharacteristicFormula : BaseEntity<int>, IAuditable, IDeactivable
    {
        [ForeignKey(nameof(Characteristic))]
        public int CharacteristicId { get; set; }
        public string Name { get; set; }
        public string Formula { get; set; }
        public int Sequence { get; set; }
        [NotMapped]
        public string ModuleName { get; set; } = "Master Data";
        [NotMapped]
        public string FeatureName { get; set; } = "Characteristic Formula";
        public virtual Characteristic Characteristic { get; set; }
    }
}
