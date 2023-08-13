namespace EnternetShop.Models.ViewModels
{
    public class CartViewModel
    {
        public Guid CartViewModelId { get; set; }
        public List<CartItemViewModel> Items { get; set; } = new List<CartItemViewModel>();

        public decimal FullPrice => Items.Sum(x => x.Price);
        public int AllAmount => Items.Sum(x => x.Amount);
    }
}
