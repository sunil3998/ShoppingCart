using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShoppingCart.Web.Models;
using ShoppingCart.Web.Service.IService;
using ShoppingCart.Web.Utility;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace ShoppingCart.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            this._orderService = orderService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(string? status)
        {
            IEnumerable<OrderHeaderDto> list;
            string userId = null;
            if (!User.IsInRole(StaticData.RoleAdmin))
            {
                userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;
            }
            ResponseDto response = _orderService.GetAllOrder(userId).GetAwaiter().GetResult();
            if (response != null && response.IsSuccess)
            {
               list = JsonConvert.DeserializeObject<List<OrderHeaderDto>>(Convert.ToString(response.Result));
               switch(status)
                {
                    case StaticData.Status_Approved:
                        list = list.Where(u => u.OrderStatus == status).ToList();
                        break;
                    case StaticData.Status_ReadyForPickup:
                        list = list.Where(u => u.OrderStatus == status).ToList();
                        break;
                    case StaticData.Status_Cancelled:
                        list = list.Where(u => u.OrderStatus == status).ToList();
                        break;
                    default:
                        break;
                }
            }
            else
            {
                list = new List<OrderHeaderDto>();
            }
            return Json(new { data = list });
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int orderId)
        {
            OrderHeaderDto orderHeaderDto = new OrderHeaderDto();
            string userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;

            ResponseDto response = _orderService.GetOrder(orderId).GetAwaiter().GetResult();
            if (response != null && response.IsSuccess)
            {
                orderHeaderDto = JsonConvert.DeserializeObject<OrderHeaderDto>(Convert.ToString(response.Result));
            }
            if (!User.IsInRole(StaticData.RoleAdmin) && userId != orderHeaderDto.UserId)
            {
                return NotFound();
            }
            return View(orderHeaderDto);
        }

        [HttpPost("ReadyForPickup")]
        public async Task<IActionResult> ReadyForPickup(int orderId)
        {
            ResponseDto response =await _orderService.UpdateOrderStatus(orderId,StaticData.Status_ReadyForPickup);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Status updated successfully";
                return RedirectToAction("Index");
            }
            return RedirectToAction(nameof(Detail),new { orderId=orderId});
        }

        [HttpPost("CancelOrder")]
        public async Task<IActionResult> CancelOrder(int orderId)
        {
            ResponseDto response = await _orderService.UpdateOrderStatus(orderId, StaticData.Status_Cancelled);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Status updated successfully";
                return RedirectToAction("Index");
            }
            return RedirectToAction(nameof(Detail), new { orderId = orderId });
        }

        [HttpPost("CompleteOrder")]
        public async Task<IActionResult> CompleteOrder(int orderId)
        {
            ResponseDto response = await _orderService.UpdateOrderStatus(orderId, StaticData.Status_Completed);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Status updated successfully";
                return RedirectToAction("Index");
            }
            return RedirectToAction(nameof(Detail), new { orderId = orderId });
        }

        [HttpPost("ApproveOrder")]
        public async Task<IActionResult> ApproveOrder(int orderId)
        {
            ResponseDto response = await _orderService.UpdateOrderStatus(orderId, StaticData.Status_Approved);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Status updated successfully";
                return RedirectToAction("Index");
            }
            return RedirectToAction(nameof(Detail), new { orderId = orderId });
        }
    }
}
