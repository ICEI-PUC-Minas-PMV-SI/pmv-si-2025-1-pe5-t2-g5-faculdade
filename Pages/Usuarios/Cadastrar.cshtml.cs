using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace MinhaAplicacao.Pages.Usuarios;

public class CadastroModel : PageModel
{
    private readonly ApiSettings _settings;

    public string ApiBaseUrl => _settings.BaseUrl;

    public CadastroModel(IOptions<ApiSettings> options)
    {
        _settings = options?.Value ?? throw new ArgumentNullException(nameof(options));
    }
}