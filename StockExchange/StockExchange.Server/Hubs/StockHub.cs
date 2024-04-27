using Microsoft.AspNetCore.SignalR;

namespace StockExchange.Server.Hubs
{
    public class StockHub : Hub
    {

        public StockHub()
        {
                
        }

        [HubMethodName("SendPrice")]
        public async Task SendPrice(string symbol, decimal price, DateTime timestamp)
        {
            await Clients.All.SendAsync("ReceivePrice", symbol, price, timestamp);
        }
    }
}
