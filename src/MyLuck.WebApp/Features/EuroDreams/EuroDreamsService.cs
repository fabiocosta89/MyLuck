namespace MyLuck.WebApp.Features.EuroDreams;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using MyLuck.Infrastructure.Features.EuroDreams;
using MyLuck.Infrastructure.Features.Settings;
using Email;
using Shared.Lottery;

public class EuroDreamsService : IEuroDreamsService
{
    private readonly ILogger<EuroDreamsService> _logger;
    private readonly IOptions<LoterieSettings> _loterieSettings;
    private readonly ILoterieService _loterieService;
    private readonly IEuroDreamDataService _euroDreamDataService;
    private readonly IMailService _mailService;
    private readonly ISettingsDataService _settingsDataService;

    public EuroDreamsService(
        ILogger<EuroDreamsService> logger,
        IOptions<LoterieSettings> loterieSettings,
        ILoterieService loterieService,
        IEuroDreamDataService euroDreamDataService,
        IMailService mailService,
        ISettingsDataService settingsDataService)
    {
        _logger = logger;
        _loterieSettings = loterieSettings;
        _loterieService = loterieService;
        _euroDreamDataService = euroDreamDataService;
        _mailService = mailService;
        _settingsDataService = settingsDataService;
    }

    public async Task GetResultsAsync()
    {
        // EmailSettings emailSettings = await _settingsDataService.GetEmailSettings();
        // // If the option is turned off, leave
        // if (!emailSettings.EuroDream)
        // {
        //     return;
        // }

        // Get url from appsettings
        string url = _loterieSettings.Value.EuroDreams;

        // Get the result from the Loterie website
        var draws = await _loterieService
            .GetResultAsync<LoterieDraws<LoterieDraw<EuroDreamsResult>>>(url);
        if (draws == null)
        {
            _logger.LogError("No results!");
            return;
        }

        // Map into the entity model
        EuroDream? euroDream = EuroDreamsMappings.LoterieDrawsToEuroDream(draws);
        if (euroDream == null
            || !euroDream.DrawTimeValue.HasValue)
        {
            _logger.LogError("Error on mapping!");
            return;
        }

        // Check if the draw exist already
        bool existAlready = await _euroDreamDataService.ExisteByDrawTimeAsync(euroDream.DrawTimeValue.Value);
        if (existAlready)
        {
            _logger.LogInformation("EuroDreams draw already exist.");
            return;
        }

        // Save the draw if it's new
        _logger.LogInformation("New EuroDreams draw.");
        await _euroDreamDataService.CreateAsync(euroDream);

        // Send email
        string key = $"{euroDream.Number1} {euroDream.Number2} {euroDream.Number3} {euroDream.Number4} {euroDream.Number5} {euroDream.Number6} - {euroDream.Special}";
        string date = euroDream.DrawTime.ToString("dd/MM/yyyy");

        MailRequest mailRequest = new()
        {
            Subject = string.Format(EuroDreamsConsts.Email.Subject, date),
            Body = string.Format(EuroDreamsConsts.Email.Body, key, date)
        };
        await _mailService.SendEmailAsync(mailRequest);
        _logger.LogInformation("Email sent.");
    }
}
