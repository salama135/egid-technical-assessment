using System.ComponentModel.DataAnnotations;

namespace StockExchange.Server.Model
{
    public class Stock
    {
        [Key]
        public string Symbol { get; set; }
        public double Price { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
