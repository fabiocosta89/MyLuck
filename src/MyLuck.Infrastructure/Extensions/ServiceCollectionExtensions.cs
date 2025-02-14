namespace MyLuck.Infrastructure.Extensions;
using Microsoft.Extensions.DependencyInjection;

using Features.EuroDreams;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<IEuroDreamDataService, EuroDreamDataService>();
        services.AddTransient<IEuroDreamsRepository, EuroDreamsRepository>();

        return services;
    }
}
