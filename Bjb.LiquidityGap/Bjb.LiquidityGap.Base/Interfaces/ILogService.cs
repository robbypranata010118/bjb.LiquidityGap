using Bjb.LiquidityGap.Base.Dtos.AuditTrails;
using System.Threading.Tasks;

namespace Bjb.LiquidityGap.Base.Interfaces
{
    public interface ILogService
    {
        public Task<int> InsertLog(AuditTrailRequest logDto);
    }
}
