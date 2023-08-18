using EnternetShop.Data;
using Microsoft.EntityFrameworkCore;

namespace EnternetShop.Models.RepositoryModel
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDBContext _context;
        public ProductRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetByIdAsync(Guid id)
        {            
            return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
