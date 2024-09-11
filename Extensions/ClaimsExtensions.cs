using System.Security.Claims;
using System.Linq; // Ensure this is included for LINQ methods

namespace WebApi.Extensions
{
    public static class ClaimsExtensions
    {
        public static string GetUsername(this ClaimsPrincipal user)
        {
            // Find the claim safely, and return null or empty if it's not found
            var claim = user.Claims.SingleOrDefault(x => x.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname", StringComparison.OrdinalIgnoreCase));
            return claim?.Value ?? string.Empty; // Safely handle null claim
        }
    }
}