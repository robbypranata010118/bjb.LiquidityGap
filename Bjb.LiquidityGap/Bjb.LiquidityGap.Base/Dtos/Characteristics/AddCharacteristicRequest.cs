using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bjb.LiquidityGap.Base.Dtos.Characteristics
{
    public class AddCharacteristicRequest
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CalcDay { get; set; }
    }
}
