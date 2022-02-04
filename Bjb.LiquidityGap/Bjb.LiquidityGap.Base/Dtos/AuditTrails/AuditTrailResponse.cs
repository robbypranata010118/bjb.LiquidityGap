using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bjb.LiquidityGap.Base.Dtos.AuditTrails
{
    public class AuditTrailResponse
    {
        public Guid Id { get; set; }
        public DateTime LogDate { get; set; }
        public string Module { get; set; }
        public string Feature { get; set; }
        public string Action { get; set; }
        public string ReferenceId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public string ApplicationName { get; set; }
        public string Message { get; set; }
        public string Detail { get; set; }
    }
}
