using StockExchange.Server.Model;

namespace StockExchange.Server.Services
{
    public interface IOrderService
    {
        void PlaceOrder(Order order);
    }

    public class OrderService : IOrderService
    {
        private readonly IStockService _stockService;

        public OrderService(IStockService stockService)
        {
            _stockService = stockService;
        }

        public void PlaceOrder(Order order)
        {
            // Validate the order
            if (order.Quantity <= 0)
            {
                throw new ArgumentException("Quantity must be greater than zero.");
            }

            // Get the stock price
            var stockPrice = _stockService.GetStockPrice(order.Symbol);

            // Process the order based on the order type
            if (order.OrderType.Equals("buy", StringComparison.OrdinalIgnoreCase))
            {
                // Calculate the total cost
                var totalCost = stockPrice * order.Quantity;

                // Check if there is enough balance in the account
                if (totalCost > AccountBalance)
                {
                    throw new InvalidOperationException("Insufficient balance to place the order.");
                }

                // Deduct the total cost from the account balance
                AccountBalance -= totalCost;

                // Place the buy order
                // ...
            }
            else if (order.OrderType.Equals("sell", StringComparison.OrdinalIgnoreCase))
            {
                // Check if there are enough shares in the account
                if (GetShareCount(order.Symbol) < order.Quantity)
                {
                    throw new InvalidOperationException("Not enough shares to place the order.");
                }

                // Add the total cost to the account balance
                AccountBalance += stockPrice * order.Quantity;

                // Place the sell order
                // ...
            }
            else
            {
                throw new ArgumentException("Invalid order type.");
            }
        }

        private decimal AccountBalance { get; set; }

        private int GetShareCount(string symbol)
        {
            // Implementation to get the share count for the given symbol
            // ...

            return 0; // Replace with actual share count
        }
    }
}
