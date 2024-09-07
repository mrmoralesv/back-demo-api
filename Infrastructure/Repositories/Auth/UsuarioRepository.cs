using Core.Entities.Auth;
using Core.Interfaces.Auth;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Auth;

public class UsuarioRepository : GenericRepository<Usuario>, IUsuarioRepository
{
    public UsuarioRepository(b2m_DemosContext context) : base(context)
    {
    }

    public async Task<Usuario> GetByRefreshTokenAsync(string refreshToken)
    {
        return await _context.Usuarios
                    .Include(u => u.Roles)
                    .Include(u => u.RefreshTokens)
                    .FirstOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == refreshToken));
    }

    public async Task<Usuario> GetByUsernameAsync(string username)
    {
        return await _context.Usuarios
                            .Include(u => u.Roles)
                            .Include(u => u.RefreshTokens)
                            .FirstOrDefaultAsync(u => u.Username.ToLower() == username.ToLower());
    }
}

