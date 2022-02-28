using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Bjb.LiquidityGap.WebApi.Extensions
{
    public static class ConsulExtention
    {
        public static IServiceCollection AddConsulConfig(this IServiceCollection services, IConfiguration configuration)
        {
            var a = configuration.GetSection("Consul:Host").Value;
            services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(c =>
              {
                  c.Address = new System.Uri(configuration.GetSection("Consul:Host").Value);
              }));
            return services;
        }

        public static IApplicationBuilder UseConsul(this IApplicationBuilder app)
        {
            var consulClient = app.ApplicationServices.GetRequiredService<IConsulClient>();
            var logger = app.ApplicationServices.GetRequiredService<ILoggerFactory>().CreateLogger("ConsulExtention");
            var lifetime = app.ApplicationServices.GetRequiredService<IApplicationLifetime>();
            var registration = new AgentServiceRegistration
            {
                ID = $"ApplicationService-Id",
                Name = "ApplicationService",
                Address = "localhost",
                Port = 5001,
                Tags = new string[] { "Application" , "NET5" }
            };
            logger.LogInformation("Register to consul");
            consulClient.Agent.ServiceDeregister(registration.ID).ConfigureAwait(false);
            consulClient.Agent.ServiceRegister(registration).ConfigureAwait(false);
            lifetime.ApplicationStopping.Register(() =>
            {
                logger.LogInformation("Deregister to consul");
                consulClient.Agent.ServiceDeregister(registration.ID).ConfigureAwait(!false);
            });
            return app;
        }
    }
}
