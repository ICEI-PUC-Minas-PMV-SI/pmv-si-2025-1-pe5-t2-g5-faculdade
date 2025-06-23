using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MinhaAplicacao.Pages.Livros;
public class EditarLivroModel(IHttpClientFactory clientFactory) : PageModel
{
    private readonly HttpClient _http = clientFactory.CreateClient("MinhaApi");

    [BindProperty]
    public Livro Livro { get; set; } = new();

    public async Task<IActionResult> OnGetAsync([FromQuery] int id)
    {
        var resposta = await _http.GetFromJsonAsync<Livro>($"api/livro/{id}");

        if (resposta == null)
            return NotFound();

        Livro = resposta;

        ModelState.Clear();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        var resposta = await _http.PostAsJsonAsync(
            $"api/livro/editar/{Livro.Id}",
            Livro
        );

        if (resposta.IsSuccessStatusCode)
            return RedirectToPage("/Livros/Index");

        ModelState.AddModelError("", "Erro ao atualizar livro.");
        return Page();
    }
}