using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Core.Entities.Auth;

namespace Infrastructure.Data;

public class b2m_DemosContext : DbContext
{
    public b2m_DemosContext()
    {
    }

    public b2m_DemosContext(DbContextOptions<b2m_DemosContext> options) : base(options)
    {
    }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Rol> Roles { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //Configuraciones
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

    }

}
