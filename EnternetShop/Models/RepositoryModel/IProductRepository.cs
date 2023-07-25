namespace EnternetShop.Models.RepositoryModel
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAll();
        Product GetById(Guid id);
        Task Create(Product product);
    }
}
