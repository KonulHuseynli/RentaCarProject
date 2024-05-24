using Microsoft.AspNetCore.Identity;

namespace RentaCar.Models
{
    public class User:IdentityUser
    {
        public string Fullname { get; set; }
    }
}
