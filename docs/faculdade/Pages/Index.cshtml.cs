using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace MinhaAplicacao.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {            
        }
    }
}