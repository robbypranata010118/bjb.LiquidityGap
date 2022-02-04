using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bjb.LiquidityGap.Base.Interfaces
{
    public interface ISeeder
    {
        IServiceScope ServiceScope { get; set; }
        Task Seed();
    }
}
