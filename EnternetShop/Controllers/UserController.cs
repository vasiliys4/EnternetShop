using EnternetShop.Extention;
using EnternetShop.Models;
using EnternetShop.Models.Identity;
using EnternetShop.Models.ViewModels;
using EnternetShop.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EnternetShop.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserManager<UserForDB> _userManager;
        private readonly OrderService _orderService;
        public UserController(UserManager<UserForDB> userManager, OrderService orderService)
        {
            _userManager = userManager;
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));
            var model = new User { Id = user.Id, Email = user.Email, Name = user.FirstName, Surname = user.LastName, PhoneNumber = user.PhoneNumber };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var model = new User { Id = user.Id, Email = user.Email, Name = user.FirstName, Surname = user.LastName, PhoneNumber = user.PhoneNumber };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(User model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    user.Email = model.Email;
                    user.UserName = model.Email;
                    user.FirstName = model.Name;
                    user.LastName = model.Surname;
                    user.PhoneNumber = model.PhoneNumber;
                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        result.AddErrorsTo(ModelState);
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var model = new User { Id = user.Id, Email = user.Email, Name = user.FirstName };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(User model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    var result = await _userManager.ChangePasswordAsync(user, model.Password, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        result.AddErrorsTo(ModelState);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Пользователь не найден");
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));
            var orders = new UserOrdersViewModel
            {
                Orders = await _orderService.GetAllByUserId(_userManager.GetUserId(User)),
                User = new User { Id = user.Id, Email = user.Email, Name = user.FirstName, Surname = user.LastName, PhoneNumber = user.PhoneNumber, }
            };
            return View(orders);
        }
    }
}
