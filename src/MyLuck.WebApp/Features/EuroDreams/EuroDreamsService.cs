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

        foreach (var result in euroDreamsResults.OrderBy(x => x.DrawDay))
        {
            bool existAlready = await _euroDreamsRepository.ExistByDrawTimeAsync(result.DrawDay, cancellationToken);
            if (existAlready)
            {
                continue;
            }
            
            _logger.LogInformation("New EuroDreams draw for the day {Day}.", result.DrawDay.ToString("dd/MM/yyyy"));
            await _euroDreamsRepository.CreateAsync(result, cancellationToken);
            
            var key = BuildResultString(result);
            var date = result.DrawDay.ToString("dd/MM/yyyy");
            
            foreach (var email in emails)
            {
                var (lotteryKeyString, lotteryKeyMatchString) = BuildKeyString(email, result);

                string body;
                if (lotteryKeyString.Length > 0)
                {
                    body = string.Format(EuroDreamsConsts.Email.BodyWithUserKey, key, date, lotteryKeyString, lotteryKeyMatchString);
                }
                else
                {
                    body = string.Format(EuroDreamsConsts.Email.Body, key, date);
                }
                
                MailRequest mailRequest = new()
                {
                    EmailTo = email.Email,
                    Subject = string.Format(EuroDreamsConsts.Email.Subject, date),
                    Body = body
                };
                await _mailService.SendEmailAsync(mailRequest);
            }

            _logger.LogInformation("Emails sent.");
        }
    }

    public async Task GetOldResultsAsync(CancellationToken cancellationToken = default)
    {
        var offset = 0;
        const int maxOffset = 25;
        const int offsetIncrement = 5;
        while (offset <= maxOffset)
        {
            LotteryResult[]? draws = await _lotteryResults.GetEuroDreamsResultsWithOffset(offset, cancellationToken);
            if (draws is null)
            {
                _logger.LogError("No results!");
                return;
            }
            
            // Map into the entity model
            Collection<Infrastructure.Features.EuroDreams.EuroDreams> euroDreamsResults = EuroDreamsMappings.LoterieResultToEuroDreams(draws);
            foreach (var result in euroDreamsResults)
            {
                bool existAlready = await _euroDreamsRepository.ExistByDrawTimeAsync(result.DrawDay, cancellationToken);
                if (existAlready)
                {
                    continue;
                }
                
                await _euroDreamsRepository.CreateAsync(result, cancellationToken);
            }
            
            offset += offsetIncrement;
        }
    }

    private static (StringBuilder lotteryKeyString, StringBuilder lotteryKeyMatchString) BuildKeyString(
        NotificationInfo email, Infrastructure.Features.EuroDreams.EuroDreams result)
    {
        StringBuilder lotteryKeyString = new();
        StringBuilder lotteryKeyMatchString = new();
        foreach (var lotteryKey in email.LotteryKey.Where(x => x.LotteryType == LotteryType.EuroDreams))
        {
            lotteryKeyString.AppendLine($"{string.Join(" ", lotteryKey.Numbers)} - {string.Join(" ", lotteryKey.SpecialNumbers)}");
                    
            var matchNumbers = lotteryKey.Numbers.Intersect(result.Numbers).ToArray();
            var matchSpecialNumbers = lotteryKey.SpecialNumbers.Intersect(result.SpecialNumbers).ToArray();
            if (matchNumbers.Length > 0 || matchSpecialNumbers.Length > 0)
            {
                lotteryKeyMatchString.AppendLine($"{string.Join(" ", matchNumbers)} - {string.Join(" ", matchSpecialNumbers)}");
            }
        }

        return (lotteryKeyString, lotteryKeyMatchString);
    }

    private static string BuildResultString(Infrastructure.Features.EuroDreams.EuroDreams result)
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
