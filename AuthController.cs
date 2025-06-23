using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

[ApiController]
[Route("api/auth")]
public class AuthController(IConfiguration config, AppDbContext context) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Login([FromBody] UsuarioLoginModel model)
    {
        var usuario = context.Usuario.SingleOrDefault(u => u.Email == model.Email);

        if (usuario == null || !BCrypt.Net.BCrypt.Verify(model.Senha, usuario.Senha))
        {
            return Unauthorized("Credenciais inválidas.");
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.Email, usuario.Email),
            new Claim(ClaimTypes.Name, usuario.Nome)
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(identity),
            new AuthenticationProperties { IsPersistent = true });

        var token = GenerateJwtToken(usuario);
        return Ok(new { token });
    }

    private string GenerateJwtToken(Usuario user)
    {
        var key = Encoding.ASCII.GetBytes(config["JwtSettings:SecretKey"] ?? string.Empty);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, user.Id.ToString()) }),
            Expires = DateTime.UtcNow.AddHours(1),
            Issuer = config["JwtSettings:Issuer"],
            Audience = config["JwtSettings:Audience"],
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}

public class UsuarioLoginModel
{
    public string Email { get; set; }
    
    public string Senha { get; set; }
}