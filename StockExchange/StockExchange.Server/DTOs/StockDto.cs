using StockExchange.Server.Model;

namespace StockExchange.Server.DTOs
{
    public class StockDto
    {
        public string Symbol { get; set; }
        public double Price { get; set; }
        public DateTime Timestamp { get; set; }

        public StockDto(Stock stock)
        {
            this.Symbol = stock.Symbol;
            this.Price = stock.Price;
            this.Timestamp = stock.Timestamp;
        }
    }
}
