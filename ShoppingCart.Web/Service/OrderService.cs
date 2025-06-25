using Microsoft.AspNetCore.Mvc.Rendering;
using ShoppingCart.Web.Models;
using ShoppingCart.Web.Service.IService;
using ShoppingCart.Web.Utility;
using System.Drawing.Imaging;
using System.Text;
using System.Web;

namespace ShoppingCart.Web.Service
{
    public class OrderService : IOrderService
    {
        private readonly IBaseService _baseService;
        public OrderService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> CreateOrder(CartDto cartDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticData.ApiType.POST,
                Data = cartDto,
                Url = StaticData.OrdersApiBase + "/api/order/CreateOrder"
            }, withBearer: true);
        }

        public async Task<ResponseDto?> GetAllOrder(string? userId = null)
        {
            if (string.IsNullOrEmpty(userId))
                userId = HttpUtility.HtmlDecode(userId);

            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticData.ApiType.GET,
                Url = StaticData.OrdersApiBase + $"/api/OrderApi/GetAllOrder"
            }, withBearer: true);
        }

        public async Task<ResponseDto?> GetOrder(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticData.ApiType.GET,
                Url = StaticData.OrdersApiBase + $"/api/OrderApi/GetOrder/{id}"
            }, withBearer: true);
        }

        public async Task<ResponseDto?> UpdateOrderStatus(int orderId, string newStatus)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticData.ApiType.PUT,
                Data = newStatus,
                Url = StaticData.OrdersApiBase + $"/api/OrderApi/UpdateOrderStatus/{orderId}"
            }, withBearer: true);
        }
    }
}
