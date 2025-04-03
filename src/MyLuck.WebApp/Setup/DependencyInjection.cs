using MyLuck.Infrastructure.Extensions;
using MyLuck.WebApp.Features.EuroDreams;
using Quartz;

namespace MyLuck.WebApp.Setup;
internal static class DependencyInjection
{
    internal static void Setup(WebApplicationBuilder builder)
    {
        builder.Configuration
            .AddUserSecrets<Program>();

        // Add services
        builder.Services
            .AddInfrastructure(builder.Configuration)
            .AddScoped<IEuroDreamsService, EuroDreamsService>();

        // Add jobs
        builder.Services.AddQuartz(x =>
        {
            x.UseDefaultThreadPool(tp => tp.MaxConcurrency = 1);
            
            // Eurodreams
            x.ScheduleJob<EuroDreamsJob>(trigger => trigger
                .WithIdentity("trigger_eurodreams")
                .StartNow()
                .WithDailyTimeIntervalSchedule(20, IntervalUnit.Minute)
                .WithDescription("Check for new Eurodreams results")
            );
        });
        
        builder.Services.AddQuartzHostedService();
    }
}
