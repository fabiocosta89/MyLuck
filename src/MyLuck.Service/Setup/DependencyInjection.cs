namespace MyLuck.Service.Setup;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using MyLuck.Infrastructure.Extensions;
using MyLuck.Infrastructure.Settings;
using MyLuck.Service.Features.High5;
using MyLuck.Service.Features.Lotto;
using MyLuck.Service.Models;
using MyLuck.Service.Services;

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

        // Add services
        builder.Services
            .Configure<MyLuckDatabaseSettings>(
                builder.Configuration.GetSection("MyLuckDatabase"))
            .Configure<LoterieSettings>(
                builder.Configuration.GetSection("Loterie"))
            .Configure<EmailSettings>(
                builder.Configuration.GetSection("EmailConfiguration"))
            .AddHttpClient()
            .AddInfrastructure()
            .AddSingleton<ILoterieService, LoterieService>()
            .AddSingleton<IHigh5Service, High5Service>()
            .AddSingleton<ILottoService, LottoService>()
            .AddSingleton<IMailService, MailService>();
    }
}
