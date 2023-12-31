﻿using EnternetShop.Models.RepositoryModel;
using EnternetShop.Models.ViewModels;
using EnternetShop.Models;
using EnternetShop.Extention;

namespace EnternetShop.Services
{
    public class CartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<CartViewModel> AddProductToCartAsync(ProductViewModel productViewModel, string userId)
        {
            var existingCart = await _cartRepository.TryGetByUserIdAsync(userId);
            var productVM = productViewModel.ToProduct();

            Cart cart;
            if (existingCart == null)
            {
                cart = await _cartRepository.CreateAsync(userId, productVM.Id);
            }
            else
            {
                cart = await _cartRepository.AddProductAsync(existingCart.CartId, productVM.Id);
            }

            var cartViewModel = new CartViewModel()
            {
                CartViewModelId = cart.CartId,
                Items = cart.CartItems.ToCartItemsViewModel()
            };
            return cartViewModel;
        }

        public async Task<CartViewModel> GetCurrentCartAsync(string userId)
        {
            var existingCart = await _cartRepository.TryGetByUserIdAsync(userId);
            if (existingCart != null)
            {
                return new CartViewModel()
                {
                    CartViewModelId = existingCart.CartId,
                    Items = existingCart.CartItems.ToCartItemsViewModel()
                };
            }

            return new CartViewModel()
            {
                Items = new List<CartItemViewModel>()
            };
        }

        public async Task DeleteItemAsync(string userId, Guid cartItemId)
        {
            var existingCart = await _cartRepository.TryGetByUserIdAsync(userId);
            var cartItem = existingCart.CartItems.FirstOrDefault(x => x.CartItemId == cartItemId);
            await _cartRepository.DeleteItemAsync(existingCart, cartItem.Product);
        }

        public async Task UpdateAmountAsync(string userId, Guid cartItemId, int amount)
        {
            var existingCart = await _cartRepository.TryGetByUserIdAsync(userId);
            if (existingCart != null)
            {
                var cartItem = existingCart.CartItems.FirstOrDefault(x => x.CartItemId == cartItemId);
                cartItem.Amount = amount;        
            }
            await _cartRepository.UpdateAsync(existingCart);
        }

        public async Task DeleteCartAsync(string userId)
        {
            var existingCart = await _cartRepository.TryGetByUserIdAsync(userId);
            if (existingCart == null) return;
            await _cartRepository.DeleteCartAsync(userId);
        }
    }
}
