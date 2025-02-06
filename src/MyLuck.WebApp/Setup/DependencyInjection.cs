using MyLuck.WebApp.Features.Email;
using MyLuck.WebApp.Features.Shared.Lottery;

namespace MyLuck.WebApp.Setup;

using Infrastructure.Extensions;
using Infrastructure.Settings;
using Features.EuroDreams;

using Quartz;

internal static class DependencyInjection
{
    internal static void Setup(WebApplicationBuilder builder)
    {
        builder.Configuration
            .AddUserSecrets<Program>();

        // Add services
        builder.Services
            .Configure<MyLuckDatabaseSettings>(
                builder.Configuration.GetSection("MyLuckDatabase"))
            .Configure<LoterieSettings>(
                builder.Configuration.GetSection("Loterie"))
            .Configure<MailSettings>(
                builder.Configuration.GetSection("EmailConfiguration"))
            .AddInfrastructure()
            .AddScoped<ILoterieService, LoterieService>()
            .AddScoped<IMailService, MailService>()
            .AddScoped<IEuroDreamsService, EuroDreamsService>()
            .AddHttpClient<LoterieService>();

        // Add jobs
        builder.Services.AddQuartz(x =>
        {
            x.UseDefaultThreadPool(tp => tp.MaxConcurrency = 1);
        
            // Eurodreams
            // x.ScheduleJob<EuroDreamsJob>(trigger => trigger
            //     .WithIdentity("trigger_eurodreams")
            //     .StartAt(DateBuilder.EvenMinuteDate(DateTimeOffset.UtcNow.AddSeconds(15)))
            //     // .StartNow()
            //     .WithDailyTimeIntervalSchedule(15, IntervalUnit.Second)
            //     // .WithDailyTimeIntervalSchedule(schedule => schedule.WithInterval(10, IntervalUnit.Second))
            //     .WithDescription("Check for new Eurodreams results")
            // );
            
            // // Eurodreams
            // x.ScheduleJob<EuroDreamsJob>(trigger => trigger
            //     .WithIdentity("trigger_euro_dreams", "group1")
            //     .StartNow()
            //     .WithSimpleSchedule(schedule => schedule
            //         .WithIntervalInMinutes(15)
            //         .RepeatForever()),
            //     job => job
            //         .WithIdentity("euro_dreams", "group1")
            // );
        });
        
        // builder.Services.AddQuartzHostedService();
    }
}
