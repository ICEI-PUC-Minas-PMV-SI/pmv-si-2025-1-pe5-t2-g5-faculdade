using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MinhaAplicacao.Pages.Alunos;
public class EditarAlunoModel(IHttpClientFactory clientFactory) : PageModel
{
    private readonly HttpClient _http = clientFactory.CreateClient("MinhaApi");

    [BindProperty]
    public Aluno Aluno { get; set; } = new();

    public async Task<IActionResult> OnGetAsync([FromQuery] int id)
    {
        var resposta = await _http.GetFromJsonAsync<Aluno>($"api/aluno/{id}");

        if (resposta == null)
            return NotFound();

        Aluno = resposta;

        ModelState.Clear();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        var resposta = await _http.PostAsJsonAsync(
            $"api/aluno/editar/{Aluno.Id}",
            Aluno
        );

        if (resposta.IsSuccessStatusCode)
            return RedirectToPage("/Alunos/Index");

        ModelState.AddModelError("", "Erro ao atualizar aluno.");
        return Page();
    }
}