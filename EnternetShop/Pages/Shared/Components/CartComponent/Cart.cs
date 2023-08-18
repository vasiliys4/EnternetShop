using Azure.Core;
using EnternetShop.Models.Identity;
using EnternetShop.Models.ViewModels;
using EnternetShop.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace EnternetShop.Pages.Shared.Components.CartComponent
{
    public class Cart : ViewComponent
    {
        private readonly CartService cartService;
        private readonly SignInManager<UserForDB> signInManager;
        private readonly UserManager<UserForDB> userManager;

        public Cart(CartService basketService, SignInManager<UserForDB> signInManager, UserManager<UserForDB> userManager)
        {
            cartService = basketService;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(string userName)
        {
            var itemsCount = await GetBasketViewModel();
            return View(itemsCount.AllAmount);
        }

        private async Task<CartViewModel> GetBasketViewModel()
        {
            if (signInManager.IsSignedIn(HttpContext.User))
            {
                var userId = userManager.GetUserId(HttpContext.User);
                return await cartService.GetCurrentCartAsync(userId);
            }
            string anonymousId = GetBasketIdFromCookie();
            if (anonymousId == null)
                return new CartViewModel();
            return await cartService.GetCurrentCartAsync(anonymousId);
        }

        private string GetBasketIdFromCookie()
        {
            if (Request.Cookies.ContainsKey(Constants.BASKET_COOKIENAME))
            {
                return Request.Cookies[Constants.BASKET_COOKIENAME];
            }
            return null;
        }
    }
}
