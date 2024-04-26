namespace StockExchange.Server.DTOs
{
    public class OrderDto
    {
        public string Symbol { get; set; }
        public string OrderType { get; set; }
        public int Quantity { get; set; }
    }
}
