using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentaCar.Areas.Admin.ViewModels.BookService;
using RentaCar.Areas.Admin.ViewModels.Category;
using RentaCar.Areas.Admin.ViewModels.CategoryComponent;
using RentaCar.DAL;
using RentaCar.Helpers;
using RentaCar.Models;
using System.Reflection.Metadata;

namespace RentaCar.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BookServiceController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IFileService _fileService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BookServiceController(AppDbContext appDbContext, IFileService fileService, IWebHostEnvironment webHostEnvironment)
        {
            _appDbContext = appDbContext;
            _fileService = fileService;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            var model = new BookServiceIndexViewModel
            {
                Services = await _appDbContext.bookServices.ToListAsync()
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

        public async Task<IActionResult> Create(BookServiceCreateViewModel model)
        {


            if (!ModelState.IsValid) return View(model);


            bool isExist = await _appDbContext.bookServices.AnyAsync(p => p.Title.ToLower().Trim() == model.Title.ToLower().Trim());
            if (isExist)
            {
                ModelState.AddModelError("Title", "Bu adda service movcuddur");
                return View(model);
            }
            if (!_fileService.IsImage(model.FilePath))
            {
                ModelState.AddModelError("FilePath", "File image formatinda deyil zehmet olmasa image formasinda secin!!");
                return View(model);
            }
            if (!_fileService.CheckSize(model.FilePath, 300))
            {
                ModelState.AddModelError("FilePath", "File olcusu 300 kbdan boyukdur");
                return View(model);
            }


            //if (hasError) { return View(model); }


            var product = new BookService
            {
                Title = model.Title,
                Description = model.Description,
                FilePath = await _fileService.UploadAsync(model.FilePath, _webHostEnvironment.WebRootPath)


            };

            await _appDbContext.bookServices.AddAsync(product);
            await _appDbContext.SaveChangesAsync();




            return RedirectToAction("Index");
        }
        #endregion
        #region Update 
        [HttpGet]

        public async Task<IActionResult> Update(int id)
        {
            var service = await _appDbContext.bookServices.FindAsync(id);

            if (service == null) return NotFound();

            var model = new BookServiceUpdateViewModel
            {
                Id = service.Id,
                Title = service.Title,
                Description = service.Description,
                MainPhotoName = service.FilePath
            };
            return View(model);
        }
        [HttpPost]

        public async Task<IActionResult> Update(BookServiceUpdateViewModel model, int id)
        {


            if (!ModelState.IsValid) return View(model);

            if (id != model.Id) return BadRequest();

            var service = await _appDbContext.bookServices.FindAsync(id);




            if (service == null) return NotFound();

            bool isExits = await _appDbContext.bookServices.AnyAsync(p => p.Title.ToLower().Trim() == model.Title.ToLower().Trim() && p.Id != model.Id);

            if (isExits)
            {
                ModelState.AddModelError("Title", "Bu service movcuddur");
                return View(model);
            }
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

                _fileService.Delete(service.FilePath, _webHostEnvironment.WebRootPath);
                service.FilePath = await _fileService.UploadAsync(model.MainPhoto, _webHostEnvironment.WebRootPath);
            }

            service.Title = model.Title;
            service.Description = model.Description;
            model.MainPhotoName = service.FilePath;

            await _appDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion
        #region Details
        [HttpGet]

        public async Task<IActionResult> Details(int id)
        {
            var service = await _appDbContext.bookServices.FindAsync(id);



            if (service == null) return NotFound();

            var model = new BookServiceDetailsViewModel
            {
                Id = service.Id,
                Title = service.Title,
                Description = service.Description,
                PhotoPath = service.FilePath,
            };

            return View(model);
        }
            #endregion
        #region Delete
            [HttpGet]

            public async Task<IActionResult> Delete(int id)
            {
                var service = await _appDbContext.bookServices.FindAsync(id);

                if (service == null) return NotFound();

                _fileService.Delete(service.FilePath, _webHostEnvironment.WebRootPath);



                _appDbContext.bookServices.Remove(service);
                await _appDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            #endregion

        }
    }
