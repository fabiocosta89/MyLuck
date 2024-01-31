namespace MyLuck.Infrastructure.Extensions;
using Microsoft.Extensions.DependencyInjection;

using MyLuck.Infrastructure.Features.EuroDreams;
using MyLuck.Infrastructure.Features.High5;
using MyLuck.Infrastructure.Features.Key;
using MyLuck.Infrastructure.Features.Lotto;
using MyLuck.Infrastructure.Features.Settings;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<IHigh5DataService, High5DataService>();
        services.AddTransient<ILottoDataService, LottoDataService>();
        services.AddTransient<IEuroDreamDataService, EuroDreamDataService>();
        services.AddTransient<IKeyDataService, KeyDataService>();
        services.AddTransient<ISettingsDataService, SettingsDataService>();

        return services;
    }
}
