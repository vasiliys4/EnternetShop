using EnternetShop.Extention;
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

        public async Task<List<ProductViewModel>> GetAllProducts()
        {
            var products = await productRepository.GetAll();
            var productsViewModel = new List<ProductViewModel>();
            foreach (var product in products)
            {
                var productViewModel = product.ToProductViewModel();
                productsViewModel.Add(productViewModel);
            }
            return productsViewModel;
        }

        public async Task<ProductViewModel> GetProduct(Guid id)
        {
            var product = await productRepository.GetById(id);
            var productViewModel = product.ToProductViewModel();
            return productViewModel;
        }
        public async Task Create(ProductViewModel productViewModel)
        {
            if (productViewModel.File != null)
            {
                string path = "/images/products/" + productViewModel.File.FileName;
                productViewModel.File.CopyTo(new FileStream(appEnvironment.WebRootPath + path, FileMode.Create));
            }
            await productRepository.Create(productViewModel.ToProduct());
        }
    }
}
