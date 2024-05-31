using HandCrafter.Model;
using HandCrafter.Services;
using Microsoft.AspNetCore.Mvc;

namespace HandCrafter.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CompositionController : Controller
    {
        private readonly ValidService _validService;
        private readonly CompositionService _compositionService;

        public CompositionController(ValidService validService, CompositionService compositionService)
    => (_validService, _compositionService) = (validService, compositionService);


        [HttpGet("GetComposition")]
        public async Task<IResult> GetComposition()
        {
            var allCategories = _compositionService.GetCompositionsService();
            return Results.Json(allCategories);
        }

        [HttpPost("PostComposition")]
        public IActionResult PostComposition(PostNameModel postComposition)
        {
            if (!_validService.ValidString(postComposition.Name))
            {
                return BadRequest();
            }
            _compositionService.AddCompositionService(postComposition.Name);
            return Ok();
        }

        [HttpDelete("DeleteComposition")]
        public IActionResult DeleteComposition(GetIdModel compositionId)
        {
            if (!_validService.ValidId(compositionId.Id))
            {
                return BadRequest();
            }
            _compositionService.DeleteCompositionService(compositionId.Id);
            return Ok();
        }
    }
}
