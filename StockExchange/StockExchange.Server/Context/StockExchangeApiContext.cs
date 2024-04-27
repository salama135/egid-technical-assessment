using Microsoft.EntityFrameworkCore;
using StockExchange.Server.Model;
using System;

namespace StockExchange.Server.Context
{
    public class StockExchangeApiContext : DbContext
    {
        public DbSet<Stock> Stocks { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Stock>()
                .HasIndex(s => new { s.Symbol })
                .IsUnique(true);
        }

        public StockExchangeApiContext(DbContextOptions<StockExchangeApiContext> options) : base(options)
        {

            //Orders.Add(new Order { Id = new Guid(), Symbol = "AAPL",  OrderType = "Buy",  Quantity = 100 });
            //Orders.Add(new Order { Id = new Guid(), Symbol = "GOOGL", OrderType = "Sell", Quantity = 200 });
            //Orders.Add(new Order { Id = new Guid(), Symbol = "MSFT",  OrderType = "Buy",  Quantity = 300 });
            //Orders.Add(new Order { Id = new Guid(), Symbol = "AMZN",  OrderType = "Sell", Quantity = 400 });
            //Orders.Add(new Order { Id = new Guid(), Symbol = "FB",    OrderType = "Buy",  Quantity = 500 });
            //Orders.Add(new Order { Id = new Guid(), Symbol = "TSLA",  OrderType = "Sell", Quantity = 600 });
            Random r = new Random();

            Stocks.Add(new Stock { Id = new Guid(), Symbol = "AAPL",  Price = 100.0, Timestamp = DateTime.Now, Quantity = r.Next(1, 1000) });
            Stocks.Add(new Stock { Id = new Guid(), Symbol = "GOOGL", Price = 200.0, Timestamp = DateTime.Now, Quantity = r.Next(1, 1000) });
            Stocks.Add(new Stock { Id = new Guid(), Symbol = "MSFT",  Price = 300.0, Timestamp = DateTime.Now, Quantity = r.Next(1, 1000) });
            Stocks.Add(new Stock { Id = new Guid(), Symbol = "AMZN",  Price = 400.0, Timestamp = DateTime.Now, Quantity = r.Next(1, 1000) });
            Stocks.Add(new Stock { Id = new Guid(), Symbol = "FB",    Price = 500.0, Timestamp = DateTime.Now, Quantity = r.Next(1, 1000) });
            Stocks.Add(new Stock { Id = new Guid(), Symbol = "TSLA",  Price = 600.0, Timestamp = DateTime.Now, Quantity = r.Next(1, 1000) });
            Stocks.Add(new Stock { Id = new Guid(), Symbol = "NVDA",  Price = 700.0, Timestamp = DateTime.Now, Quantity = r.Next(1, 1000)});

            Users.Add(new User { Id = new Guid(), Name = "Name1" });
            Users.Add(new User { Id = new Guid(), Name = "Name2" });
            Users.Add(new User { Id = new Guid(), Name = "Name3" });
            Users.Add(new User { Id = new Guid(), Name = "Name4" });
            Users.Add(new User { Id = new Guid(), Name = "Name5" });
            Users.Add(new User { Id = new Guid(), Name = "Name6" });



        }
    }
}
