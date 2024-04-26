using System.ComponentModel.DataAnnotations;

namespace StockExchange.Server.Model
{
    public class Stock
    {
        [Key]
        public Guid Id { get; set; }
        public string Symbol { get; set; }
        public double Price { get; set; }
        public DateTime Timestamp { get; set; }
        public int Quantity { get; set; }
    }
}
