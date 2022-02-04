using Bjb.LiquidityGap.Base.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Bjb.LiquidityGap.Infrastructure.Persistence
{
    public static class SeedManager
    {
        public static async Task<IHost> PerformSeeding(this IHost host)
        {
            System.Reflection.Assembly assemblies = typeof(SeedManager).Assembly;
            var types = assemblies.DefinedTypes.Where(x => x.GetInterfaces().Contains(typeof(ISeeder)));
            using (var scope = host.Services.CreateScope())
            {
                foreach (System.Reflection.TypeInfo type in types)
                {
                    var seeder = assemblies.CreateInstance(type.FullName) as ISeeder;
                    seeder.ServiceScope = scope;
                    await seeder.Seed();
                }
            }
            return host;
        }
    }
}
