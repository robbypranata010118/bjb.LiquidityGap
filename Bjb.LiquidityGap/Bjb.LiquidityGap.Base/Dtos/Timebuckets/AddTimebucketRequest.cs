using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bjb.LiquidityGap.Base.Dtos.Timebuckets
{
    public class AddTimebucketRequest
    {
        public string Code { get; set; }
        public string Label { get; set; }
        public int Sequence { get; set; }
    }
}
