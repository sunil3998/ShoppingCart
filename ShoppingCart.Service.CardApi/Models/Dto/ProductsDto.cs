using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.Service.CartApi.Models.Dto
{
    public class ProductsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public string ImageUrl { get; set; }
        public bool IsActive { get; set; }
        public bool IsSuccess { get; set; }
    }
}
