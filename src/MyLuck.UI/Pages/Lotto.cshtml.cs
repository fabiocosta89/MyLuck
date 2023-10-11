using Microsoft.AspNetCore.Mvc.RazorPages;

using MyLuck.Infrastructure.Features.Lotto;

namespace MyLuck.UI.Pages
{
    public partial class LottoModel : PageModel
    {
        private readonly ILottoDataService _lottoDataService;
        public IEnumerable<Lotto> Lottos { get; set; } = Enumerable.Empty<Lotto>();

        public LottoModel(ILottoDataService lottoDataService) => _lottoDataService = lottoDataService;

        public async Task OnGetAsync()
        {
            Lottos = await _lottoDataService.GetAll();
        }
    }
}
