using HandCrafter.DataBase;
using HandCrafter.Model;
using HandCrafter.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;



namespace HandCrafter.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController(UserService userService) => (_userService) = (userService);


        [HttpPost("UserPost")]
        public IActionResult UserPost(UseresRequestModel newUser)
        {
           _userService.AddUserService(newUser);
        
            return Ok();
        }

        [HttpGet("UsersGet")]
        public IResult UsersGet()
        {
            var users = _userService.GetUsersService();

            return Results.Json(users);
        }

        [Authorize]
        [HttpGet("OneUserGet")]
        public IResult OneUserGet()
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
            var user = _userService.GetUserService(id);
            return Results.Json(user);
        }
    }
}
