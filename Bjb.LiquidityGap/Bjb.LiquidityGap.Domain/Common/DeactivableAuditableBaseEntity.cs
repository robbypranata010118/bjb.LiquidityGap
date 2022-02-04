using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bjb.LiquidityGap.Domain.Common
{
    public partial class DeactivableAuditableBaseEntity<T> : AuditableBaseEntity<T>, IDeactivable
    {
        [Required]
        public bool IsActive { get; set; } = true;
    }
}
