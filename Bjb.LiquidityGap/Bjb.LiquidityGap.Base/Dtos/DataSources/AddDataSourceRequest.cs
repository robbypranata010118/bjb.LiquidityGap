using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bjb.LiquidityGap.Base.Dtos.DataSources
{
    public  class AddDataSourceRequest
    {
        public string Name { get; set; }
        public string ConnString { get; set; }
        public bool UseEtl { get; set; }
    }
}
