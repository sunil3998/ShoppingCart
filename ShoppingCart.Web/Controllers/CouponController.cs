using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShoppingCart.Web.Models;
using ShoppingCart.Web.Service.IService;

namespace ShoppingCart.Web.Controllers
{
    public class CouponController : Controller
    {
        private readonly ICouponService _couponService;
        public CouponController(ICouponService couponService)
        {
            _couponService=couponService;
        }
        public async Task<IActionResult> Index()
        {
            List<CouponsDto>? list = new();
            ResponseDto? responseDto = await _couponService.GetAllCouponAsync();
            if (responseDto!=null && responseDto.IsSuccess)
            {
                list=JsonConvert.DeserializeObject<List<CouponsDto>>(Convert.ToString(responseDto.Result));
            }
            else
            {
                TempData["error"] = responseDto.Message;
            }
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            CouponsDto couponsDto = new CouponsDto();
            return View(couponsDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CouponsDto couponsDto)
        {
            couponsDto.CouponPicture = new byte[0];
            ResponseDto? responseDto = await _couponService.CreateCouponAsync(couponsDto);
            if(responseDto !=null && responseDto.IsSuccess)
            {
                TempData["success"] = responseDto?.Message;
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = responseDto.Message;
            }
            return View(couponsDto);
        }

        public async Task<IActionResult> Details(int id)
        {
            CouponsDto? objCouponsDto = new();
            ResponseDto? responseDto = await _couponService.GetCouponByIdAsync(id);
            if (responseDto != null && responseDto.IsSuccess)
            {
                objCouponsDto = JsonConvert.DeserializeObject<CouponsDto>(Convert.ToString(responseDto.Result));
            }
            else
            {
                TempData["error"] = responseDto.Message;
            }
            return View(objCouponsDto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            CouponsDto? objCouponsDto = new();
            ResponseDto? responseDto = await _couponService.GetCouponByIdAsync(id);
            if (responseDto != null && responseDto.IsSuccess)
            {
                objCouponsDto = JsonConvert.DeserializeObject<CouponsDto>(Convert.ToString(responseDto.Result));
            }
            else
            {
                TempData["error"] = responseDto.Message;
            }
            return View(objCouponsDto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CouponsDto couponsDto)
        {
            couponsDto.CouponPicture = new byte[0];
            ResponseDto? responseDto = await _couponService.UpdatedCouponAsync(couponsDto);
            if(responseDto!=null && responseDto.IsSuccess)
            {
                TempData["success"] = responseDto.Message;
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = responseDto.Message;
            }
            return View(couponsDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            CouponsDto objCouponsDto = new CouponsDto();
            ResponseDto? responseDto = await _couponService.GetCouponByIdAsync(id);
            if (responseDto != null && responseDto.IsSuccess)
            {
                objCouponsDto = JsonConvert.DeserializeObject<CouponsDto>(Convert.ToString(responseDto.Result));
            }
            else
            {
                TempData["error"] = responseDto.Message;
            }
            return View(objCouponsDto);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(CouponsDto couponsDto)
        {
            ResponseDto? responseDto = await _couponService.DeleteCouponAsync(couponsDto.Id);
            if (responseDto != null && responseDto.IsSuccess)
            {
                TempData["success"] = responseDto.Message;
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = responseDto.Message;
            }
            return View(couponsDto);
        }
    }
}
