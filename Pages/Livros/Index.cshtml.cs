using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace MinhaAplicacao.Pages.Livros;

public class IndexLivroModel : PageModel
{
    private readonly IHttpClientFactory clientFactory;

    public List<Livro> Livros { get; set; } = [];

    private readonly ApiSettings _settings;

    public string ApiBaseUrl => _settings.BaseUrl;

    public IndexLivroModel(IOptions<ApiSettings> options, IHttpClientFactory clientFactory)
    {
        this.clientFactory = clientFactory;
        _settings = options?.Value ?? throw new ArgumentNullException(nameof(options));
    }

    public async Task OnGetAsync()
    {
        var client = this.clientFactory.CreateClient("MinhaApi");
        var resposta = await client.GetFromJsonAsync<List<Livro>>("api/livro");

        if (resposta != null)
            Livros = resposta;
    }
}