using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MinhaAplicacao.Pages.Usuarios;

public class IndexModel(IHttpClientFactory clientFactory) : PageModel
{
    private readonly HttpClient _http = clientFactory.CreateClient("MinhaApi");

    public List<Usuario> Usuarios { get; set; } = [];

    public async Task OnGetAsync()
    {
        var resposta = await _http.GetFromJsonAsync<List<Usuario>>("api/usuario");

        if (resposta != null)
            Usuarios = resposta;
    }

    public async Task<IActionResult> OnPostExcluirAsync(int id)
    {
        var resposta = await _http.DeleteAsync($"api/usuario/{id}");

        if (resposta.IsSuccessStatusCode)
            return RedirectToPage();

        ModelState.AddModelError(string.Empty, "Erro ao excluir o usuário.");
        await OnGetAsync();
        return Page();
    }
}