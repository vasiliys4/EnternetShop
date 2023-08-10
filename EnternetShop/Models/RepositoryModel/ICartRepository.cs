namespace EnternetShop.Models.RepositoryModel
{
    public interface ICartRepository
    {
        Task<Cart> TryGetByUserId(string userId);
        Task<Cart> Create(string userId, Guid product);
        Task<Cart> AddProduct(Guid id, Guid product);
        Task Update(Cart existingCart);
        Task DeleteItem(Cart existingCart, Product product);
        Task DeleteCart(string userId);
    }
}
