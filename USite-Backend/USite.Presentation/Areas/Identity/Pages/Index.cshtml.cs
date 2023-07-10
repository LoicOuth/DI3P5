#nullable disable

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using USite.Infrastructure.Settings;

namespace USite.Presentation.Areas.Identity.Pages.Account
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public IndexModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult OnGetAsync()
        {
            return Redirect(_configuration.GetPresentationLink());
        }
    }
}
