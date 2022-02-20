using Bjb.LiquidityGap.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bjb.LiquidityGap.Domain.Entities
{
    public class CharacteristicTimebucket : BaseEntity<int>, IAuditable, IDeactivable
    {
        [ForeignKey(nameof(Characteristic))]
        public int CharacteristicId { get; set; }
        [ForeignKey(nameof(Timebucket))]
        public int TimebucketId { get; set; }
        public bool UsePercentage { get; set; }
        public int DayRange { get; set; }
        public decimal Percentage { get; set; }
        [NotMapped]
        public string ModuleName { get; set; } = "Master Data";
        [NotMapped]
        public string FeatureName { get; set; } = "Characteristic Timebucket";
        public virtual Characteristic Characteristic { get; set; }
        public virtual Timebucket Timebucket { get; set; }
    }
}
