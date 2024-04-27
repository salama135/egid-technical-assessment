
using StockExchange.Server.Context;
using StockExchange.Server.Hubs;
using Microsoft.EntityFrameworkCore;

namespace StockExchange.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

            builder.Services.AddControllers();
            builder.Services.AddDbContext<StockExchangeApiContext>(opt => {
                opt.UseInMemoryDatabase("StockExchangeDb");
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSignalR();
            builder.Services.AddSingleton<StockPriceGenerator>();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                    policy => {
                        policy.AllowAnyHeader();
                        policy.AllowAnyMethod();
                        policy.SetIsOriginAllowed((host) => true);
                        policy.AllowCredentials();
                    });
            });

            var app = builder.Build();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.UseCors(MyAllowSpecificOrigins);
            app.MapHub<StockHub>("/stockHub");

            app.MapFallbackToFile("/index.html");
            
            StockPriceGenerator stockPriceGenerator = app.Services.GetRequiredService<StockPriceGenerator>();
            stockPriceGenerator.Start();

            app.Run();

        }
    }
}
