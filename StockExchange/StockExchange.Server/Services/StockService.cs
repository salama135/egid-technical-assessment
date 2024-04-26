namespace StockExchange.Server.Services
{
    public interface IStockService
    {
        decimal GetStockPrice(string symbol);
    }

    public class StockService : IStockService
    {
        public decimal GetStockPrice(string symbol)
        {
            // Implementation to get the stock price for the given symbol
            // ...

            return 50.0m; // Replace with actual stock price
        }
    }
}
