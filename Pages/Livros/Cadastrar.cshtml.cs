using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace MinhaAplicacao.Pages.Livros;

public class CadastroLivroModel : PageModel
{
    private readonly ApiSettings _settings;

    public string ApiBaseUrl => _settings.BaseUrl;

    public CadastroLivroModel(IOptions<ApiSettings> options)
    {
        _settings = options?.Value ?? throw new ArgumentNullException(nameof(options));
    }
}