using Microsoft.EntityFrameworkCore;
using StockExchange.Server.Context;
using StockExchange.Server.Model;

namespace StockExchange.Server.Repository
{
    public interface IStockRepository
    {
        Stock GetStockBySymbol(string symbol);
        IEnumerable<Stock> GetAllStocks();
        void AddStock(Stock stock);
        void UpdateStock(Stock stock);
        void DeleteStock(string symbol);
    }

    public class StockRepository : IStockRepository
    {
        private readonly StockExchangeApiContext _context;

        public StockRepository(StockExchangeApiContext context)
        {
            _context = context;
        }

        public Stock GetStockBySymbol(string symbol)
        {
            return _context.Stocks.FirstOrDefault(s => s.Symbol.Equals(symbol));
        }

        public IEnumerable<Stock> GetAllStocks()
        {
            return _context.Stocks.ToList();
        }

        public void AddStock(Stock stock)
        {
            _context.Stocks.Add(stock);
            _context.SaveChanges();
        }

        public void UpdateStock(Stock stock)
        {
            _context.Entry(stock).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteStock(string symbol)
        {
            var stock = _context.Stocks.FirstOrDefault(s => s.Symbol == symbol);
            _context.Stocks.Remove(stock);
            _context.SaveChanges();
        }
    }
}
