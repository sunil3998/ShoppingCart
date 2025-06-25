using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingCart.Service.AuthApi.Models
{
   public class ApplicationUser:IdentityUser
    {
        public string Name { get; set; }
       
    }
}
