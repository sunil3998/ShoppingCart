using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Coupon.Data;
using ShoppingCart.Coupon.Models;
using ShoppingCart.Coupon.Models.Dto;

namespace ShoppingCart.Coupon.Controllers
{
    [Route("api/coupon")]
    [ApiController]
    [Authorize]
    public class CouponAPIController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ResponseDto _response;
        private readonly IMapper _mapper;
        public CouponAPIController(AppDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
            this._response = new ResponseDto();
        }

        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                IEnumerable<CouponModel> objList = _context.Coupons.ToList();
                _response.Result = _mapper.Map<IEnumerable<CouponsDto>>(objList);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet]
        [Route("{id:int}")]
        public object Get(int id)
        {
            try
            {
                CouponModel obj = _context.Coupons.First(x => x.Id == id);
                _response.Result = _mapper.Map<CouponsDto>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet]
        [Route("GetByCouponCode")]
        public object GetByCouponCode(string couponCode)
        {
            try
            {
                CouponModel obj = _context.Coupons.First(x => x.Title == couponCode);
                _response.Result = _mapper.Map<CouponsDto>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost]
        [Route("Create")]
        [Authorize(Roles ="ADMIN")]
        public ResponseDto Post([FromBody] CouponsDto couponsDto)
        {
            try
            {
                CouponModel coupon = _mapper.Map<CouponModel>(couponsDto);
                _context.Coupons.Add(coupon);
                _context.SaveChanges();
                _response.Result = _mapper.Map<CouponsDto>(coupon);
                _response.Message = "Coupon is added successfully.";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.ToString();
            }
            return _response;
        }

        [HttpPut]
        [Route("Update")]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Put([FromBody] CouponsDto couponsDto)
        {
            try
            {
                CouponModel coupon = _mapper.Map<CouponModel>(couponsDto);
                _context.Coupons.Update(coupon);
                _context.SaveChanges();
                _response.Result = _mapper.Map<CouponsDto>(coupon);
                _response.Message = "Coupon is updated successfully.";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.ToString();
            }
            return _response;
        }

        [HttpDelete]
        [Route("Delete")]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Delete(int id)
        {
            try
            {
                var coupon = _context.Coupons.First(x => x.Id == id);
                _context.Coupons.Remove(coupon);
                _context.SaveChanges();
                _response.Result = _mapper.Map<CouponsDto>(coupon);
                _response.Message = "Coupon is deleted successfully.";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.ToString();
            }
            return _response;
        }
    }
}
