using Bjb.LiquidityGap.Application.Extensions;
using Bjb.LiquidityGap.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Bjb.LiquidityGap.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Error()
                .WriteTo.Console()
                .WriteTo.Debug()
                .WriteTo.File
                (
                    path: "Log/application_.log",
                    rollOnFileSizeLimit: true, // rolling file, will created application-001 when reach file size limit
                    fileSizeLimitBytes: 524288000, // file size is 500MB
                    retainedFileCountLimit: 30, // make a rolling file up to 30
                    rollingInterval: RollingInterval.Day, // always make a new on once change day
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}]\t{Message:lj} - [{SourceContext}]{NewLine}{Exception}"
                )
                .CreateLogger();
            CreateHostBuilder(args)
                .Build()
                .PerformMigration()
                .PerformSeeding().GetAwaiter().GetResult()
                .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .BaseConfigurationn(option =>
                {
                    option.ConfigFolder = "Configs";
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseSerilog();
    }
}
