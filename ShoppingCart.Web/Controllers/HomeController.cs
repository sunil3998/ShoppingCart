using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShoppingCart.Web.Models;
using ShoppingCart.Web.Service;
using ShoppingCart.Web.Service.IService;
using ShoppingCart.Web.Utility;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;

namespace ShoppingCart.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        private readonly ICartService _cartService;
        public HomeController(ILogger<HomeController> logger, IProductService productService, ICartService cartService)
        {
            _logger = logger;
            _productService = productService;
            _cartService = cartService; 
        }

        [Authorize(Roles = StaticData.RoleAdmin)]
        public async Task<IActionResult> Index()
        {
            List<ProductsDto>? list = new();
            ResponseDto? responseDto = await _productService.GetAllProductAsync();
             if (responseDto != null && responseDto.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ProductsDto>>(Convert.ToString(responseDto.Result));
            }
            else
            {
                TempData["error"] = responseDto.Message;
            }
            return View(list);
        }

        [Authorize(Roles = StaticData.RoleAdmin)]
        public async Task<IActionResult> Details(int id)
        {
            ProductsDto? productsDto = new();
            ResponseDto? responseDto = await _productService.GetProductByIdAsync(id);
            if (responseDto!=null && responseDto.IsSuccess)
            {
                productsDto = JsonConvert.DeserializeObject<ProductsDto>(Convert.ToString(responseDto.Result));
            }
            else
            {
                TempData["error"] = responseDto.Message;
            }
            return View(productsDto);
        }

        [HttpPost]
        [ActionName("ProductDetails")]
    /////    [Authorize(Roles = StaticData.RoleAdmin)]
        public async Task<IActionResult> Details(ProductsDto productsDto)
        {
            CartDto cartDto = new CartDto()
            {
                CartHeader= new CartHeaderDto()
                {
                    UserId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value
                },
            };


            CartDetailsDto cartDetailsDto = new CartDetailsDto()
            {
                Count = productsDto.Count,
                ProductId = productsDto.Id,
            };

            List<CartDetailsDto> cartDetailsDtos = new() { cartDetailsDto };
            cartDto.CartDetails = cartDetailsDtos;

            ResponseDto? responseDto = await _cartService.CartUpsertAsync(cartDto);
            if (responseDto != null && responseDto.IsSuccess)
            {
                TempData["success"] = "Product added to cart successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = responseDto.Message;
            }
            return View(productsDto);
        }

        [Authorize(Roles = StaticData.RoleAdmin)]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
