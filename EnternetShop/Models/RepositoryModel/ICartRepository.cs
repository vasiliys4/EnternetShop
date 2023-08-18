namespace EnternetShop.Models.RepositoryModel
{
    public interface ICartRepository
    {
        Task<Cart> TryGetByUserIdAsync(string userId);
        Task<Cart> CreateAsync(string userId, Guid product);
        Task<Cart> AddProductAsync(Guid id, Guid product);
        Task UpdateAsync(Cart existingCart);
        Task DeleteItemAsync(Cart existingCart, Product product);
        Task DeleteCartAsync(string userId);
    }
}
