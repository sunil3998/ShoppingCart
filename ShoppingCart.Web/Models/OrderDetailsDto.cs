
namespace ShoppingCart.Web.Models
{
    public class OrderDetailsDto
    {
        public int OrderDetailsId { get; set; }
        public int OrdertHeaderId { get; set; }
        public int ProductId { get; set; }
        public ProductsDto? Product { get; set; }
        public int Count { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
    }
}
