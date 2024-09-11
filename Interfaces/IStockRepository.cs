using WebApi.Dtos.Stock;
using WebApi.Helpers;
using WebApi.Models;

namespace WebApi.Interfaces;

public interface IStockRepository
{
    Task<List<Stock>> GetAllAsync(QueryObject queryObject);
    ValueTask<Stock?> GetByIdAsync(int id);
    Task<Stock> CreateAsync(Stock stockModel);
    Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto);
    Task<Stock?> DeleteAsync(int id);
    Task<bool> StockExist(int id);
}