using HandCrafter.DataBase;
using HandCrafter.Model;
using HandCrafter.Services;
using Microsoft.AspNetCore.Mvc;

namespace HandCrafter.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ValidService _validService;
        private readonly CategoryService _categoryService;

        public CategoryController(ValidService validService, CategoryService categoryService)
    => (_validService, _categoryService) = (validService, categoryService);


        [HttpGet("GetCategory")]
        public async Task<IResult> GetCategory()
        {
            var allCategories = _categoryService.GetCategoryService();
            return Results.Json(allCategories);
        }

        [HttpPost("PostCategory")]
        public IActionResult PostCategory(PostNameModel postCategory)
        {
            if (!_validService.ValidString(postCategory.Name))
            {
                return BadRequest();
            }
            _categoryService.AddCategoryService(postCategory.Name);
            return Ok();
        }

        [HttpDelete("DeleteCategory")]
        public IActionResult DeleteCategory(GetIdModel categoryId)
        {
            if (!_validService.ValidId(categoryId.Id))
            {
                return BadRequest();
            }
            _categoryService.DeleteCategoryService(categoryId.Id);
            return Ok();
        }
    }
}