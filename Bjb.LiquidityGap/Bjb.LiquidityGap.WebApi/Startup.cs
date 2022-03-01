using Bjb.LiquidityGap.Application;
using Bjb.LiquidityGap.Application.Extensions;
using Bjb.LiquidityGap.Infrastructure.Persistence;
using Bjb.LiquidityGap.WebApi.Extensions;
using Bjb.LiquidityGap.WebApi.Helpers;
using Bjb.LiquidityGap.WebApi.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Serilog;

namespace Bjb.LiquidityGap.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                     .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Hour)
                     .CreateLogger();
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
#pragma warning disable CS1591
        public void ConfigureServices(IServiceCollection services)
        {

            services.BindConfigurationExtension(Configuration);
            services.AddControllers(options =>
            {
                options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
            }).AddNewtonsoftJson(options =>
            {
                //options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.Converters.Add(new StringEnumConverter());
            });
            services
               .Configure<ApiBehaviorOptions>(options =>
               {
                   // disable the automatic model state validation before reaching controller (remove overriding)
                   options.SuppressModelStateInvalidFilter = true;
               })
               .Configure<RouteOptions>(options =>
               {
                   // this will make url path to lower case
                   options.LowercaseUrls = true;
               });
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            services.AddApiVersioningExtension(Configuration);
            services.AddApplicationExtension(Configuration);
            services.AddSwaggerExtension(Configuration);
            services.AddJWTExtension(Configuration);
            services.AddPersistenceInfrastructure(Configuration);
            services.AddHttpContextAccessor();
            services.AddOwnCorsConfiguration(Configuration);
            services.AddSignalR();
            services.AddMemoryCache();
            services.AddConsulConfig(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddSerilog();
            if (env.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }
            //app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("DefaultPolicy");
            app.UseAuthentication();
            app.UseConsul();
            app.UseAuthorization();
            app.UseSwaggerExtension(env, provider);
            app.UseExceptionMiddleware();
            app.UseStaticFiles();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
