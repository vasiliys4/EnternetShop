using System.ComponentModel.DataAnnotations.Schema;

namespace EnternetShop.Models
{
    public class Product
    {
        public Guid ProductId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public int Size { get; set; }
        //public short Count { get; set; }
        public string? ImagePath { get; set; }
    }
}
