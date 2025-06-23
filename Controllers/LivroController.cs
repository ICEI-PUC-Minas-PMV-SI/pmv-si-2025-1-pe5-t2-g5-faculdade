using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/livro")]
public class LivroController(AppDbContext context) : ControllerBase
{
    [HttpPost("registrar")]
    public async Task<IActionResult> Registrar([FromBody] Livro livro)
    {
        context.Livro.Add(livro);
        await context.SaveChangesAsync();

        return Ok(new { mensagem = "Livro registrado com sucesso!" });
    }

    [HttpPost("editar/{id}")]
    public async Task<IActionResult> Editar(int id, [FromBody] Livro dados)
    {
        var livro = await context.Livro.FindAsync(id);
        if (livro == null)
            return NotFound();

        livro.Titulo = dados.Titulo;
        livro.Autor = dados.Autor;
        livro.Edicao = dados.Edicao;
        livro.Ano = dados.Ano;

        context.Livro.Update(livro);
        await context.SaveChangesAsync();

        return Redirect("/Livros/Index");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetLivroById(int id)
    {
        var livro = await context.Livro.FindAsync(id);

        if (livro == null)
        {
            return NotFound();
        }

        return Ok(livro);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var livros = await context.Livro.ToListAsync();

        if (livros == null)
        {
            return NotFound();
        }

        return Ok(livros);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var livro = await context.Livro.FindAsync(id);

        if (livro == null)
        {
            return NotFound();
        }

        context.Livro.Remove(livro);
        await context.SaveChangesAsync();

        return Ok();
    }
}
