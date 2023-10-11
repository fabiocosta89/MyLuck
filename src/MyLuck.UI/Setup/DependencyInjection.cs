namespace MyLuck.UI.Setup;

using MyLuck.Infrastructure.Settings;
using MyLuck.Infrastructure.Extensions;

internal static class DependencyInjection
{
    /// <summary>
    /// Setup Dependeny Injection
    /// </summary>
    /// <returns></returns>
    internal static void SetupDependencyInjection(this IServiceCollection services, IConfiguration config)
    {
        // Add services
        services
            .Configure<MyLuckDatabaseSettings>(
                config.GetSection("MyLuckDatabase"))
            .AddInfrastructure();
    }
}
