using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using eTickets.V8.Data;
using eTickets.V8.Data.ViewModels;
using eTickets.V8.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace eTickets.V8.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signManager;
        private readonly AppDbContext _context;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signManager, AppDbContext context)
        {
            _userManager = userManager;
            _signManager = signManager;
            _context = context;
        }

        public async Task<IActionResult> Login()
        {
            var response = new LoginVM();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            //model state ini berpatokan pada VM yang kita buat sebagai contoh required, sehingga ketika tidak diisi  maka tidak valid model statenya 
            if (!ModelState.IsValid)
            {
                return View(loginVM);
            }
            //cari dulu email ada atau nga di db
            var user = await _userManager.FindByEmailAsync(loginVM.emailAddress);
            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);
                //conditional jika password benar
                if (passwordCheck)
                {
                    var result = await _signManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                    if (result.Succeeded)
                    {
                        //Index merupakan nama method, dan "Moviex" adalah nama controller dibawah ini
                        return RedirectToAction("Index", "Movies");
                    }
                }
                TempData["Error"] = "password is incorrect";
                return View(loginVM);
            }
            TempData["Error"] = "Email is incorrect";
            return View(loginVM);
        }

        public IActionResult Register()
        {
            var response = new RegisterVM();
            return View(response);
        }

        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
            {
                return View(registerVM);
            }
        }
    }
}