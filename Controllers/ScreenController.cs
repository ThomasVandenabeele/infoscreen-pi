using System.Collections.Generic;
using System.Linq;
using InfoScreenPi.Models;
using InfoScreenPi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InfoScreenPi.Controllers
{
    public class ScreenController : Controller
    {

        private InfoScreenContext _context;

        public ScreenController(InfoScreenContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            
            List<ItemViewModel> lijst = _context.Items
                            .Include(i => i.Soort)
                            .Include(i => i.Background)
                            .Where(i => i.Active == true && i.Archieved == false).ToList<Item>()
                            .Select(item => new ItemViewModel(item)).ToList<ItemViewModel>();

            return View(lijst);
        }
    }
}