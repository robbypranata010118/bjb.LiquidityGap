using Bjb.LiquidityGap.Base.Dtos.TimeBuckets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bjb.LiquidityGap.Base.Dtos.SheetitemTimebucket
{
    public class SheetItemTimebucketResponse
    {
        public int Id { get; set; }
        public TimeBucketResponse Timebucket { get; set; }
    }
}
