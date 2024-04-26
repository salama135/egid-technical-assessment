using Microsoft.AspNetCore.Mvc;
using StockExchange.Server.DTOs;
using StockExchange.Server.Model;
using StockExchange.Server.Services;

namespace StockExchange.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public IActionResult PlaceOrder(OrderDto orderDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var order = new Order
            {
                Symbol = orderDto.Symbol,
                OrderType = orderDto.OrderType,
                Quantity = orderDto.Quantity
            };

            _orderService.PlaceOrder(order);

            return Ok();
        }
    }
}
