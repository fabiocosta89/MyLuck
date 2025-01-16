namespace MyLuck.Functions.Settings;

using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using MyLuck.Infrastructure.Extensions;
using MyLuck.Infrastructure.Settings;
using MyLuck.Service.Features.EuroDreams;
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
    internal static void Setup(FunctionsApplicationBuilder builder)
    {
        builder.Configuration
            .AddUserSecrets<Program>()
            .AddEnvironmentVariables();

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
}
