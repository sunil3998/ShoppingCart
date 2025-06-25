using ShoppingCart.Service.AuthApi.Models;

namespace ShoppingCart.Service.AuthApi.Services.IServices
{
    public interface IJwtTokenGenerate
    {
        string GenerateToken(ApplicationUser applicationUser, IEnumerable<string> roles);
    }
}
