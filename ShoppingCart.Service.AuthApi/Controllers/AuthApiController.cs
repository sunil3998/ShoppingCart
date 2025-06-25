using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Service.AuthApi.Models.Dto;
using ShoppingCart.Service.AuthApi.Services.IServices;

namespace ShoppingCart.Service.AuthApi.Controllers
{
    [ApiController]
    [Route("api/AuthUser")]
    public class AuthApiController : ControllerBase
    {
        private readonly ILogger<AuthApiController> _logger;
        private readonly IAuthService _authService;
        protected ResponseDto _responseDto;
        public AuthApiController(ILogger<AuthApiController> logger, IAuthService authService)
        {
            _logger = logger;
            _authService = authService;
            _responseDto = new();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto registrationRequestDto)
        {
            var errorMessage=await _authService.Register(registrationRequestDto);
            if (!string.IsNullOrEmpty(errorMessage.EmailId))
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "";
               return BadRequest(errorMessage);
            }
            return Ok(_responseDto);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            return Ok(_responseDto);
        }


    }
}
