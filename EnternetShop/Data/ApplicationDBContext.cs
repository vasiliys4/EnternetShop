using EnternetShop.Models;
using Microsoft.EntityFrameworkCore;

namespace EnternetShop.Data
{
    public class ApplicationDBContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }
    }
}
