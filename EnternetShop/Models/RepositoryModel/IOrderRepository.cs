namespace EnternetShop.Models.RepositoryModel
{
    public interface IOrderRepository
    {
        Task<Order> TryGetByUserId(string userId);
        Task<List<Order>> TryGetAllByUserId(string userId);
        Task<List<Order>> GetAll();
        Task AddProduct(Guid orderId, Product product, int amount);
        Task Create(string userId, Guid product, int amount);
        Task AddInformation(Order orderInfo);
        Task ChangeStatus(string status, Guid orderId);
        Task<Order> TryGetByOrderId(Guid orderId);
    }
}
