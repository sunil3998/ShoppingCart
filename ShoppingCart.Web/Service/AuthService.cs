using ShoppingCart.Web.Models;
using ShoppingCart.Web.Service.IService;
using ShoppingCart.Web.Utility;

namespace ShoppingCart.Web.Service
{
    public class AuthService : IAuthService
    {
        private readonly IBaseService _baseService;
        public AuthService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public async Task<ResponseDto?> AssignRoleAsync(RegistrationRequestDto registrationRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticData.ApiType.POST,
                Data = registrationRequestDto,
                Url = StaticData.AuthApiBase + "/api/AuthApi/AssignRole"
            }, withBearer: false);
        }

        public async Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticData.ApiType.POST,
                Data = loginRequestDto,
                Url = StaticData.AuthApiBase + "/api/AuthApi/login"
            }, withBearer: false);
        }

        public async Task<ResponseDto?> RegisterAsync(RegistrationRequestDto registrationRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticData.ApiType.POST,
                Data = registrationRequestDto,
                Url = StaticData.AuthApiBase + "/api/AuthApi/register"
            }, withBearer: false);
        }
    }
}
