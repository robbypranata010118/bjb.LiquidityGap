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
    public class Characteristic : BaseEntity<int>, IAuditable, IDeactivable
    {
        [MaxLength(10)]
        public string Code { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(150)]
        public string Description { get; set; }
        [Required]
        public int CalcDay { get; set; }
        [NotMapped]
        public string ModuleName { get; set; } = "Master Data";
        [NotMapped]
        public string FeatureName { get; set; } = "Characteristic";
        public virtual ICollection<SheetItemCharacteristic> SheetItemCharacteristics { get; set; }
        public virtual ICollection<CharacteristicFormula> CharacteristicFormulas { get; set; }
        public virtual ICollection<CharacteristicTimebucket> characteristicTimebuckets { get; set; }
    }
}
