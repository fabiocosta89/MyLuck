using Microsoft.Extensions.Configuration;
using MyLuck.Infrastructure.Features.Shared.LotteryResultsApi;
using Microsoft.Extensions.DependencyInjection;
using MyLuck.Infrastructure.Features.Email;
using MyLuck.Infrastructure.Features.EuroDreams;
using MyLuck.Infrastructure.Features.NotificationInfo;
using MyLuck.Infrastructure.Settings;

namespace MyLuck.Infrastructure.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager manager)
    {
        services.Configure<MailSettings>(
            manager.GetSection("EmailConfiguration"));
        services.Configure<LotteryApiSettings>(
            manager.GetSection("LotteryApi"));
        services.Configure<MyLuckDatabaseSettings>(
            manager.GetSection("MyLuckDatabase"));
        services.AddTransient<IEuroDreamsRepository, EuroDreamsRepository>();
        services.AddTransient<INotificationInfoRepository, NotificationInfoRepository>();
        services.AddTransient<IMailService, MailService>();
        services.AddTransient<ILotteryResultsApi, LotteryResultsApi>();
        services.AddHttpClient<LotteryResultsApi>();

        return services;
    }
}
