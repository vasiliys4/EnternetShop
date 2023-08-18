using EnternetShop.Models;
using EnternetShop.Models.RepositoryModel;
using EnternetShop.Services;
using Microsoft.AspNetCore.Mvc;

namespace EnternetShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductService _productService;
        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(Guid id)
        {            
            var products = await _productService.GetProductAsync(id);
            if (products == null)
            {
                Redirect("Home/Index");
            }
            return View(products);
        }
    }
}
