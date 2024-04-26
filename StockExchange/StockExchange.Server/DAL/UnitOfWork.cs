using Microsoft.EntityFrameworkCore;
using StockExchange.Server.Context;
using StockExchange.Server.Repository;

namespace StockExchange.Server.DAL
{
    public interface IUnitOfWork
    {
        IOrderRepository OrderRepository { get; }
        IStockRepository StockRepository { get; }
        int Complete();
        void Dispose();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly StockExchangeApiContext _context;
        public IOrderRepository OrderRepository { get; private set; }
        public IStockRepository StockRepository { get; private set; }

        public UnitOfWork(StockExchangeApiContext context)
        {
            _context = context;
            OrderRepository = new OrderRepository(_context);
            StockRepository = new StockRepository(_context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
