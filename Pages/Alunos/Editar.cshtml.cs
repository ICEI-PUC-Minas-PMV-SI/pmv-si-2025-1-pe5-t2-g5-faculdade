using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace MinhaAplicacao.Pages.Alunos;
public class EditarAlunoModel : PageModel
{
    private readonly IHttpClientFactory clientFactory;

    private readonly ApiSettings _settings;

    public string ApiBaseUrl => _settings.BaseUrl;

    [BindProperty]
    public Aluno Aluno { get; set; } = new();

    public EditarAlunoModel(IOptions<ApiSettings> options, IHttpClientFactory clientFactory)
    {
        this.clientFactory = clientFactory;
        _settings = options?.Value ?? throw new ArgumentNullException(nameof(options));
    }

    public async Task<IActionResult> OnGetAsync([FromQuery] int id)
    {
        var client = this.clientFactory.CreateClient("MinhaApi");
        var resposta = await client.GetFromJsonAsync<Aluno>($"api/aluno/{id}");

        if (resposta == null)
            return NotFound();

        Aluno = resposta;

        ModelState.Clear();
        return Page();
    }
}