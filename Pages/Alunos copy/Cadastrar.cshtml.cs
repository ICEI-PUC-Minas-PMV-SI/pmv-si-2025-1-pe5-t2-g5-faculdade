using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MinhaAplicacao.Pages.Livros;

public class CadastroLivroModel(IHttpClientFactory clientFactory) : PageModel
{
    private readonly HttpClient _http = clientFactory.CreateClient("MinhaApi");

    [BindProperty]
    public Livro Livro { get; set; } = new();

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        var resposta = await _http.PostAsJsonAsync("api/livro/registrar", Livro);

        if (resposta.IsSuccessStatusCode)
        {
            return RedirectToPage("/Livros/Index");
        }

        var erro = await resposta.Content.ReadAsStringAsync();
        ModelState.AddModelError(string.Empty, $"Erro ao cadastrar: {erro}");
        return Page();
    }
}