using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace MinhaAplicacao.Pages;

public class LoginModel : PageModel
{
    private readonly ApiSettings _settings;

    public string ApiBaseUrl => _settings.BaseUrl;

    public LoginModel(IOptions<ApiSettings> options)
    {
        _settings = options?.Value ?? throw new ArgumentNullException(nameof(options));
    }
}