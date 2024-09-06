using WebApi.Dtos.Stock;
using WebApi.Models;

namespace WebApi.Mappers;

public static class StockMapper
{
    public static StockDto ToStockDto(this Stock stockModel)
    {
        return new StockDto
        {
            Id = stockModel.Id,
            Symbol = stockModel.Symbol,
            CompanyName = stockModel.CompanyName,
            Purchase = stockModel.Purchase,
            LastDiv = stockModel.LastDiv,
            Industry = stockModel.Industry,
            MarketCap = stockModel.MarketCap,
            Comments = stockModel.Comments.Select(s => s.ToCommentDto()).ToList()
                
        };
    }

    public static Stock ToStockFromCreateDto(this CreateStockRequestDto createStockRequest)
    {
        return new Stock()
        {
            Symbol = createStockRequest.Symbol,
            CompanyName = createStockRequest.CompanyName,
            Purchase = createStockRequest.Purchase,
            LastDiv = createStockRequest.LastDiv,
            Industry = createStockRequest.Industry,
            MarketCap = createStockRequest.MarketCap
        };
    }

    public static Stock updateDto(this UpdateStockRequestDto updateStockRequest)
    {
        return new Stock()
        {
            Symbol = updateStockRequest.Symbol,
            CompanyName = updateStockRequest.CompanyName,
            Purchase = updateStockRequest.Purchase,
            LastDiv = updateStockRequest.LastDiv,
            Industry = updateStockRequest.Industry,
            MarketCap = updateStockRequest.MarketCap
        };
    }
}