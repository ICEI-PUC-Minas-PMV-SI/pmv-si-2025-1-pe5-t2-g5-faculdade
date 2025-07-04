using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace MinhaAplicacao.Pages.Usuarios;

public class IndexModel : PageModel
{
    private readonly IHttpClientFactory clientFactory;

    public List<Usuario> Usuarios { get; set; } = [];

    private readonly ApiSettings _settings;

    public string ApiBaseUrl => _settings.BaseUrl;

    public IndexModel(IOptions<ApiSettings> options, IHttpClientFactory clientFactory)
    {
        this.clientFactory = clientFactory;
        _settings = options?.Value ?? throw new ArgumentNullException(nameof(options));
    }

    public async Task OnGetAsync()
    {
        var client = this.clientFactory.CreateClient("MinhaApi");
        var resposta = await client.GetFromJsonAsync<List<Usuario>>("api/usuario");

        if (resposta != null)
            Usuarios = resposta;
    }
}