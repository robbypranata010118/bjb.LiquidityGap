using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bjb.LiquidityGap.Base.Interfaces
{
    public interface ICurrentUserService
    {
        public string UserId { get; }
        public string UserName { get; }
        public string IdFungsi { get; set; }
    }
}
