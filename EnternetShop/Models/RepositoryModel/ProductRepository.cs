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

        public async Task Create(Product product)
        {
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();
        }

        public async Task<List<Product>> GetAll()
        {
            return await context.Products.ToListAsync();
        }

        public async Task<Product> GetById(Guid id)
        {            
            return await context.Products.FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
