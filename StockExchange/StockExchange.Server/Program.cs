
using StockExchange.Server.Context;
using StockExchange.Server.Hubs;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using StockExchange.Server.Authentication;
using StockExchange.Server.Services;
using StockExchange.Server.Repository;
using StockExchange.Server.DAL;

namespace StockExchange.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

            builder.Services.AddTransient<IOrderRepository, OrderRepository>();
            builder.Services.AddTransient<IStockRepository, StockRepository>();
            builder.Services.AddTransient<IUserRepository, UserRepository>();

            builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

            builder.Services.AddDbContext<StockExchangeApiContext>(opt => {
                opt.UseInMemoryDatabase("StockExchangeDb");
            });

            builder.Services.AddTransient<IOrderService, OrderService>();
            builder.Services.AddTransient<IStockService, StockService>();
            builder.Services.AddTransient<IUserService, UserService>();

            builder.Services.AddControllers();

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

            var key = "this is my custom Secret key for authentication";
            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                };
            });

            var auth = new Auth(key);
            builder.Services.AddScoped<IJwtAuth, Auth>(o => auth);


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
            app.UseAuthentication();

            StockPriceGenerator stockPriceGenerator = app.Services.GetRequiredService<StockPriceGenerator>();
            stockPriceGenerator.Start();

            app.Run();

        }
    }
}
