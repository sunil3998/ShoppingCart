using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Service.OrderApi.Data;
using ShoppingCart.Service.OrderApi.Models;
using ShoppingCart.Service.OrderApi.Models.Dto;
using ShoppingCart.Service.OrderApi.Service.IService;
using ShoppingCart.Service.OrderApi.Utility;


namespace ShoppingCart.Service.OrderApi.Controllers
{
    [Route("api/order")]
    [ApiController]
    [Authorize]
    public class OrderAPIController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ResponseDto _response;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;

        public OrderAPIController(AppDbContext context, IMapper mapper, 
            IProductService productService)
        {
            this._context = context;
            this._mapper = mapper;
            this._response = new ResponseDto();
            this._productService = productService;
        }

        [Authorize]
        [HttpGet("CreateOrder")]
        public async Task<ResponseDto> GetCartByUserId([FromBody] CartDto cartDto)
        {
            try
            {
                OrderHeaderDto orderHeaderDto =_mapper.Map<OrderHeaderDto>(cartDto.CartHeader);
                orderHeaderDto.OrderTime = DateTime.Now;
                orderHeaderDto.OrderStatus = StaticData.Status_Pending;
                orderHeaderDto.OrderDetails=_mapper.Map<IEnumerable<OrderDetailsDto>>(cartDto.CartDetails);

                OrderHeaderModel orderHeaderModel = _mapper.Map<OrderHeaderModel>(orderHeaderDto);
                await _context.OrderHeaders.AddAsync(orderHeaderModel);
                await _context.SaveChangesAsync();

                orderHeaderDto.OrderHeaderId = orderHeaderModel.OrderHeaderId;
                _response.Result = orderHeaderDto;
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
