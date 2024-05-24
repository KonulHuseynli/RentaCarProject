using RentaCar.Models;

namespace RentaCar.ViewModels.Home
{
    public class HomewIndexViewModel
    {
        public List<OurFeaturedCar> OurFeaturedCars { get; set; }
        public List<BookService> BookServices { get; set; }
        public List<Category> Categories { get; set; }
        public List<CategoryComponent> Components { get; set; }

    }
}
