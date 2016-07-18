using System;
using System.Collections.Generic;
using System.Linq;
using InfoScreenPi.Entities;
using InfoScreenPi.ViewModels;
using InfoScreenPi.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InfoScreenPi.Controllers
{
    public class ScreenController : Controller
    {

        private InfoScreenContext _context;
        private readonly IHostingEnvironment _hostEnvironment;

        public ScreenController(InfoScreenContext context, IHostingEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            List<string> tickerData = new List<string>(System.IO.File.ReadAllLines(_hostEnvironment.WebRootPath + "/data/ticker.txt")); 
            TempData["TickerData"] = tickerData;

            //List<string> lst= TempData["TickerData"] as List<string>;

            List<ItemViewModel> lijst = _context.Items
                            .Include(i => i.Soort)
                            .Include(i => i.Background)
                            .Where(i => i.Active == true && i.Archieved == false).ToList<Item>()
                            .Select(item => new ItemViewModel(item)).ToList<ItemViewModel>();

            return View(lijst);
        }
    }
}