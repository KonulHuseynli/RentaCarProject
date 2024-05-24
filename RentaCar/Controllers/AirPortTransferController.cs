using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentaCar.DAL;
using RentaCar.Models;
using RentaCar.ViewModels.Airporttransfer;
using RentaCar.ViewModels.Home;

namespace RentaCar.Controllers
{
    public class AirPortTransferController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public AirPortTransferController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<IActionResult> Index()
        {
            var airportTransfers = await _appDbContext.airPortTransfers.ToListAsync();
            var model = new AirPortTransferIndexViewModel
            {
                AirPortTransfers = airportTransfers
            };
            return View(model);
        }
    }
}
