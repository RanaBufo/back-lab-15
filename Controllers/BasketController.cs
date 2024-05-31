using HandCrafter.Model;
using HandCrafter.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HandCrafter.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BasketController : Controller
    {
        private readonly BascketService _basketService;
        public BasketController(BascketService basketService) => (_basketService) = (basketService);


        [Authorize]
        [HttpPost("BasketPost")]
        public IResult BasketPost(BasketRequest newItem)
        {
            if (_basketService.addNeItemBascketService(newItem))
            {
                return Results.Ok();
            }
            return Results.NotFound();
        }


        [Authorize]
        [HttpGet("BasketGet")]
        public IActionResult BasketGet(int id)
        {
            var allItems = _basketService.getAllItemsBasketService(id);
            if(allItems.Count > 0)
            {
                return Json(allItems);
            }

            return Json(allItems);
            return NotFound();
        }


        [Authorize]
        [HttpGet("BasketItemGet")]
        public IActionResult BasketItemGet(int idProduct, int idUser)
        {
            var allItems = _basketService.getItemBasketService(idProduct, idUser);
            if (allItems != null)
            {
                return Json(allItems);
            }
            return NotFound();
        }


        [Authorize]
        [HttpPut("BasketPut")]
        public IActionResult BasketPut(BasketQuantityRequest basketQuantity)
        {
            if (_basketService.updateQuentityBasketService(basketQuantity))
            {
                return Ok();
            }
            return NotFound();
        }

        [HttpDelete("BasketDelete")]
        public IActionResult BasketDelete(int id)
        {
            if (_basketService.deleteBasketItemService(id))
            {
                return Ok();
            }
            return NotFound();
        }

        [HttpDelete("BasketDeleteUserId")]
        public IActionResult BasketDeleteByUserId(int id)
        {
            if (_basketService.deleteBasketIdUserService(id))
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
