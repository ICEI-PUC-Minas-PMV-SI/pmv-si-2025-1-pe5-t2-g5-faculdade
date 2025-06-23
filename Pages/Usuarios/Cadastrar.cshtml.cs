using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MinhaAplicacao.Pages.Usuarios;

public class CadastroModel(IHttpClientFactory clientFactory) : PageModel
{
    private readonly HttpClient _http = clientFactory.CreateClient("MinhaApi");

    [BindProperty]
    public Usuario Usuario { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        var resposta = await _http.PostAsJsonAsync("api/usuario/registrar", Usuario);

        if (resposta.IsSuccessStatusCode)
        {
            return RedirectToPage("/Usuarios/Index");
        }

        var erro = await resposta.Content.ReadAsStringAsync();
        ModelState.AddModelError(string.Empty, $"Erro ao cadastrar: {erro}");
        return Page();
    }
}