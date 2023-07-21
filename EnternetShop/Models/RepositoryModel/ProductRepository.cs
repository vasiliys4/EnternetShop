using EnternetShop.Data;
using Microsoft.EntityFrameworkCore;

namespace EnternetShop.Models.RepositoryModel
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDBContext context;
        public ProductRepository(ApplicationDBContext context)
        {
            this.context = context;
        }
        public void Create(Product product)
        {
            context.Products.Add(product);
            context.SaveChanges();
        }

        public List<Product> GetAll()
        {
            return context.Products.AsNoTracking().ToList();
        }

        public Product GetById(Guid id)
        {
            var allProducts = GetAll();
            return allProducts.FirstOrDefault(p => p.ProductId == id);
        }
    }
}
