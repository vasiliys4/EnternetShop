using EnternetShop.Extention;
using EnternetShop.Models;
using EnternetShop.Models.RepositoryModel;
using EnternetShop.Models.ViewModels;

namespace EnternetShop.Services
{
    public class ProductService
    {
        private readonly IProductRepository productRepository;
        IWebHostEnvironment appEnvironment;

        public ProductService(IProductRepository productRepository, IWebHostEnvironment appEnvironment)
        {
            this.productRepository = productRepository;
            this.appEnvironment = appEnvironment;
        }

        public async Task<List<ProductViewModel>> GetAllProductsAsync()
        {
            var products = await productRepository.GetAllAsync();
            var productsViewModel = new List<ProductViewModel>();
            foreach (var product in products)
            {
                var productViewModel = product.ToProductViewModel();
                productsViewModel.Add(productViewModel);
            }
            return productsViewModel;
        }

        public async Task<ProductViewModel> GetProductAsync(Guid id)
        {
            var product = await productRepository.GetByIdAsync(id);
            var productViewModel = product.ToProductViewModel();
            return productViewModel;
        }
        public async Task CreateAsync(ProductViewModel productViewModel)
        {
            if (productViewModel.File != null)
            {
                string path = "/images/products/" + productViewModel.File.FileName;
                productViewModel.File.CopyTo(new FileStream(appEnvironment.WebRootPath + path, FileMode.Create));
            }
            await productRepository.CreateAsync(productViewModel.ToProduct());
        }
        public async Task<ProductViewModel> DeleteProductAsync(ProductViewModel productView)
        {
            var product = await productRepository.GetByIdAsync(productView.Id);
            await productRepository.DeleteProductAsync(product);
            return productView;
        }
        public async Task EditProductAsync(ProductViewModel productView)
        {
            if (productView.File != null)
            {
                string path = "/images/products/" + productView.File.FileName;
                productView.File.CopyTo(new FileStream(appEnvironment.WebRootPath + path, FileMode.Create));
            }
            await productRepository.EditProductAsync(productView.ToProduct());
        }
    }
}
