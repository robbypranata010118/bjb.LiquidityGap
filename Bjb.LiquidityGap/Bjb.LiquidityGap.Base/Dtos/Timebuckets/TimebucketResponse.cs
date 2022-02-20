using Bjb.LiquidityGap.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bjb.LiquidityGap.Base.Dtos.TimeBuckets
{
    public class TimeBucketResponse
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Label { get; set; }
        public string Sequence { get; set; }
        public ICollection<ChacteristicTimeBucketResponse> CharacteristicTimebuckets { get; set; }
        public string UserIn { get; set; }
        public DateTime DateIn { get; set; }
        public string UserUp { get; set; }
        public DateTime? DateUp { get; set; }
        public bool IsActive { get; set; }
    }
    public class ChacteristicTimeBucketResponse
    {
        public int Id { get; set; }
        public int CharacteristicId { get; set; }
        public bool UsePercentage { get; set; }
        public int DayRange { get; set; }
        public decimal Percentage { get; set; }
        public string UserIn { get; set; }
        public DateTime DateIn { get; set; }
        public string UserUp { get; set; }
        public DateTime? DateUp { get; set; }
        public bool IsActive { get; set; }
    }

}
