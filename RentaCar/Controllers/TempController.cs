using Microsoft.AspNetCore.Mvc;

namespace RentaCar.Controllers
{
    public class TempController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
