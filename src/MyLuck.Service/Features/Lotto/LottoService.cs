namespace MyLuck.Service.Features.Lotto;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using MyLuck.Infrastructure.Features.Lotto;
using MyLuck.Infrastructure.Features.Settings;
using MyLuck.Service.Models;
using MyLuck.Service.Services;

using System.Threading.Tasks;

internal class LottoService : ILottoService
{
    private readonly ILogger<LottoService> _logger;
    private readonly ILoterieService _loterieService;
    private readonly IOptions<LoterieSettings> _loterieSettings;
    private readonly ILottoDataService _lottoDataService;
    private readonly IMailService _mailService;
    private readonly ISettingsDataService _settingsDataService;

    public LottoService(
        ILogger<LottoService> logger,
        ILoterieService loterieService,
        IOptions<LoterieSettings> loterieSettings,
        ILottoDataService lottoDataService,
        IMailService mailService,
        ISettingsDataService settingsDataService)
    {
        _logger = logger;
        _loterieService = loterieService;
        _loterieSettings = loterieSettings;
        _lottoDataService = lottoDataService;
        _mailService = mailService;
        _settingsDataService = settingsDataService;
    }

    /// <summary>
    /// Lotto treatment
    /// </summary>
    /// <returns></returns>
    public async Task RunAsync()
    {
        Infrastructure.Features.Settings.EmailSettings emailSettings = await _settingsDataService.GetEmailSettings();
        // If the option is turned off, leave
        if (!emailSettings.Lotto)
        {
            return;
        }

        // Get url from appsettings
        string url = _loterieSettings.Value.LottoUrl;

        // Get the lotto result from the Loterie website
        var draws = await _loterieService
            .GetResultAsync<LoterieDraws<LoterieDraw<LottoResult>>>(url);
        if (draws == null)
        {
            _logger.LogError("No results!");
            return;
        }

        // Map into the entity model
        Lotto? lotto = LottoMappings.LoterieDrawsToLotto(draws);
        if (lotto == null
            || !lotto.DrawTimeValue.HasValue)
        {
            _logger.LogError("Error on mapping!");
            return;
        }

        // Check if the draw exist already
        bool existAlready = await _lottoDataService.ExisteByDrawTimeAsync(lotto.DrawTimeValue.Value);
        if (existAlready)
        {
            _logger.LogInformation("Lotto draw already exist.");
            return;
        }

        // Save the draw if it's new
        _logger.LogInformation("New Lotto draw.");
        await _lottoDataService.CreateAsync(lotto);

        // Send email
        string key = $"{lotto.Number1} {lotto.Number2} {lotto.Number3} {lotto.Number4} {lotto.Number5} {lotto.Number6} - {lotto.Special}";
        string date = lotto.DrawTime.ToString("dd/MM/yyyy");

        MailRequest mailRequest = new()
        {
            Subject = string.Format(LottoConsts.Email.Subjet, date),
            Body = string.Format(LottoConsts.Email.Body, key, date)
        };
        await _mailService.SendEmailAsync(mailRequest);
        _logger.LogInformation("Email sent.");
    }
}
