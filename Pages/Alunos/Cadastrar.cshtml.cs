using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MinhaAplicacao.Pages.Alunos;

public class CadastroAlunoModel(IHttpClientFactory clientFactory) : PageModel
{
    private readonly HttpClient _http = clientFactory.CreateClient("MinhaApi");

    [BindProperty]
    public Aluno Aluno { get; set; } = new();

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        var resposta = await _http.PostAsJsonAsync("api/aluno/registrar", Aluno);

        if (resposta.IsSuccessStatusCode)
        {
            return RedirectToPage("/Alunos/Index");
        }

        var erro = await resposta.Content.ReadAsStringAsync();
        ModelState.AddModelError(string.Empty, $"Erro ao cadastrar: {erro}");
        return Page();
    }
}