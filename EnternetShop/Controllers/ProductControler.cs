using EnternetShop.Models.RepositoryModel;
using Microsoft.AspNetCore.Mvc;

namespace EnternetShop.Controllers
{
    public class ProductControler : Controller
    {
        private IProductRepository _productRepository;
        public ProductControler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        [HttpGet]
        public IActionResult Index(Guid id)
        {
            var product = _productRepository.GetById(id);
            return View(product);
        }
    }
}
