using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Service.AuthApi.Data;
using ShoppingCart.Service.AuthApi.Models;
using ShoppingCart.Service.AuthApi.Models.Dto;
using ShoppingCart.Service.AuthApi.Services.IServices;

namespace ShoppingCart.Service.AuthApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _appDb;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerate _jwtTokenGenerate;
        public AuthService(AppDbContext appDb, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IJwtTokenGenerate jwtTokenGenerate)
        {
            _appDb = appDb;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtTokenGenerate = jwtTokenGenerate;
        }

        public async Task<bool> AssignRole(string email, string roleName)
        {
            var user = await _appDb.ApplicationUsers.Where(x => x.Email == email).FirstOrDefaultAsync();
            if (user != null)
            {
                if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult()) ;
                {
                    //create role if not exist
                    _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();  
                }
                await _userManager.AddToRoleAsync(user, roleName);
                return true;
            }
            return false;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user = await _appDb.ApplicationUsers.FirstOrDefaultAsync(x => x.UserName.ToLower() == loginRequestDto.UserName.ToLower());
            bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);
            if (user != null && isValid == false)
            {
                return new LoginResponseDto() { User = "", Token = "" };
            }

            //if user was found, generate JWT Token
            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtTokenGenerate.GenerateToken(user, roles);
            UserDto userDto = new UserDto()
            {
                EmailId = user.Email,
                Id = user.Id,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber
            };
            LoginResponseDto loginResponseDto = new LoginResponseDto()
            {
                User = user.Email,
                Token = token
            };
            return loginResponseDto;
        }

        public async Task<UserDto> Register(RegistrationRequestDto registrationRequestDto)
        {
            ApplicationUser applicationUser = new ApplicationUser()
            {
                UserName = registrationRequestDto.EmailId,
                Email = registrationRequestDto.EmailId,
                NormalizedEmail = registrationRequestDto.EmailId.ToLower(),
                Name = registrationRequestDto.Name,
                PhoneNumber = registrationRequestDto.PhoneNumber
            };
            try
            {
                var result = await _userManager.CreateAsync(applicationUser, registrationRequestDto.Password);
                if (result.Succeeded)
                {
                    var userToReturn = _appDb.ApplicationUsers.First(u => u.UserName == registrationRequestDto.EmailId);
                    UserDto userDto = new()
                    {
                        EmailId = userToReturn.Email,
                        Id = userToReturn.Id,
                        Name = userToReturn.Name,
                        PhoneNumber = userToReturn.PhoneNumber,
                    };
                    return userDto;
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return new UserDto();
        }
    }
}
