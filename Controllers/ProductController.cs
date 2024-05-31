using HandCrafter.Model;
using HandCrafter.Services;
using Microsoft.AspNetCore.Mvc;

namespace HandCrafter.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private readonly ProductService _productService;
        public ProductController(ProductService productService) => (_productService) = (productService);

        [HttpPost("ProductPost")]
        public IActionResult ProductPost(ProductRequestModel newProduct)
        {
            if (newProduct == null)
            {
                return NoContent();
            }
            _productService.addProductService(newProduct);
            return Ok();
        }

        [HttpGet("ProductsGet")]
        public IResult ProductsGet()
        {
            var allProducts = _productService.getProductsService();
            return Results.Json(allProducts);
        }


        [HttpGet("ProductByIdGet")]
        public IResult ProductByIdGet(int id)
        {
            var Product = _productService.getProductByIdService(id);
            if (Product == null)
            {
                return Results.NoContent();
            }
            return Results.Json(Product);
        }

        [HttpGet("ProductByCategoryGet")]
        public IResult ProductByCategoryGet(int id)
        {
            var Product = _productService.getProductsByCategoriesService(id);
            if (Product == null)
            {
                return Results.NoContent();
            }
            return Results.Json(Product);
        }


    }
}
