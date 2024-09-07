using Core.Entities.Auth;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data;

public class b2m_DemosContextSeed
{

    public static async Task SeedRolesAsync(b2m_DemosContext context, ILoggerFactory loggerFactory)
    {
        try
        {
            if (!context.Roles.Any())
            {
                var roles = new List<Rol>()
                        {
                            new Rol{ Nombre="Administrador"},
                            new Rol{ Nombre="Gerente"},
                            new Rol{ Nombre="Empleado"},
                        };
                context.Roles.AddRange(roles);
                await context.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            var logger = loggerFactory.CreateLogger<b2m_DemosContextSeed>();
            logger.LogError(ex.Message);
        }
    }


    public static async Task SeedCreateUserAdminAsync(b2m_DemosContext context, ILoggerFactory loggerFactory)
    {
        try
        {
            if (!context.Usuarios.Any())
            {
                Usuario usuario = new Usuario()
                {
                    Nombres = "Administrador",
                    ApellidoPaterno = "SINCI",
                    ApellidoMaterno = "B2M",
                    Email = "mverduzco@sinci.com",
                    Username = "Administrador",
                    Password = "AQAAAAEAACcQAAAAEHWBkw41PrV1kqnmAg1sQ1VNMlUxE2WOrM1GkxyjUfOKBTZVQzSkXkBwm7i9ke1izQ==",
                    PersonalId = 0,
                    UsuariosRoles = new List<UsuariosRoles>() { new UsuariosRoles { UsuarioId = 1, RolId = 1 } }

                };

                context.Usuarios.Add(usuario);
                await context.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            var logger = loggerFactory.CreateLogger<b2m_DemosContextSeed>();
            logger.LogError(ex.Message);
        }
    }
       
}
