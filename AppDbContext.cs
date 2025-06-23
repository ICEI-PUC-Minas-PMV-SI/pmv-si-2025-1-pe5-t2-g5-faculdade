using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

    public DbSet<Usuario> Usuario { get; set; }
    public DbSet<Livro> Livro { get; set; }
    public DbSet<Aluno> Aluno { get; set; }
}
