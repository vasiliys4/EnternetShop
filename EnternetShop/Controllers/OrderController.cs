using EnternetShop.Models;
using EnternetShop.Models.Identity;
using EnternetShop.Models.ViewModels;
using EnternetShop.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EnternetShop.Controllers
{
    public class OrderController : Controller
    {
        private readonly OrderService orderService;
        private readonly ProductService productService;
        private readonly UserManager<UserForDB> userManager;
        private readonly CartService cartService;
        public OrderController(CartService cartService, OrderService orderService, ProductService productService, UserManager<UserForDB> userManager)
        {
            this.orderService = orderService;
            this.userManager = userManager;
            this.productService = productService;
            this.cartService = cartService;
        }

        [HttpGet]
        public IActionResult Thanks()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(Dictionary<Guid, int> items)
        {
            var orderViewModel = new OrderViewModel();
            foreach (var item in items)
            {
                var product = await productService.GetProductAsync(item.Key);
                orderViewModel.OrderItems.Add(new OrderItemViewModel { Product = product, Amount = item.Value, Id = Guid.NewGuid() });
            }
            return View(orderViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> SaveOrder(Dictionary<Guid, int> items, OrderViewModel orderViewModel)
        {            
            foreach (var item in items)
            {
                var product = await productService.GetProductAsync(item.Key);
                await orderService.AddProductToOrderAsync(userManager.GetUserId(User), product, item.Value);
            }
            await orderService.AddInformationAsync(userManager.GetUserId(User), orderViewModel);
            await cartService.DeleteCartAsync(userManager.GetUserId(User));
            return RedirectToAction("Thanks");
        }
    }
}
