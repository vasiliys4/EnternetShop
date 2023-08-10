namespace EnternetShop.Models
{
    public class Cart
    {
        public Guid CartId { get; set; }
        public string? UserId { get; set; }
        public List<CartItem>? CartItems { get; set; }
        public Cart() => CartItems = new List<CartItem>();
    }
}
