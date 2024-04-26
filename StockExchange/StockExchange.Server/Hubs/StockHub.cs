using Microsoft.AspNetCore.SignalR;

namespace StockExchange.Server.Hubs
{
    public class StockHub : Hub
    {
        public async Task UpdateStockPrice(string symbol, decimal price)
        {
            await Clients.All.SendAsync("updateStockPrice", symbol, price);
        }
    }
}
