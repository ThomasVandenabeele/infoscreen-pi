using Microsoft.AspNetCore.Mvc;

namespace InfoScreenPi.Controllers
{
    public class ScreenController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}