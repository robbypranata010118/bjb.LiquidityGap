using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bjb.LiquidityGap.Base.Dtos.Timebuckets
{
    public class UpdateTimebucketRequest : AddTimebucketRequest
    {
        public int Id { get; set; }
    }
}
