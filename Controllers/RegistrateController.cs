using HandCrafter.Services;
using HandCrafter.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HandCrafter.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/Registration")]
    public class RegistrateController : ControllerBase
    {

        private readonly ILogger<RegistrateController> _logger;
        private readonly TokenService _tokenService;

        public RegistrateController(ILogger<RegistrateController> logger, TokenService tokenService)
        {
            _logger = logger;
            _tokenService = tokenService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("GetRefreshToken")]
        public string GetRefreshToken(int minutes, int id)
        {
            
            var resp = _tokenService.GetRefreshTokenService(minutes, id);
            return resp;
        }

        [HttpPost]
        [Route("GetAccessToken")]
        public string GetAccessToken(int minutes)
        {
            string token = HttpContext.Request.Headers["Authorization"];
            // Удаляем "Bearer " из строки токена
            token = token.Substring("Bearer ".Length).Trim();

            // Создаем объект JwtSecurityTokenHandler
            var tokenHandler = new JwtSecurityTokenHandler();

            // Декодируем токен
            var decodedToken = tokenHandler.ReadJwtToken(token);

            // Получаем имя из токена
            var idClaim = decodedToken.Claims.FirstOrDefault(c => c.Type == "Id");
            int id;
            int.TryParse(idClaim.Value, out id);

            var resp = _tokenService.GetAccessTokenService(minutes, id);
            return resp;
        }


    }
}
