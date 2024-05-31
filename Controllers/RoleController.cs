using HandCrafter.DataBase;
using HandCrafter.Model;
using HandCrafter.Services;
using Microsoft.AspNetCore.Mvc;

namespace HandCrafter.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RoleController : Controller
    {
        private readonly ValidService _validService;
        private readonly RoleService _roleService;

        public RoleController(ValidService validService, RoleService roleService)
    => (_validService, _roleService) = (validService, roleService);


        [HttpGet("GetRole")]
        public async Task<IResult> GetRole()
        {
            var allRoles = await _roleService.GetRolsService();
            return Results.Json(allRoles);
        }

        [HttpPost("PostRole")]
        public async Task<IActionResult> PostRole(PostNameModel postRole)
        {
           if(!_validService.ValidString(postRole.Name))
            {
                return BadRequest();
            }
            await _roleService.AddRoleService(postRole.Name);
            return Ok();
        }

        [HttpDelete("DeleteRole")]
        public IActionResult DeleteRole(GetIdModel roleId)
        {
            if (!_validService.ValidId(roleId.Id))
            {
                return BadRequest();
            }
            _roleService.DeleteRoleService(roleId.Id);
            return Ok();
        }
    }
}
