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
            var query = from p in _context.Products
                        select p;
            return await query.ToListAsync();
            //return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetByIdAsync(Guid id)
        {            
            return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<Product> DeleteProductAsync(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return product;
        }
        public async Task EditProductAsync(Product product)
        {
            var oldProduct = await _context.Products.FirstOrDefaultAsync(x => x.Id == product.Id);
            oldProduct.Name = product.Name;
            oldProduct.Description = product.Description;
            oldProduct.Price = product.Price;
            oldProduct.ImagePath = product.ImagePath;
            await _context.SaveChangesAsync();
        }
    }
}
