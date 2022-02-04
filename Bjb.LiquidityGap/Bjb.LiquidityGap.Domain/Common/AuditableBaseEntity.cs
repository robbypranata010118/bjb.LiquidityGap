using System;
using System.ComponentModel.DataAnnotations;

namespace Bjb.LiquidityGap.Domain.Common
{
    public partial class AuditableBaseEntity<T>
    {
        [Required]
        public DateTime CreatedOn { get; set; }
        [Required]
        public T CreatedBy { get; set; }
        [Required]
        [MaxLength(200)]
        public string CreatedByName { get; set; }
    }
}
