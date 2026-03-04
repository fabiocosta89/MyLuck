using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using MyLuck.Infrastructure.Features.Email;
using MyLuck.Infrastructure.Features.Shared.LotteryResultsApi;

using MyLuck.Infrastructure.Features.EuroDreams;
using MyLuck.Infrastructure.Features.NotificationInfo;

namespace MyLuck.WebApp.Features.EuroDreams;

public partial class EuroDreamsService : IEuroDreamsService
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
        var draws = await _lotteryResults.GetEuroDreamsResults(cancellationToken);
        if (draws is null)
        {
            _logger.LogError("No results!");
            return;
        }

        // Map into the entity model
        var euroDreamsResults = EuroDreamsMappings.LoterieResultToEuroDreams(draws.Draws);
        
        var emails = (await _notificationInfoRepository.GetActiveEmails(cancellationToken)).ToArray();
        LogThereAreNumberofemailsEmailsActives(emails.Length);

        foreach (var result in euroDreamsResults.OrderBy(x => x.DrawDay))
        {
            var existAlready = await _euroDreamsRepository.ExistByDrawTimeAsync(result.DrawDay, cancellationToken);
            if (existAlready)
            {
                continue;
            }
            
            LogNewEurodreamsDrawForTheDayDay(result.DrawDay.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
            await _euroDreamsRepository.CreateAsync(result, cancellationToken);
            
            var key = BuildResultString(result);
            var date = result.DrawDay.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            var subject = string.Format(CultureInfo.InvariantCulture, EuroDreamsConsts.Email.Subject, date);
            
            foreach (var email in emails)
            {
                var (lotteryKeyString, lotteryKeyMatchString) = BuildKeyString(email, result);

                var body = lotteryKeyString.Length > 0 
                    ? string.Format(
                        CultureInfo.InvariantCulture, 
                        EuroDreamsConsts.Email.BodyWithUserKey, key, date, lotteryKeyString, lotteryKeyMatchString) 
                    : string.Format(
                        CultureInfo.InvariantCulture, 
                        EuroDreamsConsts.Email.Body, key, date);
                
                MailRequest mailRequest = new()
                {
                    EmailTo = email.Email,
                    Subject = subject,
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
            var draws = await _lotteryResults.GetEuroDreamsResultsWithOffset(offset, cancellationToken);
            if (draws is null)
            {
                _logger.LogError("No results!");
                return;
            }
            
            // Map into the entity model
            var euroDreamsResults = EuroDreamsMappings.LoterieResultToEuroDreams(draws.Draws);
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
            lotteryKeyString.AppendLine(
                CultureInfo.InvariantCulture, 
                $"{string.Join(" ", lotteryKey.Numbers)} - {string.Join(" ", lotteryKey.SpecialNumbers)}");
                    
            var matchNumbers = lotteryKey.Numbers.Intersect(result.Numbers).ToArray();
            var matchSpecialNumbers = lotteryKey.SpecialNumbers.Intersect(result.SpecialNumbers).ToArray();
            if (matchNumbers.Length > 0 || matchSpecialNumbers.Length > 0)
            {
                lotteryKeyMatchString.AppendLine(
                    CultureInfo.InvariantCulture,
                    $"{string.Join(" ", matchNumbers)} - {string.Join(" ", matchSpecialNumbers)}");
            }
        }

        return (lotteryKeyString, lotteryKeyMatchString);
    }

    private static string BuildResultString(Infrastructure.Features.EuroDreams.EuroDreams result)
    {
        StringBuilder key = new();
        foreach (var number in result.Numbers)
        {
            key.Append(CultureInfo.InvariantCulture, $"{number} ");
        }
        key.Append("- ");
        foreach (var number in result.SpecialNumbers)
        {
            key.Append(CultureInfo.InvariantCulture, $"{number} ");
        }

        return key.ToString();
    }

    [LoggerMessage(LogLevel.Information, "There are {NumberOfEmails} Emails actives.")]
    partial void LogThereAreNumberofemailsEmailsActives(int NumberOfEmails);

    [LoggerMessage(LogLevel.Information, "New EuroDreams draw for the day {Day}.")]
    partial void LogNewEurodreamsDrawForTheDayDay(string Day);
}
