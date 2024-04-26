using Microsoft.EntityFrameworkCore;
using StockExchange.Server.Model;
using System;

namespace StockExchange.Server.Context
{
    public class StockExchangeApiContext : DbContext
    {
        public DbSet<Stock> Stocks { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Stock>()
                .HasIndex(s => new { s.Symbol })
                .IsUnique(true);
        }

        public StockExchangeApiContext(DbContextOptions<StockExchangeApiContext> options) : base(options)
        {
            //Orders.Add(new Order { Symbol = "AAPL", OrderType = "Buy", Quantity = 100 });
            //Orders.Add(new Order { Symbol = "GOOGL", OrderType = "Sell", Quantity = 200 });
            //Orders.Add(new Order { Symbol = "MSFT", OrderType = "Buy", Quantity = 300 });
            //Orders.Add(new Order { Symbol = "AMZN", OrderType = "Sell", Quantity = 400 });
            //Orders.Add(new Order { Symbol = "FB", OrderType = "Buy", Quantity = 500 });
            //Orders.Add(new Order { Symbol = "TSLA", OrderType = "Sell", Quantity = 600 });

            //Stocks.Add(new Stock { Symbol = "AAPL", Price = 100.0, Timestamp = DateTime.Now });
            //Stocks.Add(new Stock { Symbol = "GOOGL", Price = 200.0, Timestamp = DateTime.Now });
            //Stocks.Add(new Stock { Symbol = "MSFT", Price = 300.0, Timestamp = DateTime.Now });
            //Stocks.Add(new Stock { Symbol = "AMZN", Price = 400.0, Timestamp = DateTime.Now });
            //Stocks.Add(new Stock { Symbol = "FB", Price = 500.0, Timestamp = DateTime.Now });
            //Stocks.Add(new Stock { Symbol = "TSLA", Price = 600.0, Timestamp = DateTime.Now });
            //Stocks.Add(new Stock { Symbol = "NVDA", Price = 700.0, Timestamp = DateTime.Now });
        }
    }
}
