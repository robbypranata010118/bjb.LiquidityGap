using Bjb.LiquidityGap.Base.Interfaces;
using Bjb.LiquidityGap.Infrastructure.Persistence.Repositories;
using Bjb.LiquidityGap.Infrastructure.Persistence.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bjb.LiquidityGap.Infrastructure.Persistence
{
    public static class ServiceRegistration
    {
        private const string DEFAULT_CONNECTION_STRING = "DefaultConnection";

        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDBContext>(options =>
               options.UseSqlServer
               (
                   configuration.GetConnectionString(DEFAULT_CONNECTION_STRING),
                   b => b.MigrationsAssembly(typeof(AppDBContext).Assembly.FullName)
               )

           );
            #region Repositories
            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            services.AddTransient<ILogService, LogService>();
            #endregion
        }
        public static IApplicationBuilder PerformAppMigration(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
            .GetRequiredService<IServiceScopeFactory>()
            .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<AppDBContext>())
                {
                    context.Database.Migrate();
                    //ApplicationDbInitializer.Initialize(context);
                }
            }
            return app;
        }
    }
}
