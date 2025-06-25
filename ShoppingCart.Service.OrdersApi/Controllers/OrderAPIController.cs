using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Service.OrdersApi.Service.IService;
using ShoppingCart.Service.OrdersApi.Utility;
using ShoppingCart.Service.OrdersApi.Data;
using ShoppingCart.Service.OrdersApi.Models.Dto;
using ShoppingCart.Service.OrdersApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ShoppingCart.Service.OrdersApi.Controllers
{
    [Route("api/OrderApi")]
    [ApiController]
    public class OrderAPIController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ResponseDto _response;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        private readonly ICartService _cartService;

        public OrderAPIController(AppDbContext context, IMapper mapper,
            IProductService productService, ICartService cartService)
        {
            this._context = context;
            this._mapper = mapper;
            this._response = new ResponseDto();
            this._productService = productService;
            this._cartService = cartService;
        }


        [Authorize]
        [HttpGet("GetAllOrder")]
        public ResponseDto? GetAll(string? userId="")
        {
            try
            {
                IEnumerable<OrderHeaderModel> objList;
                if(User.IsInRole(StaticData.RoleAdmin))
                {
                    objList=_context.OrderHeaders.Include(i=>i.OrderDetails).OrderByDescending(o=>o.OrderHeaderId).ToList();
                }
                else
                {
                    objList = _context.OrderHeaders.Include(i => i.OrderHeaderId).OrderByDescending(o => o.OrderHeaderId).ToList();
                }
                _response.Result = _mapper.Map<IEnumerable<OrderHeaderDto>>(objList);
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }
            return this._response;
        }

        //[Authorize]
        [HttpGet("GetOrder/{id:int}")]
        public ResponseDto? GetOrderById(int id)
        {
            try
            {
                OrderHeaderModel orderHeaderModel = _context.OrderHeaders.Include(u => u.OrderDetails).First(x => x.OrderHeaderId == id); 
                _response.Result=_mapper.Map<OrderHeaderDto>(orderHeaderModel);
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }
            return this._response;
        }


        [Authorize]
        [HttpPost("CreateOrders")]
        public async Task<ResponseDto> CreateOrder([FromBody] CartDto cartDto)
            {
            try
            {
                OrderHeaderDto orderHeaderDto = _mapper.Map<OrderHeaderDto>(cartDto.CartHeader);
                orderHeaderDto.OrderTime = DateTime.Now;
                orderHeaderDto.OrderStatus = StaticData.Status_Pending;
                orderHeaderDto.OrderDetails = _mapper.Map<IEnumerable<OrderDetailsDto>>(cartDto.CartDetails);
                
                OrderHeaderModel orderHeaderModel = _mapper.Map<OrderHeaderModel>(orderHeaderDto);
                await _context.OrderHeaders.AddAsync(orderHeaderModel);
                await _context.SaveChangesAsync();

                orderHeaderDto.OrderHeaderId = orderHeaderModel.OrderHeaderId;
                _response.Result = orderHeaderDto;

                // Remove cart when order is created
                var cartResponse = await _cartService.RemoveCartWhenCheckout(cartDto.CartHeader.Id);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.ToString();
            }
            return _response;
        }

        [Authorize]
        [HttpPut("UpdateOrderStatus/{orderId:int}")]
        public async Task<ResponseDto> UpdateOrderStatus(int orderId, [FromBody] string newStatus)
        {
            try
            {
                OrderHeaderModel orderHeader = await _context.OrderHeaders.Where(x => x.OrderHeaderId == orderId).FirstOrDefaultAsync();
                if(orderHeader!=null)
                {
                    if(newStatus==StaticData.Status_Cancelled)
                    {
                        //we will give refund
                        
                    }
                    orderHeader.OrderStatus = newStatus;
                    _context.SaveChanges();
                }
            }
            catch (Exception  ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.ToString();
            }
            return _response;
        }


    }
}
