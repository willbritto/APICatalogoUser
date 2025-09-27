using Estudando_API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Estudando_API.Contexts;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }


    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Usuario> Usuarios  { get; set; }

    protected override void OnModelCreating(ModelBuilder builder) 
    {
       base.OnModelCreating(builder);
    }
}
