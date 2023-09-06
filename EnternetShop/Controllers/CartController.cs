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

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await cartService.GetCurrentCartAsync(userManager.GetUserId(User)));
        }


        public async Task<IActionResult> Add(Guid id)
        {
            var product = await  productService.GetProductAsync(id);
            await cartService.AddProductToCartAsync(product, userManager.GetUserId(User));
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Update(Dictionary<Guid, int> items)
        {
            foreach (var item in items)
            {
                await cartService.UpdateAmountAsync(userManager.GetUserId(User), item.Key, item.Value);
            }

            return RedirectToAction("Index");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteItem(Guid itemId)
        {
            await cartService.DeleteItemAsync(userManager.GetUserId(User), itemId);
            return RedirectToAction("Index");
        }
    }
}
