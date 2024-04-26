namespace StockExchange.Server.Model
{
    public class Order
    {
        public string Symbol { get; set; }
        public string OrderType { get; set; }
        public int Quantity { get; set; }
    }
}
