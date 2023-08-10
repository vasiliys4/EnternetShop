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

        public async Task AddProductToOrder(string userId, ProductViewModel productViewModel, int amount)
        {
            var existingOrder = await _orderRepository.TryGetByUserId(userId);
            var product = productViewModel.ToProduct();
            if (existingOrder == null)
            {
                await _orderRepository.Create(userId, product.Id, amount);
            }
            else
            {
                await _orderRepository.AddProduct(existingOrder.Id, product, amount);
            }
        }

        public async Task AddInformation(string userId, OrderViewModel orderViewModelInfo)
        {
            var orderInfo = orderViewModelInfo.ToOrderInfo();
            orderInfo.Id = (await _orderRepository.TryGetByUserId(userId)).Id;
            await _orderRepository.AddInformation(orderInfo);
        }

        public async Task<List<OrderViewModel>> GetAllByUserId(string userId)
        {
            var userOrders = await _orderRepository.TryGetAllByUserId(userId);
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

        public async Task<List<OrderViewModel>> GetAll()
        {
            var allOrders = await _orderRepository.GetAll();
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

        public async Task<OrderViewModel> GetOrder(Guid id)
        {
            return (await _orderRepository.TryGetByOrderId(id)).ToOrderViewModel();
        }

        public async Task ChangeStatus(string status, Guid id)
        {
            var existingOrder = await _orderRepository.TryGetByOrderId(id);
            await _orderRepository.ChangeStatus(status, existingOrder.Id);
        }
    }
}
