using Microsoft.AspNetCore.Mvc;
using StockExchange.Server.DAL;
using StockExchange.Server.DTOs;
using StockExchange.Server.Model;

namespace StockExchange.Server.Services
{
    public interface IStockService
    {
        public double GetStockPrice(string symbol);
        public IEnumerable<StockDto> GetStocks();
        public StockDto GetStock(string symbol);
        public StockDto PostStock(StockDto stockDto);
        public bool PutStock(string symbol, StockDto stockDto);
        public bool DeleteStock(string symbol);
    }

    public class StockService : IStockService
    {
        IUnitOfWork _unitOfWork;

        public StockService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool DeleteStock(string symbol)
        {
            throw new NotImplementedException();
        }

        public StockDto GetStock(string symbol)
        {
            throw new NotImplementedException();
        }

        public double GetStockPrice(string symbol)
        {
            var stockPrice = _unitOfWork.StockRepository.GetStockBySymbol(symbol);

            if (stockPrice == null) throw new Exception($"no stock with symbol {symbol}");

            return stockPrice.Price;
        }

        public IEnumerable<StockDto> GetStocks()
        {
            var stocks = _unitOfWork.StockRepository.GetAllStocks().Select(s => new StockDto
            {
                Symbol = s.Symbol,
                Price = s.Price,
                Timestamp = s.Timestamp
            }).ToList();

            return stocks;
        }

        public StockDto PostStock(StockDto stockDto)
        {
            throw new NotImplementedException();
        }

        public bool PutStock(string symbol, StockDto stockDto)
        {
            throw new NotImplementedException();
        }
    }
}
