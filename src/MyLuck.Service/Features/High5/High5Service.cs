namespace MyLuck.Service.Features.High5;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using MyLuck.Infrastructure.Features.High5;
using MyLuck.Infrastructure.Features.Key;
using MyLuck.Service.Models;
using MyLuck.Service.Services;

using System.Threading.Tasks;
using System.Linq;

using IMailService = Services.IMailService;
using System.Text;
using MyLuck.Service.Helpers;

internal class High5Service : IHigh5Service
{
    private readonly ILogger<High5Service> _logger;
    private readonly ILoterieService _loterieService;
    private readonly IHigh5DataService _high5DataService;
    private readonly IKeyDataService _keyDataService;
    private readonly IMailService _mailService;
    private readonly IOptions<LoterieSettings> _loterieSettings;

    public High5Service(
        ILogger<High5Service> logger,
        ILoterieService loterieService,
        IHigh5DataService high5DataService,
        IKeyDataService keyDataService,
        IMailService mailService,
        IOptions<LoterieSettings> loterieSettings)
    {
        _logger = logger;
        _loterieService = loterieService;
        _high5DataService = high5DataService;
        _keyDataService = keyDataService;
        _mailService = mailService;
        _loterieSettings = loterieSettings;
    }

    /// <summary>
    /// High 5 threatment
    /// </summary>
    public async Task RunAsync()
    {
        // Get url from appsettings
        string url = _loterieSettings.Value.High5Url;

        // Get High5 result from the Loterie website
        var draws = await _loterieService
            .GetResultAsync<LoterieDraws<LoterieDraw<LoterieResult>>>(url);
        if (draws == null)
        {
            _logger.LogError("No results!");
            return;
        }

        // Map into the entity model
        High5? high5 = High5Mappings.LoterieDrawsToHigh5(draws);
        if (high5 == null
            || string.IsNullOrEmpty(high5.DrawId))
        {
            _logger.LogError("Error on mapping!");
            return;
        }

        // Check if the draw exist already
        bool existAlready = await _high5DataService.ExisteAsync(high5.DrawId);
        if (existAlready)
        {
            _logger.LogInformation("High5 draw already exist.");
            return;
        }

        // Save the draw if it's new
        _logger.LogInformation("New High5 draw.");
        await _high5DataService.CreateAsync(high5);

        // Check if there is any key saved for the game
        StringBuilder keyStringBuilder = new();
        IEnumerable<Key> keys = await _keyDataService.GetAsync(High5Consts.Name);
        if (keys.Any())
        {
            int?[] drawKeys = High5Helper.ModelIntoArray(high5);

            // Check if there are any matching numbers
            keyStringBuilder = KeyHelper.CheckMatching(keys, drawKeys, Array.Empty<int?>());
        }

        // Send email
        StringBuilder keyString = new(
            $"{high5.Number1} {high5.Number2} {high5.Number3} {high5.Number4} {high5.Number5}");
        if (keyStringBuilder.Length > 0)
        {
            keyString.AppendLine(keyStringBuilder.ToString());
        }

        string date = high5.DrawTime.ToString("dd/MM/yyyy");

        MailRequest mailRequest = new()
        {
            Subject = string.Format(High5Consts.Email.Subjet, date),
            Body = string.Format(High5Consts.Email.Body, keyString, date)
        };
        await _mailService.SendEmailAsync(mailRequest);
        _logger.LogInformation("Email sent.");
    }
}
