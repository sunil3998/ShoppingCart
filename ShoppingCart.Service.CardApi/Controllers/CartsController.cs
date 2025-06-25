using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Service.CardsApi.Models.Dto;
using ShoppingCart.Service.CartApi.Data;
using ShoppingCart.Service.CartApi.Models;
using ShoppingCart.Service.CartApi.Models.Dto;
using ShoppingCart.Service.CartApi.Service.IService;

namespace ShoppingCart.Service.CartApi.Controllers
{
    [Route("api/CartApi")]
    [ApiController]
    [Authorize]
    public class CartsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ResponseDto _response;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        private readonly ICouponService _couponService;

        public CartsController(AppDbContext context, IMapper mapper,
            IProductService productService, ICouponService couponService)
        {
            this._context = context;
            this._mapper = mapper;
            this._response = new ResponseDto();
            this._productService = productService;
            this._couponService = couponService;
        }

        [HttpGet("GetCartByUserId/{UserId}")]
        public async Task<ResponseDto> GetCartByUserId(string UserId)
        {
            try
            {
                CartDto cartDto = new()
                {
                    CartHeader = _mapper.Map<CartHeaderDto>(await _context.CartHeaderModels.FirstOrDefaultAsync(x => x.UserId == UserId))
                };

                cartDto.CartDetails = _mapper.Map<List<CartDetailsDto>>(
                                       await _context.CartDetailsModels.Where(x => x.CartHeaderId == cartDto.CartHeader.Id).ToListAsync());

                IEnumerable<ProductsDto> products = await _productService.GetProducts();

                foreach (var cartDetails in cartDto.CartDetails)
                {
                    cartDetails.Product = products.FirstOrDefault(x => x.Id == cartDetails.ProductId);
                    cartDto.CartHeader.CartTotal += (cartDetails.Count * cartDetails.Product.Price);
                }

                //apply coupon
                if (!string.IsNullOrEmpty(cartDto.CartHeader.CouponCode))
                {
                    var coupon = await _couponService.GetCoupon(cartDto.CartHeader.CouponCode);
                    if (coupon != null && cartDto.CartHeader.CartTotal > coupon.MinimumAmount)
                    {
                        cartDto.CartHeader.Discount = coupon.Discount;
                        cartDto.CartHeader.CartTotal -= coupon.Discount;
                    }
                }

                _response.Result = cartDto;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.ToString();
            }
            return _response;
        }

        [HttpPost("ApplyCoupon")]
        public async Task<ResponseDto> ApplyCoupon([FromBody] CartDto cartDto)
        {

            try
            {
                var cartFromDb = await _context.CartHeaderModels.FirstOrDefaultAsync(x => x.UserId == cartDto.CartHeader.UserId);
                cartFromDb.CouponCode = cartDto.CartHeader.CouponCode;
                _context.CartHeaderModels.Update(cartFromDb);
                await _context.SaveChangesAsync();
                _response.Result = true;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.ToString();
            }
            return _response;
        }

        [HttpPost("RemoveCoupon")]
        public async Task<ResponseDto> RemoveCoupon([FromBody] CartDto cartDto)
        {
            try
            {
                var cartFromDb = await _context.CartHeaderModels.FirstOrDefaultAsync(x => x.UserId == cartDto.CartHeader.UserId);
                cartFromDb.CouponCode = "";
                _context.CartHeaderModels.Update(cartFromDb);
                await _context.SaveChangesAsync();
                _response.Result = true;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.ToString();
            }
            return _response;
        }


        [HttpPost("CartUpsert")]
        public async Task<ResponseDto> CartUpsert([FromBody] CartDto cartDto)
        {
            try
            {
                var cartHeaderFromDb = await _context.CartHeaderModels.AsNoTracking().FirstOrDefaultAsync(x => x.UserId == cartDto.CartHeader.UserId);
                if (cartHeaderFromDb == null)
                {
                    //create header and details
                    CartHeaderModel cartHeaderModel = _mapper.Map<CartHeaderModel>(cartDto.CartHeader);
                    _context.CartHeaderModels.Add(cartHeaderModel);
                    await _context.SaveChangesAsync();

                    cartDto.CartDetails.First().CartHeaderId = cartHeaderModel.Id;
                    var carDetails = _mapper.Map<CartDetailsModel>(cartDto.CartDetails.First());
                    // carDetails.CartHeader.Id = cartHeaderModel.Id;
                    _context.CartDetailsModels.Add(carDetails);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    //header is not null, check if details has same product
                    var cartDetailsFromDb = await _context.CartDetailsModels.AsNoTracking().FirstOrDefaultAsync(
                        x => x.ProductId == cartDto.CartDetails.First().ProductId && x.CartHeaderId == cartHeaderFromDb.Id);
                    if (cartDetailsFromDb == null)
                    {
                        // create cart details
                        cartDto.CartDetails.First().CartHeaderId = cartHeaderFromDb.Id;
                        _context.CartDetailsModels.Add(_mapper.Map<CartDetailsModel>(cartDto.CartDetails.First()));
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        //update count in cart details
                        cartDto.CartDetails.First().Count += cartDetailsFromDb.Count;
                        cartDto.CartDetails.First().CartHeaderId = cartDetailsFromDb.CartHeaderId;
                        cartDto.CartDetails.First().Id = cartDetailsFromDb.Id;
                        _context.CartDetailsModels.Update(_mapper.Map<CartDetailsModel>(cartDto.CartDetails.First()));
                        await _context.SaveChangesAsync();
                    }
                }
                _response.Result = cartDto;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.ToString();
            }
            return _response;
        }

        [HttpPost("RemoveCart")]
        public async Task<ResponseDto> RemoveCart([FromBody] int cartDetailsId)
        {
            try     
            {
                CartDetailsModel cartDetailsModel = await _context.CartDetailsModels.FirstOrDefaultAsync(x => x.Id == cartDetailsId);
                int totalCartOfCartItem = await _context.CartDetailsModels
                     .Where(x => x.CartHeaderId == cartDetailsModel.CartHeaderId).CountAsync();
                _context.CartDetailsModels.Remove(cartDetailsModel);
                if (totalCartOfCartItem == 1)
                {
                    //remove header and details
                    var cartHearderToRemove = await _context.CartHeaderModels.FirstOrDefaultAsync(x => x.Id == cartDetailsModel.CartHeaderId);
                    _context.CartHeaderModels.Remove(cartHearderToRemove);
                }
                await _context.SaveChangesAsync();
                _response.Result = true;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.ToString();
            }
            return _response;
        }

        [HttpGet("RemoveCartWhenCheckout/{cartHeaderId}")]
        public async Task<ResponseDto> RemoveCartWhenCheckout(int cartHeaderId)
        {
            try
            {
                var cartDetailsModel = await _context.CartDetailsModels.Where(x => x.CartHeaderId == cartHeaderId).ToListAsync();
                _context.CartDetailsModels.RemoveRange(cartDetailsModel);
                //remove header and details
                var cartHearderToRemove = await _context.CartHeaderModels.FirstOrDefaultAsync(x => x.Id == cartHeaderId);
                _context.CartHeaderModels.Remove(cartHearderToRemove);
                await _context.SaveChangesAsync();
                _response.Result = true;
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
