namespace EnternetShop.Models.RepositoryModel
{
    public interface IOrderRepository
    {
        Task<Order> TryGetByUserIdAsync(string userId);
        Task<List<Order>> TryGetAllByUserIdAsync(string userId);
        Task<List<Order>> GetAllAsync();
        Task AddProductAsync(Guid orderId, Product product, int amount);
        Task CreateAsync(string userId, Guid product, int amount);
        Task AddInformationAsync(Order orderInfo);
        Task ChangeStatusAsync(string status, Guid orderId);
        Task<Order> TryGetByOrderIdAsync(Guid orderId);
    }
}
