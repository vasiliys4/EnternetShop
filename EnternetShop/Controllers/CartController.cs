using EnternetShop.Models.Identity;
using EnternetShop.Models.RepositoryModel;
using EnternetShop.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EnternetShop.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly CartService cartService;
        private readonly ProductService productService;
        private readonly UserManager<UserForDB> userManager;

        public CartController(CartService cartService, ProductService productService, UserManager<UserForDB> userManager)
        {
            this.cartService = cartService;
            this.productService = productService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            return View(await cartService.GetCurrentCart(userManager.GetUserId(User)));
        }

        public async Task<IActionResult> Add(Guid id)
        {
            var product = await  productService.GetProduct(id);
            await cartService.AddProductToCart(product, userManager.GetUserId(User));
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(Dictionary<Guid, int> items)
        {
            foreach (var item in items)
            {
                await cartService.UpdateAmount(userManager.GetUserId(User), item.Key, item.Value);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteItem(Guid itemId)
        {
            await cartService.DeleteItem(userManager.GetUserId(User), itemId);
            return RedirectToAction("Index");
        }
    }
}
