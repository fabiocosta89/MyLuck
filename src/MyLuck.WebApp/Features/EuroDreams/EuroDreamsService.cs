using System.Collections.ObjectModel;
using System.Text;
using MyLuck.Infrastructure.Features.Email;
using MyLuck.Infrastructure.Features.Shared.LotteryResultsApi;

using MyLuck.Infrastructure.Features.EuroDreams;
using MyLuck.Infrastructure.Features.NotificationInfo;

namespace MyLuck.WebApp.Features.EuroDreams;

public class EuroDreamsService : IEuroDreamsService
{
    private readonly ILogger<EuroDreamsService> _logger;
    private readonly IMailService _mailService;
    private readonly ILotteryResultsApi _lotteryResults;
    private readonly IEuroDreamsRepository _euroDreamsRepository;
    private readonly INotificationInfoRepository _notificationInfoRepository;

    public EuroDreamsService(
        ILogger<EuroDreamsService> logger,
        IMailService mailService,
        ILotteryResultsApi lotteryResults, 
        IEuroDreamsRepository euroDreamsRepository, 
        INotificationInfoRepository notificationInfoRepository)
    {
        _logger = logger;
        _mailService = mailService;
        _lotteryResults = lotteryResults;
        _euroDreamsRepository = euroDreamsRepository;
        _notificationInfoRepository = notificationInfoRepository;
    }

    public async Task GetResultsAsync(CancellationToken cancellationToken = default)
    {
        LotteryResult[]? draws = await _lotteryResults.GetEuroDreamsResults(cancellationToken);
        if (draws is null)
        {
            _logger.LogError("No results!");
            return;
        }

        // Map into the entity model
        Collection<Infrastructure.Features.EuroDreams.EuroDreams> euroDreamsResults = EuroDreamsMappings.LoterieResultToEuroDreams(draws);
        
        var emails = (await _notificationInfoRepository.GetActiveEmails(cancellationToken)).ToArray();
        _logger.LogInformation("There are {NumberOfEmails} Emails actives.", emails.Length);

        foreach (var result in euroDreamsResults.OrderBy(x => x.DrawTime))
        {
            bool existAlready = await _euroDreamsRepository.ExistByDrawTimeAsync(result.DrawTime, cancellationToken);
            if (existAlready)
            {
                continue;
            }
            
            _logger.LogInformation("New EuroDreams draw for the day {Day}.", result.DrawTime.ToString("dd/MM/yyyy"));
            await _euroDreamsRepository.CreateAsync(result, cancellationToken);
            
            var key = BuildKeyString(result);
            var date = result.DrawTime.ToString("dd/MM/yyyy");
            
            foreach (var email in emails)
            {
                MailRequest mailRequest = new()
                {
                    EmailTo = email,
                    Subject = string.Format(EuroDreamsConsts.Email.Subject, date),
                    Body = string.Format(EuroDreamsConsts.Email.Body, key, date)
                };
                await _mailService.SendEmailAsync(mailRequest);
            }

            _logger.LogInformation("Emails sent.");
        }
    }

    private static string BuildKeyString(Infrastructure.Features.EuroDreams.EuroDreams result)
    {
        StringBuilder key = new();
        foreach (var number in result.Numbers)
        {
            key.Append($"{number} ");
        }
        key.Append("- ");
        foreach (var number in result.SpecialNumbers)
        {
            key.Append($"{number} ");
        }

        return key.ToString();
    }
}
