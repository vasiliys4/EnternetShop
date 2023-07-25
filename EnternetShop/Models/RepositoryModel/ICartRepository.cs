namespace EnternetShop.Models.RepositoryModel
{
    public interface ICartRepository
    {
        Task<Cart> TryGetByUserId(string userId);
        Task<Cart> Create(string userId, Product product);
        Task<Cart> AddProduct(Guid id, Product product);
        void Update(Cart existingCart);
        void DeleteItem(Cart existingCart, Product product);
        void DeleteCart(string userId);
    }
}
