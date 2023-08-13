namespace EnternetShop.Models.ViewModels
{
    public class CartItemViewModel
    {
        public Guid CartItemViewModelId { get; set; }
        public ProductViewModel Product { get; set; }
        public int Amount { get; set; }

        public decimal Price => Amount * Product.Price;
    }
}
