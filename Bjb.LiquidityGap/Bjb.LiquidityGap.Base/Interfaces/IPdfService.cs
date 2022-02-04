using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bjb.LiquidityGap.Base.Interfaces
{
    public interface IPdfService
    {
        Task<byte[]> TestPdf();
        Task<byte[]> GeneratePdf(string data);
    }
}
