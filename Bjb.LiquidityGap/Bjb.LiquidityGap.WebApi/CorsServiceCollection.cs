using Bjb.LiquidityGap.Domain.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Bjb.LiquidityGap.WebApi
{
    public static class CorsServiceCollection
    {
        private const string APP_SETTINGS_CORS_SECTION = "Cors";

        public static IServiceCollection AddOwnCorsConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            CorsSetting corsSetting = new CorsSetting();
            configuration.Bind(APP_SETTINGS_CORS_SECTION, corsSetting);
            services.AddCors(options =>
            {
                options.AddPolicy("DefaultPolicy", policy =>
                {
                    policy.WithOrigins(corsSetting.Origins.ToArray())
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials();
                });
            });

            return services;
        }
    }
}
