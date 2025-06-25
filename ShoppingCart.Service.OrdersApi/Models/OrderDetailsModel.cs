using ShoppingCart.Service.OrdersApi.Models.Dto;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingCart.Service.OrdersApi.Models
{
    public class OrderDetailsModel
    {
        [Key]
        public int OrderDetailsId { get; set; }
        public int OrderHeaderId { get; set; }
        [ForeignKey("OrderHeaderId")]
        public OrderHeaderModel OrderHeader { get; set; }
        public int ProductId { get; set; }
        [NotMapped]
        public ProductsDto? Product { get; set; }
        public int Count { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
    }
}
