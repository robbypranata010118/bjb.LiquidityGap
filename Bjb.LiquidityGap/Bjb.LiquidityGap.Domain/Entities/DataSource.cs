using Bjb.LiquidityGap.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bjb.LiquidityGap.Domain.Entities
{
    public class DataSource : BaseEntity<int>, IAuditable, IDeactivable
    {
        public string Name { get; set; }
        public string ConnString { get; set; }
        public bool UseETL { get; set; }
        [NotMapped]
        public string ModuleName { get; set; } = "Master Data";
        [NotMapped]
        public string FeatureName { get; set; } = "Data Source";
    }
}
