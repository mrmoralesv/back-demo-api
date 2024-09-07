using API.Dtos.Auth;

namespace API.Services.Auth;
public interface IUserService
{
    Task<string> RegisterAsync(RegisterDto model);
    Task<DatosUsuarioDto> GetTokenAsync(LoginDto model);

    Task<string> AddRoleAsync(AddRoleDto model);

    Task<DatosUsuarioDto> RefreshTokenAsync(string refreshToken);
    Task<DatosUsuarioDto> GetMe(string userName);
}
