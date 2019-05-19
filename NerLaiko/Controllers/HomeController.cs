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
        private readonly RoleManager<IdentityRole> _rolemanager;
        private readonly UserManager<ApplicationUser> _usermanger;

        public HomeController(UserManager<ApplicationUser> usermanger)
        {
            _usermanger = usermanger;
        }

        public IActionResult Index()
        {
            return RedirectToAction("FridgeList", "Fridge");
        }

        [Authorize]
        public async Task<IActionResult> GiveAdmin()
        {
           // await _rolemanager.CreateAsync(new IdentityRole("Admin"));
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
