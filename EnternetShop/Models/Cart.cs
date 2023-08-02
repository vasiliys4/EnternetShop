namespace EnternetShop.Models
{
    public class Cart
    {
        public Guid CartId { get; set; }
        public List<Product>? Products { get; set; }
        public decimal? Price { get; set; }
    }
}
