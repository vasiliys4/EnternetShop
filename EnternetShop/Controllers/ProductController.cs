using EnternetShop.Models;
using EnternetShop.Models.RepositoryModel;
using Microsoft.AspNetCore.Mvc;

namespace EnternetShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {            
            var products = await _productRepository.GetAll();
            if (products == null)
            {
                Redirect("Home/Index");
            }
            return View(products);
        }
    }
}
