namespace EnternetShop.Models.ViewModels
{
    public class OrderItemViewModel
    {
        public Guid Id { get; set; }
        public ProductViewModel Product { get; set; }
        public int Amount { get; set; }

        public decimal Price => Amount * Product.Price;
    }
}
