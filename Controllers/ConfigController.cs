using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InfoScreenPi.Models;
namespace InfoScreenPi.Controllers
{
    public class ConfigController : Controller
    {
        private InfoScreenContext _context;

        public ConfigController(InfoScreenContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
           

            return View(_context.Users.ToList());
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
