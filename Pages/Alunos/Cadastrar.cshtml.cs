using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace MinhaAplicacao.Pages.Alunos;

public class CadastroAlunoModel : PageModel
{
    private readonly ApiSettings _settings;

    public string ApiBaseUrl => _settings.BaseUrl;

    public CadastroAlunoModel(IOptions<ApiSettings> options)
    {
        _settings = options?.Value ?? throw new ArgumentNullException(nameof(options));
    }
}