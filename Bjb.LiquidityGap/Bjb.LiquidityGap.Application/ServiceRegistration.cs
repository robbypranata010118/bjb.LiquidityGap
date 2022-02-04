using bjb.util.uim;
using Bjb.LiquidityGap.Application.Behaviours;
using Bjb.LiquidityGap.Base.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using System.Reflection;

namespace Bjb.LiquidityGap.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationExtension(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("id");
            services.AddTransient<ICurrentUserService, CurrentUserService>();
            services.AddTechRedemptionUtilUim(configuration);
        }
    }
}
