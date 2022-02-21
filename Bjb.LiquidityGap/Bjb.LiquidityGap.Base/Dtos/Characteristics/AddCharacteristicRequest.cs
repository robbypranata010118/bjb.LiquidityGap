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
        public ICollection<AddCharacteristicFormula> Formula { get; set; }
    }

    public class AddCharacteristicFormula
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Formula { get; set; }
        public int Sequence { get; set; }
        //public string UserIn { get; set; }
        //public DateTime DateIn { get; set; }
        //public string UserUp { get; set; }
        //public DateTime? DateUp { get; set; }
        //public bool IsActive { get; set; }
    }
}
