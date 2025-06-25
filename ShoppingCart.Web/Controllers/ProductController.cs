using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShoppingCart.Web.Models;
using ShoppingCart.Web.Service.IService;

namespace ShoppingCart.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            this._productService = productService;
        }
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

        public async Task<IActionResult> Create()
        {
            ProductsDto productsDto = new ProductsDto();

            return View(productsDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductsDto productsDto)
        {
           // productsDto.CouponPicture = new byte[0];
            ResponseDto? responseDto = await _productService.CreateProductAsync(productsDto);
            if (responseDto != null && responseDto.IsSuccess)
            {
                TempData["success"] = responseDto?.Message;
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = responseDto.Message;
            }
            return View(productsDto);
        }

        public async Task<IActionResult> Details(int id)
        {
            ProductsDto? objProductsDto = new();
            ResponseDto? responseDto = await _productService.GetProductByIdAsync(id);
            if (responseDto != null && responseDto.IsSuccess)
            {
                objProductsDto = JsonConvert.DeserializeObject<ProductsDto>(Convert.ToString(responseDto.Result));
            }
            else
            {
                TempData["error"] = responseDto.Message;
            }
            return View(objProductsDto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            ProductsDto? objProductsDto = new();
            ResponseDto? responseDto = await _productService.GetProductByIdAsync(id);
            if (responseDto != null && responseDto.IsSuccess)
            {
                objProductsDto = JsonConvert.DeserializeObject<ProductsDto>(Convert.ToString(responseDto.Result));
            }
            else
            {
                TempData["error"] = responseDto.Message;
            }
            objProductsDto.ImageUrl = "";
            return View(objProductsDto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductsDto productsDto)
        {
           // productsDto.CouponPicture = new byte[0];
            ResponseDto? responseDto = await _productService.UpdatedProductAsync(productsDto);
            if (responseDto != null && responseDto.IsSuccess)
            {
                TempData["success"] = responseDto.Message;
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = responseDto.Message;
            }
            return View(productsDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            ProductsDto objProductsDto = new ProductsDto();
            ResponseDto? responseDto = await _productService.GetProductByIdAsync(id);
            if (responseDto != null && responseDto.IsSuccess)
            {
                objProductsDto = JsonConvert.DeserializeObject<ProductsDto>(Convert.ToString(responseDto.Result));
            }
            else
            {
                TempData["error"] = responseDto.Message;
            }
            return View(objProductsDto);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(ProductsDto productsDto)
        {
            ResponseDto? responseDto = await _productService.DeleteProductAsync(productsDto.Id);
            if (responseDto != null && responseDto.IsSuccess)
            {
                TempData["success"] = responseDto.Message;
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = responseDto.Message;
            }
            return View(productsDto);
        }
    }
}
