using Core.Interfaces;
using Core.Interfaces.Auth;
using Infrastructure.Data;
using Infrastructure.Repositories.Auth;

namespace Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly b2m_DemosContext _context;

    private IRolRepository _roles;
    private IUsuarioRepository _usuarios;
    public UnitOfWork(b2m_DemosContext context)
    {
        _context = context;
    }

    public IRolRepository Roles
    {
        get
        {
            if (_roles == null)
            {
                _roles = new RolRepository(_context);
            }
            return _roles;
        }
    }


    public IUsuarioRepository Usuarios
    {
        get
        {
            if (_usuarios == null)
            {
                _usuarios = new UsuarioRepository(_context);
            }
            return _usuarios;
        }
    }

    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
