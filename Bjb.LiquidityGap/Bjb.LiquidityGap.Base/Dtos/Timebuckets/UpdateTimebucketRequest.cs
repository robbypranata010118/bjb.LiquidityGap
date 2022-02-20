using Bjb.LiquidityGap.Base.Dtos.TimeBuckets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bjb.LiquidityGap.Base.Dtos.TimeBuckets
{
    public class UpdateTimeBucketRequest : AddTimeBucketRequest
    {
        public int Id { get; set; }
    }
}
