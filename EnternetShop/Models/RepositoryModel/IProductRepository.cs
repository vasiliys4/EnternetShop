namespace EnternetShop.Models.RepositoryModel
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAll();
        Task<Product> GetById(Guid id);
        Task Create(Product product);
    }
}
