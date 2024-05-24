using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentaCar.DAL;
using RentaCar.Models;
using RentaCar.ViewModels.Home;

namespace RentaCar.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public HomeController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<IActionResult> Index()
            
        {
            var ourFeaturedCars =await  _appDbContext.ourFeaturedCars.ToListAsync();
            var components = await _appDbContext.CategoryComponents.ToListAsync();

            var bookservices = await _appDbContext.bookServices.ToListAsync();
            var categories = await _appDbContext.Categories
                                                   .Include(c => c.CategoryComponents)
                                                  
                                                   .ToListAsync();

            var model = new HomewIndexViewModel
            {
                OurFeaturedCars = ourFeaturedCars,  
                BookServices = bookservices,
                Categories =categories,
                Components = components 
               
            };
            return View(model);
        }
    }
}
