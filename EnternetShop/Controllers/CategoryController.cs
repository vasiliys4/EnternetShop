using EnternetShop.Data;
using EnternetShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnternetShop.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDBContext _db;
        public CategoryController(ApplicationDBContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> objList = _db.Categories;
            return View(objList);
        }
        public IActionResult Create()
        {
            return View();
        }
    }
}
