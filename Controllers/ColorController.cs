using HandCrafter.Model;
using HandCrafter.Services;
using Microsoft.AspNetCore.Mvc;

namespace HandCrafter.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ColorController : Controller
    {
        private readonly ValidService _validService;
        private readonly ColorService _colorService;

        public ColorController(ValidService validService, ColorService colorService)
    => (_validService, _colorService) = (validService, colorService);


        [HttpGet("GetColor")]
        public IResult GetColor()
        {
            var allColors =  _colorService.GetColorsService();
            return Results.Json(allColors);
        }

        [HttpPost("PostColor")]
        public IActionResult PostColor(PostNameModel postColor)
        {
            if (!_validService.ValidString(postColor.Name))
            {
                return BadRequest();
            }
            _colorService.AddColorService(postColor.Name);
            return Ok();
        }

        [HttpDelete("DeleteColor")]
        public IActionResult DeleteColor(GetIdModel colorId)
        {
            if (!_validService.ValidId(colorId.Id))
            {
                return BadRequest();
            }
            _colorService.DeleteColorService(colorId.Id);
            return Ok();
        }
    }
}
