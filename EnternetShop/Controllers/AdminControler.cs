using EnternetShop.Models;
using EnternetShop.Models.RepositoryModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnternetShop.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminControler : Controller
    {
        private readonly IProductRepository _productRepository;
        public AdminControler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            _productRepository.Create(product);
            return RedirectToAction("AddProduct");
        }
    }
}
