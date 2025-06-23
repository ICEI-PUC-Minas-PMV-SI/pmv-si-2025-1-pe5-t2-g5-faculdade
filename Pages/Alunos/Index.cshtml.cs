using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MinhaAplicacao.Pages.Alunos;

public class IndexAlunoModel(IHttpClientFactory clientFactory) : PageModel
{
    private readonly HttpClient _http = clientFactory.CreateClient("MinhaApi");

    public List<Aluno> Alunos { get; set; } = [];

    public async Task OnGetAsync()
    {
        var resposta = await _http.GetFromJsonAsync<List<Aluno>>("api/aluno");

        if (resposta != null)
            Alunos = resposta;
    }

    public async Task<IActionResult> OnPostExcluirAsync(int id)
    {
        var resposta = await _http.DeleteAsync($"api/aluno/{id}");

        if (resposta.IsSuccessStatusCode)
            return RedirectToPage();

        ModelState.AddModelError(string.Empty, "Erro ao excluir o aluno.");
        await OnGetAsync();
        return Page();
    }
}