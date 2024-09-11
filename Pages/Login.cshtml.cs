using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApi.Dtos.Account;
using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.Pages
{
    public class LoginModel : PageModel
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public LoginModel(ITokenService tokenService, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _tokenService = tokenService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [BindProperty]
        public LoginDto LoginDto { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.FindByNameAsync(LoginDto.UserName);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password.");
                return Page();
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, LoginDto.Password, false);
            if (result.Succeeded)
            {
                var token = _tokenService.CreateToken(user);
                // You can store the token in TempData or in a hidden field, or use it directly in the response
                TempData["Token"] = token;
                return RedirectToPage("/Index"); // Redirect to a different page or handle the token as needed
            }

            ModelState.AddModelError(string.Empty, "Invalid username or password.");
            return Page();
        }
    }
}