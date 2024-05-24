using System.ComponentModel.DataAnnotations;

namespace RentaCar.Areas.Admin.ViewModels.Category
{
    public class CategoryCreateViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The name must be filled"), MinLength(3, ErrorMessage = "The length of the name must be at least 3")]

        public string Title { get; set; }
    }
}
