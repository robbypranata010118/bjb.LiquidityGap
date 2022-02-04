using Bjb.LiquidityGap.Base.Parameters;
using System.Collections.Generic;

namespace Bjb.LiquidityGap.Base.Interfaces
{
    public interface IRequestParameter
    {
        int Page { get; set; }
        int Length { get; set; }
        List<string> Orders { get; set; }
        string SortType { get; set; }
        List<RequestFilterParameter> Filters { get; set; }
    }
}
