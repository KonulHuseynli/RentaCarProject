﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RentaCar.Areas.Admin.ViewModels.Account;
using RentaCar.Constants;
using RentaCar.Models;

namespace RentaCar.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AccountLoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _userManager.FindByNameAsync(model.Username);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "USERNAME or PASSWORD is wrong!");
                return View(model);
            }

            if (!await _userManager.IsInRoleAsync(user, UserRoles.Admin.ToString()))
            {
                ModelState.AddModelError(string.Empty, "USERNAME or PASSWORD is wrong!");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "USERNAME or PASSWORD is wrong!");
                return View(model);
            }
            return RedirectToAction("index", "dashboard");
        }
        [HttpGet]

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("login");
        }
    }
}
