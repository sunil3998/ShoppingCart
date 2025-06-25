using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Service.AuthApi.Data;
using ShoppingCart.Service.AuthApi.Models;
using ShoppingCart.Service.AuthApi.Models.Dto;
using ShoppingCart.Service.AuthApi.Services.IServices;

namespace ShoppingCart.Service.AuthApi.Controllers
{
    [Route("api/AuthApi")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _appDb;
        private readonly ILogger<AuthController> _logger;
        private readonly IAuthService _authService;
        protected ResponseDto _responseDto;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AuthController(ILogger<AuthController> logger, IAuthService authService,
            AppDbContext appDb, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _authService = authService;
            _responseDto = new();
            _appDb = appDb;
            _userManager = userManager; _roleManager = roleManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto registrationRequestDto)
        {
            var errorMessage = await _authService.Register(registrationRequestDto);
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
           var loginResponse=await _authService.Login(loginRequestDto);
            if(loginResponse.User==null)
            {
                _responseDto.IsSuccess=false;
                _responseDto.Message = "username or password is incorrect";
                return BadRequest(_responseDto);
            }
            _responseDto.Result=loginResponse;
            return Ok(_responseDto);
        }

        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole([FromBody] RegistrationRequestDto registrationRequestDto)
        {
            var assignRole = await _authService.AssignRole(registrationRequestDto.EmailId, registrationRequestDto.Role);
            if (!assignRole)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "username is incorrect"; 
                return BadRequest(_responseDto);
            }
            return Ok(_responseDto);
        }
    }
}
