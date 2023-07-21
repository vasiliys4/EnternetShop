namespace EnternetShop.Models
{
    public class Product
    {
        public Guid ProductId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Size { get; set; }
        public string? ImagePath { get; set; }
    }
}
