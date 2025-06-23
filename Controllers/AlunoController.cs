using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/aluno")]
public class AlunoController(AppDbContext context) : ControllerBase
{
    [HttpPost("registrar")]
    public async Task<IActionResult> Registrar([FromBody] Aluno aluno)
    {
        if (context.Aluno.Any(u => u.Email == aluno.Email))
        {
            return BadRequest("Aluno já existe!");
        }

        aluno.DataCadastro = DateTime.Now;

        context.Aluno.Add(aluno);
        await context.SaveChangesAsync();

        return Ok(new { mensagem = "Aluno registrado com sucesso!" });
    }

    [HttpPost("editar/{id}")]
    public async Task<IActionResult> Editar(int id, [FromBody] Aluno dados)
    {
        var aluno = await context.Aluno.FindAsync(id);
        if (aluno == null)
            return NotFound();

        aluno.Turma = dados.Turma;
        aluno.Curso = dados.Curso;
        aluno.Email = dados.Email;

        context.Aluno.Update(aluno);
        await context.SaveChangesAsync();

        return Redirect("/Alunos/Index");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAlunorById(int id)
    {
        var aluno = await context.Aluno.FindAsync(id);

        if (aluno == null)
        {
            return NotFound();
        }

        return Ok(aluno);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var alunos = await context.Aluno.ToListAsync();

        if (alunos == null)
        {
            return NotFound();
        }

        return Ok(alunos);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var aluno = await context.Aluno.FindAsync(id);

        if (aluno == null)
        {
            return NotFound();
        }

        context.Aluno.Remove(aluno);
        await context.SaveChangesAsync();

        return Ok();
    }
}
