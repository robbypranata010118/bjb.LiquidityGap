using Bjb.LiquidityGap.Base.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Bjb.LiquidityGap.Application
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue("UserId");
            UserName = httpContextAccessor.HttpContext?.User?.FindFirstValue("Username");
            IdFungsi = httpContextAccessor.HttpContext?.User?.FindFirstValue("IdFungsi");
        }
        public string UserId { get; }
        public string UserName { get; }
        public string IdFungsi { get; set; }
    }
}
