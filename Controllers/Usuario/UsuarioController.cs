using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/usuario")]
public class UsuarioController(AppDbContext context) : ControllerBase
{
    [HttpPost("registrar")]
    public async Task<IActionResult> Registrar([FromBody] Usuario usuario)
    {
        if (context.Usuario.Any(u => u.Email == usuario.Email))
        {
            return BadRequest("Usuário já existe!");
        }

        usuario.Senha = BCrypt.Net.BCrypt.HashPassword(usuario.Senha);
        usuario.DataCadastro = DateTime.Now;

        context.Usuario.Add(usuario);
        await context.SaveChangesAsync();

        return Ok(new { mensagem = "Usuário registrado com sucesso!" });
    }

    [HttpPost("editar/{id}")]
    public async Task<IActionResult> Editar(int id, [FromBody] Usuario dados)
    {
        var usuario = await context.Usuario.FindAsync(id);
        if (usuario == null)
            return NotFound();

        usuario.Nome = dados.Nome;
        usuario.Email = dados.Email;

        context.Usuario.Update(usuario);
        await context.SaveChangesAsync();

        return Redirect("/Usuarios/Index");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var usuario = await context.Usuario.FindAsync(id);

        if (usuario == null)
        {
            return NotFound();
        }

        return Ok(usuario);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var usuarios = await context.Usuario.ToListAsync();

        if (usuarios == null)
        {
            return NotFound();
        }

        return Ok(usuarios);
    }
}
