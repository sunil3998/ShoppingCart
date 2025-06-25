using ShoppingCart.Service.AuthApi.Models.Dto;

namespace ShoppingCart.Service.AuthApi.Services.IServices
{
    public interface IAuthService
    {
        Task<UserDto> Register(RegistrationRequestDto registrationRequestDto);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<bool> AssignRole(string email,string roleName);
    }
}
