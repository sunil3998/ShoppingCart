namespace ShoppingCart.Service.CartApi.Models.Dto
{
    public class CouponsDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public double Discount { get; set; }
        public double MinimumAmount { get; set; }
        public byte[] CouponPicture { get; set; }
        public bool IsActive { get; set; }
    }
}
