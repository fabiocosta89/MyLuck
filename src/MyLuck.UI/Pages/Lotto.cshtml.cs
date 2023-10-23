using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

using MyLuck.Infrastructure.Features.Key;
using MyLuck.Infrastructure.Features.Lotto;
using MyLuck.UI.Features.Lotto;

namespace MyLuck.UI.Pages
{
    [Authorize]
    public partial class LottoModel : PageModel
    {
        private readonly ILottoDataService _lottoDataService;
        private readonly IKeyDataService _keyDataService;

        public IEnumerable<Lotto> Lottos { get; set; } = Enumerable.Empty<Lotto>();
        
        public IEnumerable<Key> Keys { get; set; } = Enumerable.Empty<Key>();
        

        public LottoModel(ILottoDataService lottoDataService, IKeyDataService keyDataService)
        {
            _lottoDataService = lottoDataService;
            _keyDataService = keyDataService;
        }

        public async Task OnGetAsync()
        {
            // Get all Lotto results
            Lottos = await _lottoDataService.GetAll();
            Lottos = Lottos.OrderByDescending(l => l.DrawTime).ToList();

            // Get all the registered keys
            Keys = await _keyDataService.GetAsync(LottoConstants.Name);
        }
    }
}
