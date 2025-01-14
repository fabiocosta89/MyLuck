namespace MyLuck.Service.Setup;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using MyLuck.Infrastructure.Extensions;
using MyLuck.Infrastructure.Settings;
using MyLuck.Service.Features.EuroDreams;
using MyLuck.Service.Features.High5;
using MyLuck.Service.Features.Lotto;
using MyLuck.Service.Models;
using MyLuck.Service.Services;

using Quartz;

internal static class DependencyInjection
{
    /// <summary>
    /// Setup Dependeny Injection
    /// </summary>
    /// <returns></returns>
    internal static void Setup(HostApplicationBuilder builder)
    {
        builder.Logging
            .ClearProviders()
            .AddConsole();

        builder.Configuration.AddUserSecrets<Program>();

        // Add services
        builder.Services
            .Configure<MyLuckDatabaseSettings>(
                builder.Configuration.GetSection("MyLuckDatabase"))
            .Configure<LoterieSettings>(
                builder.Configuration.GetSection("Loterie"))
            .Configure<EmailSettings>(
                builder.Configuration.GetSection("EmailConfiguration"))
            .AddInfrastructure()
            .AddSingleton<ILoterieService, LoterieService>()
            .AddSingleton<IHigh5Service, High5Service>()
            .AddSingleton<ILottoService, LottoService>()
            .AddSingleton<IEuroDreamsService, EuroDreamsService>()
            .AddSingleton<IMailService, MailService>()
            .AddHttpClient<LoterieService>();
    }

    internal static void SetupQuartz(HostApplicationBuilder builder)
    {
        builder.Services.AddQuartz(x =>
        {
            // Eurodreams
            x.ScheduleJob<EuroDreamsJob>(trigger => trigger
                .WithIdentity("trigger_euro_dreams", "group1")
                .StartNow()
                .WithSimpleSchedule(schedule => schedule
                    .WithIntervalInMinutes(15)
                    .RepeatForever()),
                job => job
                    .WithIdentity("euro_dreams", "group1")
            );
        });

        builder.Services.AddQuartzHostedService(x =>
        {
            x.WaitForJobsToComplete = true;
        });

    }
}
