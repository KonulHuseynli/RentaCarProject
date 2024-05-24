using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace RentaCar.Areas.Admin.ViewModels.CategoryComponent
{
    public class CategoryComponentIndexViewModel
    {
        public List<Models.CategoryComponent> CategoryComponents { get; set; }


        #region Filter

        public string? Title { get; set; }
        public List<SelectListItem> Categories { get; set; }

        [Display(Name = "Category")]
        public int? CategoryId { get; set; }


        [Display(Name = " SubTitle")]
        public string? SubTitle { get; set; }


        [Display(Name = "Daily Price")]
        public string? DailyPrice { get; set; }


        [Display(Name = "Weekly Price")]
        public string? WeeklyPrice { get; set; }


        [Display(Name = "Date ")]
        public int? Date { get; set; }

        [Display(Name = "Seat")]
        public string? Seat { get; set; }


        [Display(Name = "FuelType")]
        public string? FuelType { get; set; }

        [Display(Name = "Auto")]
        public string? Auto { get; set; }

        #endregion

    }
}
