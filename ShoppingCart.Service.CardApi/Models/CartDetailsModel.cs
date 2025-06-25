using ShoppingCart.Service.CartApi.Models.Dto;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingCart.Service.CartApi.Models
{
    [Table("CartDetails")]
    public class CartDetailsModel
    {
        [Key]
        public int Id { get; set; }
        public int CartHeaderId { get; set; }
        public CartHeaderModel? CartHeaders { get; set; }
        public int ProductId { get; set; }
        [NotMapped]
        public ProductsDto? Product { get; set; }
        public int Count { get; set; }
    }
}
