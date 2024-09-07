using API.Dtos.Auth;
using API.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers;


public class UsuariosController :BaseApiController
{
    private readonly IUserService _userService;

    public UsuariosController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register"), Authorize]
    public async Task<ActionResult<string>> RegisterAsync(RegisterDto model)
    {
        var result = await _userService.RegisterAsync(model);
        return Ok(result);
    }

    [HttpPost("token")]
    public async Task<IActionResult> GetTokenAsync(LoginDto model)
    {
        var result = await _userService.GetTokenAsync(model);
        SetRefreshTokenInCookie(result.RefreshToken);
        return Ok(result);
    }

    [HttpPost("addrole")]
    public async Task<IActionResult> AddRoleAsync(AddRoleDto model)
    {
        var result = await _userService.AddRoleAsync(model);
        return Ok(result);
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] DatosUsuarioDto refreshToken)
    {
        var identity = HttpContext;
        var refreshTokenInCookie = Request.Cookies["refreshToken"];
        var response = await _userService.RefreshTokenAsync(refreshToken == null ? refreshTokenInCookie : refreshToken.RefreshToken);
        if (!string.IsNullOrEmpty(response.RefreshToken))
            SetRefreshTokenInCookie(response.RefreshToken);
        
        return Ok(response);
    }

    [HttpGet, Authorize]
    public async Task<IActionResult> GetMe()
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;

        if (identity != null)
        {
            //Obtenemos el claim con valor "userName" dentro de identity.
            var i = from claims in identity.Claims
                    from value in claims.Properties
                    where value.Value == "sub"
                    select new
                    {
                        claims = value.Value,
                        claims.Value
                    };

            var user = await _userService.GetMe(i.FirstOrDefault().Value);
            if (!string.IsNullOrEmpty(user.RefreshToken))
                SetRefreshTokenInCookie(user.RefreshToken);
            return Ok(user);
        }

        return Unauthorized("No existe");
    }

    private void SetRefreshTokenInCookie(string refreshToken)
    {
        var cookieOptions = new CookieOptions
        {
            SameSite = SameSiteMode.None,
            Domain = null,
            IsEssential = true,
            HttpOnly = true,
            Expires = DateTime.UtcNow.AddDays(1),
            Path = "/",
            Secure = true
        };
        Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
    }


}
