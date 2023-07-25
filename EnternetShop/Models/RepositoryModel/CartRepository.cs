using EnternetShop.Data;

namespace EnternetShop.Models.RepositoryModel
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDBContext _context;

        public CartRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Cart> AddProduct(Guid id, Product product)
        {
            throw new NotImplementedException();
        }

        public async Task<Cart> Create(string userId, Product product)
        {
            throw new NotImplementedException();
        }

        public async void DeleteCart(string userId)
        {
            throw new NotImplementedException();
        }

        public async void DeleteItem(Cart existingCart, Product product)
        {
            throw new NotImplementedException();
        }

        public async Task<Cart> TryGetByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public async void Update(Cart existingCart)
        {
            throw new NotImplementedException();
        }
    }
}
