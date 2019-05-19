using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NerLaiko.Models;

namespace NerLaiko.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _usermanger;
        private readonly RoleManager<IdentityRole> _rolemanager;

        public HomeController(UserManager<ApplicationUser> usermanger, RoleManager<IdentityRole> rolemanager)
        {
            _usermanger = usermanger;
            _rolemanager = rolemanager;
        }

        public IActionResult Index()
        {
            var us = User.IsInRole("Admin");
            return RedirectToAction("FridgeList", "Fridge");
        }

        [Authorize]
        public async Task<IActionResult> GiveAdmin()
        {
            await _rolemanager.CreateAsync(new IdentityRole("Admin"));
            await _usermanger.AddToRolesAsync(await _usermanger.GetUserAsync(User), new[] { "Admin" });
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
