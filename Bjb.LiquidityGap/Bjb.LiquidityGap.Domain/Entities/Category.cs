using Bjb.LiquidityGap.Domain.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bjb.LiquidityGap.Domain.Entities
{
    public class Category : BaseEntity<int>, IAuditable, IDeactivable
    {
        [MaxLength(10)]
        public string Code { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [NotMapped]
        public string ModuleName { get; set; } = "Master Data";
        [NotMapped]
        public string FeatureName { get; set; } = "Sub Category";
        public virtual ICollection<SubCategory> SubCategories { get; set; }
    }
}
