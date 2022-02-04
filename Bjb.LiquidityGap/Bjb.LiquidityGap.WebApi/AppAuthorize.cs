using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bjb.LiquidityGap.WebApi
{
    public class AppAuthorize : AuthorizeAttribute
    {
        public string Permissions { get; set; }
        public string Feature { get; set; }
    }
}
