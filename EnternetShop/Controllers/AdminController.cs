using EnternetShop.Models;
using EnternetShop.Models.Identity;
using EnternetShop.Models.RepositoryModel;
using EnternetShop.Models.ViewModels;
using EnternetShop.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EnternetShop.Controllers
{
    [Authorize(Roles ="admin")]
    public class AdminController : Controller
    {
        private readonly ProductService _productService;
        private readonly UserManager<UserForDB> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly OrderService _orderService;
        public AdminController(ProductService productService, UserManager<UserForDB> userManager, RoleManager<IdentityRole> roleManager, OrderService orderService)
        {
            _productService = productService;
            _userManager = userManager;
            _roleManager = roleManager;
            _orderService = orderService;
        }
        [HttpGet]
        public IActionResult AddProduct()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductViewModel product)
        {
            await _productService.CreateAsync(product);
            return RedirectToAction("AddProduct");
        }
        [HttpGet]
        public IActionResult GetUsers() => View(_userManager.Users.ToList());
        [HttpGet]
        public IActionResult GetRole() => View(_roleManager.Roles.ToList());
        [HttpGet]
        public IActionResult Create() => View();
        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var result = await _roleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(name);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                await _roleManager.DeleteAsync(role);
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult UserList() => View(_userManager.Users.ToList());
        [HttpGet]
        public async Task<IActionResult> Edit(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var allRoles = _roleManager.Roles.ToList();
                var model = new ChangeRoleViewModel
                {
                    UserId = user.Id,
                    UserEmail = user.Email,
                    UserRoles = userRoles,
                    AllRoles = allRoles
                };
                return View(model);
            }

            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(string userId, List<string> roles)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var addedRoles = roles.Except(userRoles);
                var removedRoles = userRoles.Except(roles);

                await _userManager.AddToRolesAsync(user, addedRoles);

                await _userManager.RemoveFromRolesAsync(user, removedRoles);

                return RedirectToAction("UserList");
            }

            return NotFound();
        }
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderService.GetAllAsync();
            if (orders == null)
            {
                Redirect("Admin/Index");
            }
            return View(orders);
        }
        [HttpGet]
        public async Task<IActionResult> GetOrder(Guid id)
        {
            var order = await _orderService.GetOrderAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        public async Task<IActionResult> ChangeOrderStatus(string status, Guid id)
        {
            await _orderService.ChangeStatusAsync(status, id);
            return RedirectToAction("GetOrder", new { id });
        }
        [HttpGet]
        public async Task<IActionResult> DeleteProduct()
        {
            var products = await _productService.GetAllProductsAsync();
            return View(products);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var productView = await _productService.GetProductAsync(id);
            await _productService.DeleteProductAsync(productView);
            return RedirectToAction("DeleteProduct");
        }
    }
}
