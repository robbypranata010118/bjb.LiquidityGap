using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bjb.LiquidityGap.Base.Dtos.Timebuckets
{
    public class TimebucketResponse
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Label { get; set; }
        public string Sequence { get; set; }
        public string UserIn { get; set; }
        public DateTime DateIn { get; set; }
        public string UserUp { get; set; }
        public DateTime? DateUp { get; set; }
        public bool IsActive { get; set; }
    }
}
