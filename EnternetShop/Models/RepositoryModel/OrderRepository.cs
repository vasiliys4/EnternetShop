using EnternetShop.Data;
using Microsoft.EntityFrameworkCore;

namespace EnternetShop.Models.RepositoryModel
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDBContext _context;

        public OrderRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<Order>? TryGetByUserId(string userId)
        {
            var orders = await GetAll();
            foreach (var order in orders)
            {
                if (order.UserId == userId && order.Status == null)
                {
                    return order;
                }
            }
            return null;
        }

        public async Task<List<Order>> TryGetAllByUserId(string userId)
        {
            var allOrders = await GetAll();
            var orders = new List<Order>();
            foreach (var order in allOrders)
            {
                if (order.UserId == userId)
                {
                    orders.Add(order);
                }
            }
            return orders;
        }

        public async Task<List<Order>> GetAll()
        {
            return await _context.Order.AsNoTracking().Include(o => o.OrderItems).ThenInclude(p => p.Product).ToListAsync();
        }

        public async Task AddProduct(Guid orderId, Product product, int amount)
        {
            var order = await _context.Order.FirstOrDefaultAsync(x => x.Id == orderId);
            order.OrderItems.Add(new OrderItem { Order = order, Product = product, Amount = amount, Id = Guid.NewGuid() });
            _context.SaveChanges();
        }

        public async Task Create(string userId, Guid product, int amount)
        {
            var order = new Order { UserId = userId, Number = (await GetAll()).Count + 1 };
            await _context.Order.AddAsync(order);
            order.OrderItems.Add(new OrderItem { Order = order, ProductId = product, Amount = amount, Id = Guid.NewGuid() });
            await _context.SaveChangesAsync();
        }

        public async Task AddInformation(Order orderInfo)
        {
            var order = await _context.Order.FirstOrDefaultAsync(x => x.Id == orderInfo.Id);
            if (orderInfo.UserAddress != null)
                order.UserAddress = orderInfo.UserAddress;
            if (orderInfo.UserPhone != null)
                order.UserPhone = orderInfo.UserPhone;
            if (orderInfo.Status != null)
                order.Status = orderInfo.Status;
            if (orderInfo.DateTime != null)
                order.DateTime = orderInfo.DateTime;
            if (orderInfo.UserFirstName != null)
                order.UserFirstName = orderInfo.UserFirstName;
            if (orderInfo.UserLastName != null)
                order.UserLastName = orderInfo.UserLastName;
            if (orderInfo.UserEmail != null)
                order.UserEmail = orderInfo.UserEmail;
            if (orderInfo.UserComment != null)
                order.UserComment = orderInfo.UserComment;
            await _context.SaveChangesAsync();
        }

        public async Task ChangeStatus(string status, Guid orderId)
        {
            var order = await _context.Order.FirstOrDefaultAsync(x => x.Id == orderId);
            order.Status = status;
            await _context.SaveChangesAsync();
        }

        public async Task<Order> TryGetByOrderId(Guid orderId)
        {
            var orders = await GetAll();
            foreach (var order in orders)
            {
                if (order.Id == orderId)
                {
                    return order;
                }
            }
            return null;
        }
    }
}
