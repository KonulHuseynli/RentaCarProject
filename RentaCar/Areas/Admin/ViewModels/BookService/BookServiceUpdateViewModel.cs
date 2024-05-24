
namespace RentaCar.Areas.Admin.ViewModels.BookService
{
    public class BookServiceUpdateViewModel
    {
        public int Id { get; set; } 
        public string Title { get; set; }  
        public string Description { get; set; }

        public IFormFile? MainPhoto { get; set; }


        public string? MainPhotoName { get; set; }
    }
}
