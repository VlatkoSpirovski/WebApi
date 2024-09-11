using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.Repository;

public class PortfolioRepository : IPortfolioRepository
{
    private readonly ApplicationDBContext _dbContext;
    public PortfolioRepository(ApplicationDBContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<List<Stock>> GetUserPortfolio(AppUser user)
    {
        return await _dbContext.Portfolios.Where(u => u.AppUserId == user.Id).Select(stock => new Stock
        {
            Id = stock.Stock.Id,
            Symbol =stock.Stock.Symbol,
            CompanyName =stock.Stock.CompanyName,
            Purchase = stock.Stock.Purchase,
            LastDiv = stock.Stock.LastDiv,
            Industry = stock.Stock.Industry,
            MarketCap = stock.Stock.MarketCap
        }).ToListAsync();
    }
}