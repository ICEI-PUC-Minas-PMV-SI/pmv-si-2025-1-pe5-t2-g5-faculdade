using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace MinhaAplicacao.Pages.Alunos;

public class IndexAlunoModel : PageModel
{
    public List<Aluno> Alunos { get; set; } = [];
    
    private readonly IHttpClientFactory clientFactory;

    private readonly ApiSettings _settings;

    public string ApiBaseUrl => _settings.BaseUrl;

    public IndexAlunoModel(IOptions<ApiSettings> options, IHttpClientFactory clientFactory)
    {
        this.clientFactory = clientFactory;
        _settings = options?.Value ?? throw new ArgumentNullException(nameof(options));
    }

    public async Task OnGetAsync()
    {
        var client = this.clientFactory.CreateClient("MinhaApi");
        var resposta = await client.GetFromJsonAsync<List<Aluno>>("api/aluno");

        if (resposta != null)
            Alunos = resposta;
    }
}