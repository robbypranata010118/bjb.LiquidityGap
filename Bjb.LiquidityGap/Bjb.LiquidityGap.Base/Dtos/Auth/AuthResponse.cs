using Bjb.LiquidityGap.Base.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bjb.LiquidityGap.Base.Dtos.Auth
{
    public class AuthResponse
    {
        public AuthResponse()
        {
            TokenScheme = CommonConstants.TOKEN_AUTHORIZATION_SCHEME;
            Expired = DateTime.Now.AddMinutes(CommonConstants.TOKEN_EXPIRED);
            TokenLifeTime = CommonConstants.TOKEN_EXPIRED;
        }
        public string Token { get; set; }
        public string DefaultFeature { get; set; }
        public string TokenScheme { get; }
        public int TokenLifeTime { get; set; }
        public DateTime Expired { get; set; }
    }
}
