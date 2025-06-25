using ShoppingCart.Service.CardsApi.Models.Dto;

namespace ShoppingCart.Service.CartApi.Models.Dto
{
    public class CartDto
    {
        public CartHeaderDto CartHeader  { get; set; }
        public IEnumerable<CartDetailsDto>? CartDetails { get; set; }
    }
}
