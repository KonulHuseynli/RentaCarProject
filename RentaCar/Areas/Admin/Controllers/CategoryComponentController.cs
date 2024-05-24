using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentaCar.Areas.Admin.ViewModels.Category;
using RentaCar.Areas.Admin.ViewModels.CategoryComponent;
using RentaCar.DAL;
using RentaCar.Models;
using RentaCar.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace RentaCar.Areas.Admin.Controllers

{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class CategoryComponentController : Controller
    {
        private readonly AppDbContext _appDbContext;

        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IFileService  _fileService;

        public CategoryComponentController(AppDbContext appDbContext, IWebHostEnvironment webHostEnvironment,IFileService fileService)
        {
            _appDbContext = appDbContext;

            _webHostEnvironment = webHostEnvironment;

            _fileService=fileService;

        }
        public async Task<IActionResult> Index(CategoryComponentIndexViewModel model)
        {

            var components = FilterProducts(model);

            model = new CategoryComponentIndexViewModel
            {
                CategoryComponents = await components.Include(p => p.Category).ToListAsync(),
                Categories = await _appDbContext.Categories.Select(c => new SelectListItem
                {
                    Text = c.Title,
                    Value = c.Id.ToString()
                })
               .ToListAsync()

            };
            return View(model);

        }

        #region Filter component
        private IQueryable<CategoryComponent> FilterProducts(CategoryComponentIndexViewModel model)
        {
            var components = FilterByTitle(model.Title);

            components = FilterByCategory(components, model.CategoryId);


            return components;
        }

        private IQueryable<CategoryComponent> FilterByTitle(string title)
        {
            return _appDbContext.CategoryComponents.Where(p => !string.IsNullOrEmpty(title) ? p.Title.Contains(title) : true);
        }

        private IQueryable<CategoryComponent> FilterByCategory(IQueryable<CategoryComponent> products, int? categoryId)
        {
            return products.Where(p => categoryId != null ? p.CategoryId == categoryId : true);
        }

        private IQueryable<CategoryComponent> FilterBySubTitle(string subtitle)
        {
            return _appDbContext.CategoryComponents.Where(p => !string.IsNullOrEmpty(subtitle) ? p.SubTitle.Contains(subtitle) : true);
        }

        private IQueryable<CategoryComponent> FilterByDailyPrice(string dailyprice)
        {
            return _appDbContext.CategoryComponents.Where(p => !string.IsNullOrEmpty(dailyprice) ? p.DailyPrice.Contains(dailyprice) : true);
        }
        private IQueryable<CategoryComponent> FilterByWeeklyPrice(string weeklyprice)
        {
            return _appDbContext.CategoryComponents.Where(p => !string.IsNullOrEmpty(weeklyprice) ? p.WeeklyPrice.Contains(weeklyprice) : true);
        }



        private IQueryable<CategoryComponent> FilterBySeat(string seat)
        {
            return _appDbContext.CategoryComponents.Where(p => !string.IsNullOrEmpty(seat) ? p.Seat.Contains(seat) : true);
        }
        private IQueryable<CategoryComponent> FilterByFuelType(string fuel)
        {
            return _appDbContext.CategoryComponents.Where(p => !string.IsNullOrEmpty(fuel) ? p.FuelType.Contains(fuel) : true);
        }

        #endregion

        #region Create

        [HttpGet]
        public async Task<IActionResult> Create()
        {


            var model = new CategoryComponentCreateViewModel
            {
                Categories = await _appDbContext.Categories.Select(c => new SelectListItem
                {
                    Text = c.Title,
                    Value = c.Id.ToString()
                }).ToListAsync()
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CategoryComponentCreateViewModel model)
        {
            model.Categories = await _appDbContext.Categories.Select(c => new SelectListItem
            {
                Text = c.Title,
                Value = c.Id.ToString()
            }).ToListAsync();


            if (!ModelState.IsValid) return View(model);
            var categorycomponent = await _appDbContext.Categories.FindAsync(model.CategoryId);

            if (categorycomponent == null)
            {
                ModelState.AddModelError("CategoryId", "Bu component movcud deyil");
                return View(model);
            }

            bool isExist = await _appDbContext.CategoryComponents.AnyAsync(p => p.Title.ToLower().Trim() == model.Title.ToLower().Trim());
            if (isExist)
            {
                ModelState.AddModelError("Title", "Bu adda component movcuddur");
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


            var product = new CategoryComponent
            {
                Title = model.Title,
                SubTitle = model.SubTitle,
                DailyPrice = model.DailyPrice,
                WeeklyPrice = model.WeeklyPrice,
                Date = model.Date,
                Seat = model.Seat,
                CategoryId = model.CategoryId,
                FuelType = model.FuelType,
                Auto = model.Auto,
                PhotoPath = await _fileService.UploadAsync(model.MainPhoto, _webHostEnvironment.WebRootPath),


            };

            await _appDbContext.CategoryComponents.AddAsync(product);
            await _appDbContext.SaveChangesAsync();


           

            return RedirectToAction("Index");

        }


        #endregion

        #region Delete
        [HttpGet]

        public async Task<IActionResult> Delete(int id)
        {
            var product = await _appDbContext.CategoryComponents.FindAsync(id);

            if (product == null) return NotFound();

            _fileService.Delete(product.PhotoPath, _webHostEnvironment.WebRootPath);

          
           
            _appDbContext.CategoryComponents.Remove(product);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #region Details
        [HttpGet]

        public async Task<IActionResult> Details(int id)
        {
            var product = await _appDbContext.CategoryComponents.FindAsync(id);



            if (product == null) return NotFound();

            var model = new CategoryComponentDetailsViewModel
            {
                Id = product.Id,
                Title = product.Title,
                SubTitle  = product.SubTitle,
                DailyPrice = product.DailyPrice,
                WeeklyPrice = product.WeeklyPrice,
                FuelType = product.FuelType,
                CategoryId = product.CategoryId,
                Auto=product.Auto,
                PhotoPath = product.PhotoPath,
                Categories = await _appDbContext.Categories.Select(c => new SelectListItem
                {
                    Text = c.Title,
                    Value = c.Id.ToString()
                }).ToListAsync()

            };

            return View(model);
        }

        #endregion

        #region Update

        [HttpGet]

        public async Task<IActionResult> Update(int id)
        {
            var product = await _appDbContext.CategoryComponents.FindAsync(id);

            if (product == null) return NotFound();

            var model = new CategoryComponentUpdateViewModel
            {
                Id = product.Id,
                Title = product.Title,
                Seat=product.Seat,
                Date = product.Date,
                SubTitle = product.SubTitle,
                DailyPrice = product.DailyPrice,
                WeeklyPrice = product.WeeklyPrice,
                FuelType = product.FuelType,
                CategoryId = product.CategoryId,
                Auto = product.Auto,
                MainPhotoName = product.PhotoPath,



                Categories = await _appDbContext.Categories.Select(c => new SelectListItem
                {
                    Text = c.Title,
                    Value = c.Id.ToString()
                }).ToListAsync()

            };

            return View(model);
        }
        [HttpPost]

        public async Task<IActionResult> Update(CategoryComponentUpdateViewModel model, int id)
        {
            model.Categories = await _appDbContext.Categories.Select(c => new SelectListItem
            {
                Text = c.Title,
                Value = c.Id.ToString()
            }).ToListAsync();

            if (!ModelState.IsValid) return View(model);

            if (id != model.Id) return BadRequest();

            var product = await _appDbContext.CategoryComponents.FindAsync(id);




            if (product == null) return NotFound();

            bool isExits = await _appDbContext.CategoryComponents.AnyAsync(p => p.Title.ToLower().Trim() == model.Title.ToLower().Trim() && p.Id != model.Id);

            if (isExits)
            {
                ModelState.AddModelError("Title", "Bu component movcuddur");
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

                _fileService.Delete(product.PhotoPath, _webHostEnvironment.WebRootPath);
                product.PhotoPath = await _fileService.UploadAsync(model.MainPhoto, _webHostEnvironment.WebRootPath);
            }

            product.Title = model.Title;
            product.SubTitle = model.SubTitle;
            product.Date = model.Date;
            product.DailyPrice = model.DailyPrice;
            product.WeeklyPrice = model.WeeklyPrice;
            product.Seat = model.Seat;
            product.FuelType = model.FuelType;
            product.Auto = model.Auto;
            model.MainPhotoName = product.PhotoPath;

            var category = await _appDbContext.Categories.FindAsync(model.CategoryId);
            if (category == null) return NotFound();
            product.CategoryId = category.Id;

            await _appDbContext.SaveChangesAsync();
            return RedirectToAction("Index");

            #endregion

        }
        }
    }
