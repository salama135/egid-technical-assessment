using System.ComponentModel.DataAnnotations;

namespace StockExchange.Server.Model
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }
        public string Symbol { get; set; }
        public string OrderType { get; set; }
        public int Quantity { get; set; }
    }
}
