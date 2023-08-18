using EnternetShop.Extention;
using EnternetShop.Models.RepositoryModel;
using EnternetShop.Models.ViewModels;

namespace EnternetShop.Services
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task AddProductToOrderAsync(string userId, ProductViewModel productViewModel, int amount)
        {
            var existingOrder = await _orderRepository.TryGetByUserIdAsync(userId);
            var product = productViewModel.ToProduct();
            if (existingOrder == null)
            {
                await _orderRepository.CreateAsync(userId, product.Id, amount);
            }
            else
            {
                await _orderRepository.AddProductAsync(existingOrder.Id, product, amount);
            }
        }

        public async Task AddInformationAsync(string userId, OrderViewModel orderViewModelInfo)
        {
            var orderInfo = orderViewModelInfo.ToOrderInfo();
            orderInfo.Id = (await _orderRepository.TryGetByUserIdAsync(userId)).Id;
            await _orderRepository.AddInformationAsync(orderInfo);
        }

        public async Task<List<OrderViewModel>> GetAllByUserId(string userId)
        {
            var userOrders = await _orderRepository.TryGetAllByUserIdAsync(userId);
            var orderViewModels = new List<OrderViewModel>();
            if (userOrders != null)
            {
                foreach (var userOrder in userOrders)
                {
                    orderViewModels.Add(userOrder.ToOrderViewModel());
                }
            }
            return orderViewModels;
        }

        public async Task<List<OrderViewModel>> GetAllAsync()
        {
            var allOrders = await _orderRepository.GetAllAsync();
            var orderViewModels = new List<OrderViewModel>();
            if (allOrders != null)
            {
                foreach (var order in allOrders)
                {
                    orderViewModels.Add(order.ToOrderViewModel());
                }
            }
            return orderViewModels;
        }

        public async Task<OrderViewModel> GetOrderAsync(Guid id)
        {
            return (await _orderRepository.TryGetByOrderIdAsync(id)).ToOrderViewModel();
        }

        public async Task ChangeStatusAsync(string status, Guid id)
        {
            var existingOrder = await _orderRepository.TryGetByOrderIdAsync(id);
            await _orderRepository.ChangeStatusAsync(status, existingOrder.Id);
        }
    }
}
