using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bjb.LiquidityGap.Base.Dtos.DataSources
{
    public class DataSourceResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ConnString { get; set; }
        public bool UseEtl { get; set; }
        public string UserIn { get; set; }
        public DateTime DateIn { get; set; }
        public string UserUp { get; set; }
        public DateTime? DateUp { get; set; }
        public bool IsActive { get; set; }
    }
}
