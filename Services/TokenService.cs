using HandCrafter.Controllers;
using HandCrafter.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HandCrafter.Services
{
    public class TokenService
    {
        private readonly JWTSettings _options;

        private readonly ILogger<TokenService> _logger;
        private readonly UserService _userService;

        public TokenService(IOptions<JWTSettings> options, ILogger<TokenService> logger, UserService userService)
        {
            _options = options.Value;
            _logger = logger;
            _userService = userService;
        }
        
        public string GetRefreshTokenService(int minutes, int id)
        {
            var user = _userService.GetUserService(id);

            if (user == null)
            {
                return "Error";
            }
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim("Id", user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Role, user.Contact.IdRole.ToString()));

            var singingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));

            var jwt = new JwtSecurityToken
            (
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(minutes)),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(singingKey, SecurityAlgorithms.HmacSha256)
            );

            var resp = new JwtSecurityTokenHandler().WriteToken(jwt);
            return resp;
        }

        public string GetAccessTokenService(int minutes, int id)
        {

            var user = _userService.GetUserService(id);

            if (user == null)
            {
                return "Error";
            }
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName));
            claims.Add(new Claim("Id", user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Role, user.Contact.IdRole.ToString()));

            var singingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));

            var jwt = new JwtSecurityToken
            (
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(minutes)),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(singingKey, SecurityAlgorithms.HmacSha256)
            );

            var resp = new JwtSecurityTokenHandler().WriteToken(jwt);
            return resp;
        }
    }
}
