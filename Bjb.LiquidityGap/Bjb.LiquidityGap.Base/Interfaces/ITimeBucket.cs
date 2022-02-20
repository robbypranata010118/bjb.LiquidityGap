using Bjb.LiquidityGap.Base.Dtos.TimeBuckets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bjb.LiquidityGap.Base.Interfaces
{
    public interface ITimeBucket
    {
        Task<int> CreateTimeBucket(AddTimeBucketRequest timeBucket);
        Task<int> EditTimeBucket(UpdateTimeBucketRequest timeBucket);

    }
}
