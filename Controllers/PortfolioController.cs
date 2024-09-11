using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApi.Extensions;
using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class PortfolioController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IStockRepository _stockRepository;
        private readonly IPortfolioRepository _portfolioRepository;

        public PortfolioController(UserManager<AppUser> userManager, IStockRepository stockRep, IPortfolioRepository portfolioRepository)
        {
            _userManager = userManager;
            _stockRepository = stockRep;
            _portfolioRepository = portfolioRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserPortfolio()
        {
            // Get username from claims
            var username = User.GetUsername();
            if (string.IsNullOrEmpty(username))
            {
                return BadRequest("Username claim not found.");
            }

            // Find the user by username
            var appUser = await _userManager.FindByNameAsync(username);
            if (appUser == null)
            {
                return NotFound("User not found.");
            }

            // Retrieve the user's portfolio
            var userPortfolio = await _portfolioRepository.GetUserPortfolio(appUser);
            if (userPortfolio == null)
            {
                return NotFound("Portfolio not found.");
            }

            return Ok(userPortfolio);
        }
    }
}