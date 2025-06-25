using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingCart.Service.CartApi.Models
{
    [Table("CartHeaders")]
    public class CartHeaderModel
    {
        [Key]
        public int Id { get; set; }
        public string? UserId { get; set; }
        public string? CouponCode { get; set; }
        [NotMapped]
        public double Discount { get; set; }
        [NotMapped]
        public double CartTotal { get; set; }
        // Consider initializing the Details collection to avoid null reference exceptions
        public ICollection<CartDetailsModel> CartDetailsModels { get; set; } = new List<CartDetailsModel>();
    }
}
