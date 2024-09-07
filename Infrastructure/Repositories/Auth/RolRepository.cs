using Core.Entities.Auth;
using Core.Interfaces.Auth;
using Infrastructure.Data;
namespace Infrastructure.Repositories.Auth;

public class RolRepository : GenericRepository<Rol>, IRolRepository
{
    public RolRepository(b2m_DemosContext context) : base(context)
    {
    }
}
