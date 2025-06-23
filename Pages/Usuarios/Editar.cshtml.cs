using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace MinhaAplicacao.Pages.Usuarios;
public class EditarModel : PageModel
{
    private readonly IHttpClientFactory clientFactory;

    private readonly ApiSettings _settings;

    public string ApiBaseUrl => _settings.BaseUrl;

    [BindProperty]
    public Usuario Usuario { get; set; } = new();

    public EditarModel(IOptions<ApiSettings> options, IHttpClientFactory clientFactory)
    {
        this.clientFactory = clientFactory;
        _settings = options?.Value ?? throw new ArgumentNullException(nameof(options));
    }

    public async Task<IActionResult> OnGetAsync([FromQuery] int id)
    {
        var client = this.clientFactory.CreateClient("MinhaApi");
        var resposta = await client.GetFromJsonAsync<Usuario>($"api/usuario/{id}");

        if (resposta == null)
            return NotFound();

        Usuario = resposta;

        ModelState.Clear();
        return Page();
    }
}