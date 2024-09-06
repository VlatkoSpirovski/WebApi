using WebApi.Dtos.Stock;
using WebApi.Models;

namespace WebApi.Interfaces;

public interface IStockRepository
{
    Task<List<Stock>> GetAllAsync();
    ValueTask<Stock?> GetByIdAsync(int id);
    Task<Stock> CreateAsync(Stock stockModel);
    Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto);
    Task<Stock?> DeleteAsync(int id);
}