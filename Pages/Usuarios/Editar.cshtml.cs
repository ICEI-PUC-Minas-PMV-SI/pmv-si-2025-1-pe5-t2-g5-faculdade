using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MinhaAplicacao.Pages.Usuarios;
public class EditarModel(IHttpClientFactory clientFactory) : PageModel
{
    private readonly HttpClient _http = clientFactory.CreateClient("MinhaApi");

    [BindProperty]
    public Usuario Usuario { get; set; } = new();

    public async Task<IActionResult> OnGetAsync([FromQuery] int id)
    {
        var resposta = await _http.GetFromJsonAsync<Usuario>($"api/usuario/{id}");

        if (resposta == null)
            return NotFound();

        Usuario = resposta;

        ModelState.Clear();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        var resposta = await _http.PostAsJsonAsync(
            $"api/usuario/editar/{Usuario.Id}",
            Usuario
        );

        if (resposta.IsSuccessStatusCode)
            return RedirectToPage("/Usuarios/Index");

        ModelState.AddModelError("", "Erro ao atualizar usuário.");
        return Page();
    }
}