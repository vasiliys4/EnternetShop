﻿namespace EnternetShop.Models
{
    public class CartItem
    {
        public Guid CartItemId { get; set; }
        public int Amount { get; set; }
        public Guid ProductId { get; set; }
        public Product? Product { get; set; }
        public Guid CartId { get; set; }
        public Cart? Cart { get; set; }
    }
}
