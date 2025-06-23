using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace MinhaAplicacao.Pages.Livros;
public class EditarLivroModel : PageModel
{
    private readonly IHttpClientFactory clientFactory;

    private readonly ApiSettings _settings;

    public string ApiBaseUrl => _settings.BaseUrl;

    [BindProperty]
    public Livro Livro { get; set; } = new();

    public EditarLivroModel(IOptions<ApiSettings> options, IHttpClientFactory clientFactory)
    {
        this.clientFactory = clientFactory;
        _settings = options?.Value ?? throw new ArgumentNullException(nameof(options));
    }

    public async Task<IActionResult> OnGetAsync([FromQuery] int id)
    {
        var client = this.clientFactory.CreateClient("MinhaApi");
        var resposta = await client.GetFromJsonAsync<Livro>($"api/livro/{id}");

        if (resposta == null)
            return NotFound();

        Livro = resposta;

        ModelState.Clear();
        return Page();
    }
}