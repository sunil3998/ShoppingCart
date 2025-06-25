namespace ShoppingCart.Service.AuthApi.Models
{
    public class JwtOptions
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string audience { get; set; }
    }
}
