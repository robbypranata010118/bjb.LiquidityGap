using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bjb.LiquidityGap.Base.Dtos.DataSources
{
    public class UpdateDataSourceRequest : AddDataSourceRequest
    {
        public int Id { get; set; }
    }
}
