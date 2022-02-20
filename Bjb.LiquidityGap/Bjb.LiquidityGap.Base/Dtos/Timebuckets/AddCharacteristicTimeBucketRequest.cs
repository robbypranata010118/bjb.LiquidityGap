using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bjb.LiquidityGap.Base.Dtos.TimeBuckets
{
    public class AddCharacteristicTimeBucketRequest
    {
        public int CharacteristicId { get; set; }
        public bool UsePercentage { get; set; }
        public int DayRange { get; set; }
        public decimal Percentage { get; set; }
    }
}
