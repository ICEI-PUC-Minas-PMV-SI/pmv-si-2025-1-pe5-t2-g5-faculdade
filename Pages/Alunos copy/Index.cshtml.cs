using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MinhaAplicacao.Pages.Livros;

public class IndexLivroModel(IHttpClientFactory clientFactory) : PageModel
{
    private readonly HttpClient _http = clientFactory.CreateClient("MinhaApi");

    public List<Livro> Livros { get; set; } = [];

    public async Task OnGetAsync()
    {
        var resposta = await _http.GetFromJsonAsync<List<Livro>>("api/livro");

        if (resposta != null)
            Livros = resposta;
    }

    public async Task<IActionResult> OnPostExcluirAsync(int id)
    {
        var resposta = await _http.DeleteAsync($"api/livro/{id}");

        if (resposta.IsSuccessStatusCode)
            return RedirectToPage();

        ModelState.AddModelError(string.Empty, "Erro ao excluir o livro.");
        await OnGetAsync();
        return Page();
    }
}