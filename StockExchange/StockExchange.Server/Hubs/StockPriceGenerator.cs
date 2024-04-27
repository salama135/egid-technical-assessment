using Microsoft.AspNetCore.SignalR;

namespace StockExchange.Server.Hubs
{
    public class StockPriceGenerator
    {
        private readonly IHubContext<StockHub> _stockHub;
        private Timer _timer;

        public StockPriceGenerator(IHubContext<StockHub> stockHub)
        {
            _stockHub = stockHub;
        }

        public void Start()
        {
            _timer = new Timer(UpdateStockPrice, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
        }

        private void UpdateStockPrice(object state)
        {
            // Generating a random price for simplicity
            decimal price = new Random().Next(101, 113);
            _stockHub.Clients.All.SendAsync("ReceivePrice", "GOOGL", price, DateTime.Now);
        }
    }
}
