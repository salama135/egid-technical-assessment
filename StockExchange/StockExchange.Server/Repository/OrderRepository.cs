using Microsoft.EntityFrameworkCore;
using StockExchange.Server.Context;
using StockExchange.Server.Model;

namespace StockExchange.Server.Repository
{
    public interface IOrderRepository
    {
        void AddOrder(Order order);
        Order GetOrder(int id);
        IEnumerable<Order> GetOrders();
        void UpdateOrder(Order order);
        void DeleteOrder(int id);
    }

    public class OrderRepository : IOrderRepository
    {
        private readonly StockExchangeApiContext _context;

        public OrderRepository(StockExchangeApiContext context)
        {
            _context = context;
        }

        public void AddOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public Order GetOrder(int id)
        {
            return _context.Orders.Find(id);
        }

        public IEnumerable<Order> GetOrders()
        {
            return _context.Orders.ToList();
        }

        public void UpdateOrder(Order order)
        {
            _context.Entry(order).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteOrder(int id)
        {
            var order = _context.Orders.Find(id);
            _context.Orders.Remove(order);
            _context.SaveChanges();
        }
    }
}
