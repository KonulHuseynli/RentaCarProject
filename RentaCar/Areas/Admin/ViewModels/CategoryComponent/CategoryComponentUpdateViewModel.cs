using Microsoft.AspNetCore.Mvc.Rendering;

namespace RentaCar.Areas.Admin.ViewModels.CategoryComponent
{
    public class CategoryComponentUpdateViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string DailyPrice { get; set; }
        public string Seat { get; set; }    
        public int Date { get; set; }   
        public string WeeklyPrice { get; set; }

        public string FuelType { get; set; }
        public string Auto { get; set; }
        public int CategoryId { get; set; }
    
        public List<SelectListItem>? Categories { get; set; }

        public IFormFile? MainPhoto { get; set; }


        public string? MainPhotoName { get; set; }




    }
}
