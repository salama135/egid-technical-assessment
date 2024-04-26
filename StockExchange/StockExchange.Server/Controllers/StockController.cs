using Microsoft.AspNetCore.Mvc;
using StockExchange.Server.Context;
using StockExchange.Server.DTOs;
using StockExchange.Server.Model;

namespace StockExchange.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockController : ControllerBase
    {
        private readonly StockExchangeApiContext _context;

        public StockController(StockExchangeApiContext context)
        {
            _context = context;
        }

        // GET: api/Stock
        [HttpGet]
        public ActionResult<IEnumerable<StockDto>> GetStocks()
        {
            var stocks = _context.Stocks.Select(s => new StockDto
            {
                Symbol = s.Symbol,
                Price = s.Price,
                Timestamp = s.Timestamp
            }).ToList();

            return Ok(stocks);
        }

        // GET: api/Stock/5
        [HttpGet("{symbol}/history")]
        public ActionResult<StockDto> GetStock(string symbol)
        {
            var stock = _context.Stocks.FirstOrDefault(s => s.Symbol == symbol);

            if (stock == null)
            {
                return NotFound();
            }

            var stockDto = new StockDto
            {
                Symbol = stock.Symbol,
                Price = stock.Price,
                Timestamp = stock.Timestamp
            };

            return Ok(stockDto);
        }

        // POST: api/Stock
        [HttpPost]
        public ActionResult<StockDto> PostStock(StockDto stockDto)
        {
            if(_context.Stocks.Any(s => s.Symbol.Equals(stockDto.Symbol)))
            {
                return Conflict();
            }

            var stock = new Stock
            {
                Id = Guid.NewGuid(),
                Quantity = stockDto.Quantity,
                Symbol = stockDto.Symbol,
                Price = stockDto.Price,
                Timestamp = DateTime.Now
            };

            _context.Stocks.Add(stock);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetStock), new { symbol = stock.Symbol }, stockDto);
        }

        // PUT: api/Stock/5
        [HttpPut("{symbol}")]
        public IActionResult PutStock(string symbol, StockDto stockDto)
        {
            var stock = _context.Stocks.FirstOrDefault(s => s.Symbol == symbol);

            if (stock == null)
            {
                return NotFound();
            }

            stock.Price = stockDto.Price;
            stock.Timestamp = DateTime.Now;

            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/Stock/5
        [HttpDelete("{symbol}")]
        public IActionResult DeleteStock(string symbol)
        {
            var stock = _context.Stocks.FirstOrDefault(s => s.Symbol == symbol);

            if (stock == null)
            {
                return NotFound();
            }

            _context.Stocks.Remove(stock);
            _context.SaveChanges();

            return NoContent();
        }
    }
}

