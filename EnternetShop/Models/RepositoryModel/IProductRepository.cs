namespace EnternetShop.Models.RepositoryModel
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(Guid id);
        Task CreateAsync(Product product);
    }
}
