using EnternetShop.Models.Identity;

namespace EnternetShop.Models.ViewModels
{
    public class UserOrdersViewModel
    {
        public List<OrderViewModel> Orders { get; set; }
        public User User { get; set; }
    }
}
