namespace ShoppingCart.Web.Models
{
    public class RegistrationRequestDto
    {
        public string EmailId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
