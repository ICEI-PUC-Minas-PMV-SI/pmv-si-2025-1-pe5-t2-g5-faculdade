using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MinhaAplicacao.Pages;

public class LogoutModel : PageModel
{
    public IActionResult OnGet()
    {
        HttpContext.Session.Clear(); // Remove o token e quaisquer dados da sessão
        return RedirectToPage("/Login");
    }
}