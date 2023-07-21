namespace EnternetShop.Models.RepositoryModel
{
    public interface IProductRepository
    {
        List<Product> GetAll();
        Product GetById(Guid id);
        void Create(Product product);
    }
}
