using EnternetShop.Data;
using Microsoft.EntityFrameworkCore;

namespace EnternetShop.Models.RepositoryModel
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDBContext _context;

        public CartRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Cart> AddProduct(Guid id, Guid productId)
        {
            var cart = await _context.Carts.FirstOrDefaultAsync(x => x.CartId == id);
            var prod = cart.CartItems.FirstOrDefault(x => x.ProductId == productId);
            if (prod != null) 
            {
                prod.Amount += 1;
            }
            else
            {
                cart.CartItems.Add(new CartItem { Cart = cart, ProductId = productId, Amount = 1, CartItemId = Guid.NewGuid() });
            }
            await _context.SaveChangesAsync();
            return cart;
        }

        public async Task<Cart> Create(string userId, Guid productId)
        {
            var cart = new Cart { UserId = userId };
            await _context.Carts.AddAsync(cart);
            cart.CartItems.Add(new CartItem { Cart = cart, ProductId = productId, Amount = 1, CartId = Guid.NewGuid() });
            await _context.SaveChangesAsync();
            return cart;
        }

        public async Task DeleteCart(string userId)
        {
            _context.Carts.Remove(await TryGetByUserId(userId));
            await _context.SaveChangesAsync();
        }

        public async Task DeleteItem(Cart existingCart, Product product)
        {
            var cart = await _context.Carts.FirstOrDefaultAsync(x => x.CartId == existingCart.CartId);
            var cartItem = cart.CartItems.FirstOrDefault(x => x.ProductId == product.Id);
            product.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();
        }

        public async Task<Cart> TryGetByUserId(string userId)
        {
            var cart = await _context.Carts.Include(c => c.CartItems).ThenInclude(p => p.Product).FirstOrDefaultAsync(i => i.UserId == userId);
            return cart;
        }

        public async Task Update(Cart existingCart)
        {
            var cart = await _context.Carts.FirstOrDefaultAsync(x => x.CartId == existingCart.CartId);
            cart = existingCart;
            await _context.SaveChangesAsync();
        }
    }
}
