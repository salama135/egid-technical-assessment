using Microsoft.EntityFrameworkCore;
using StockExchange.Server.Model;

namespace StockExchange.Server.Context
{
    public class StockExchangeApiContext : DbContext
    {
        public DbSet<Stock> Stocks { get; set; }
        public StockExchangeApiContext(DbContextOptions<EmployeeDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
