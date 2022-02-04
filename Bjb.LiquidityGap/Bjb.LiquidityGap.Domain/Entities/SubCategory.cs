using Bjb.LiquidityGap.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bjb.LiquidityGap.Domain.Entities
{
    public class SubCategory : BaseEntity<int>, IAuditable, IDeactivable
    {
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }
        [MaxLength(10)]
        public string Code { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [NotMapped]
        public string ModuleName { get; set; } = "Master Data";
        [NotMapped]
        public string FeatureName { get; set; } = "Category";
        public virtual Category Category { get; set; }
    }
}
