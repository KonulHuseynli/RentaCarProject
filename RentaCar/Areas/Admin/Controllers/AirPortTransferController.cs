using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentaCar.Areas.Admin.ViewModels.AirPortTransfer;
using RentaCar.Areas.Admin.ViewModels.BookService;
using RentaCar.DAL;
using RentaCar.Helpers;
using RentaCar.Models;

namespace RentaCar.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class AirPortTransferController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IFileService _fileService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AirPortTransferController(AppDbContext appDbContext, IFileService fileService, IWebHostEnvironment webHostEnvironment)
        {
            _appDbContext = appDbContext;
            _fileService = fileService;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            var model = new AirPortTransferIndexViewModel
            {
                AirPortTransfers = await _appDbContext.airPortTransfers.ToListAsync()
            };
            return View(model);
        }
        #region Create
        [HttpGet]

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(AirPortTransferCreateViewModel model)
        {


            if (!ModelState.IsValid) return View(model);


            bool isExist = await _appDbContext.airPortTransfers.AnyAsync(p => p.Brand.ToLower().Trim() == model.Brand.ToLower().Trim());
            if (isExist)
            {
                ModelState.AddModelError("Brand", "Bu adda brand movcuddur");
                return View(model);
            }
            if (!_fileService.IsImage(model.MainPhoto))
            {
                ModelState.AddModelError("MainPhoto", "File image formatinda deyil zehmet olmasa image formasinda secin!!");
                return View(model);
            }
            if (!_fileService.CheckSize(model.MainPhoto, 300))
            {
                ModelState.AddModelError("MainPhoto", "File olcusu 300 kbdan boyukdur");
                return View(model);
            }


            //if (hasError) { return View(model); }
            if (model.CreateDate == null)
            {
                model.CreateDate = DateTime.Today;
            }


            var product = new AirPortTransfer
            {
                Brand = model.Brand,
                Description = model.Description,
                Model = model.Model,
                Price = model.Price,
                CreateDate = model.CreateDate.Value,
                FilePath = await _fileService.UploadAsync(model.MainPhoto, _webHostEnvironment.WebRootPath)


            };

            await _appDbContext.airPortTransfers.AddAsync(product);
            await _appDbContext.SaveChangesAsync();




            return RedirectToAction("Index");
        }
        #endregion
        #region Update 
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var blog = await _appDbContext.airPortTransfers.FindAsync(id);

            if (blog == null) return NotFound();

            var model = new AirPortTransferUpdateViewModel
            {

                Brand = blog.Brand,
                Model = blog.Model,
                Price = blog.Price,
                Description = blog.Description,
                CreateDate = blog.CreateDate,
                MainPhotoName = blog.FilePath,


            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(AirPortTransferUpdateViewModel model, int id)
        {
            var brand = await _appDbContext.airPortTransfers.FindAsync(id);

            if (!ModelState.IsValid) return View(model);

            if (id != model.Id) return BadRequest();

            bool isExits = await _appDbContext.airPortTransfers.AnyAsync(p => p.Brand.ToLower().Trim() == model.Brand.ToLower().Trim() && p.Id != brand.Id);

            if (isExits)
            {
                ModelState.AddModelError("brand", "Bu brend movcuddur");
                return View(model);
            }
            brand.Brand = model.Brand;
            brand.Model = model.Model;
            brand.Price = model.Price;
            brand.Description = model.Description;
            brand.CreateDate = model.CreateDate;
            model.MainPhotoName = brand.FilePath;


            if (model.MainPhoto != null)
            {

                if (!_fileService.IsImage(model.MainPhoto))
                {
                    ModelState.AddModelError("Photo", "Image formatinda secin");
                    return View(model);
                }
                if (!_fileService.CheckSize(model.MainPhoto, 300))
                {
                    ModelState.AddModelError("Photo", "Sekilin olcusu 300 kb dan boyukdur");
                    return View(model);
                }
                brand.FilePath = await _fileService.UploadAsync(model.MainPhoto, _webHostEnvironment.WebRootPath);
            }


            await _appDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion
        #region Details
        [HttpGet]

        public async Task<IActionResult> Details(int id)
        {
            var brand = await _appDbContext.airPortTransfers.FindAsync(id);

            if (brand == null) return NotFound();

            var model = new AirPortTransferDetailsViewModel
            {
                Id = brand.Id,
                Brand = brand.Brand,
                Model=brand.Model,
                Price=brand.Price,
                Description = brand.Description,
                CreateDate = brand.CreateDate,
                MainPhotoName = brand.FilePath,


            };
            return View(model);

        }
        #endregion
        #region Delete
        [HttpGet]

        public async Task<IActionResult> Delete(int id)
        {
            var blog = await _appDbContext.airPortTransfers.FindAsync(id);
            if (blog == null) return NotFound();

            _appDbContext.airPortTransfers.Remove(blog);

            await _appDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion
    }
}