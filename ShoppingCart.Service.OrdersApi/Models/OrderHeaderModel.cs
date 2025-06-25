using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.Service.OrdersApi.Models
{
    public class OrderHeaderModel
    {
        [Key]
        public int OrderHeaderId { get; set; }
        public string? UserId { get; set; }
        public string? CouponCode { get; set; }
     
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }

        /// <summary>
        /// details of the order
        /// </summary>
        public string? Discount { get; set; } 
        public double OrderTotal { get; set; }
        public DateTime OrderTime { get; set; } = DateTime.Now;
        public string? OrderStatus { get; set; } = "Pending";
        public string? PaymentStatus { get; set; } = "Pending";
        public string? PaymentIntentId { get; set; }
        public string? StripeSessionId { get; set; }
        public IEnumerable<OrderDetailsModel>? OrderDetails { get; set; }
    }
}
