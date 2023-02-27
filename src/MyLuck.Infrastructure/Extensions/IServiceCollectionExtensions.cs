namespace MyLuck.Infrastructure.Extensions;
using Microsoft.Extensions.DependencyInjection;

using MyLuck.Infrastructure.Features.High5;
using MyLuck.Infrastructure.Features.Lotto;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IHigh5DataService, High5DataService>();
        services.AddSingleton<ILottoDataService, LottoDataService>();

        return services;
    }
}
