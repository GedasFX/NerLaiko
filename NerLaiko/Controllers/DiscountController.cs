using Microsoft.AspNetCore.Mvc;

namespace NerLaiko.Controllers
{
    public class DiscountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}