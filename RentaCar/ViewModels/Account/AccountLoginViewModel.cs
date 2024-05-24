using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace RentaCar.ViewModels.Account
{
    public class AccountLoginViewModel
    {
        [Required]
        public string  Username { get; set; }

        [Required, MaxLength(100), DataType(DataType.Password)]
        public string Password { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
