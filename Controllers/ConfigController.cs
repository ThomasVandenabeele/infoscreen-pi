using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InfoScreenPi.Entities;
using InfoScreenPi.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using InfoScreenPi.ViewModels;
using InfoScreenPi.Infrastructure.Services;
using InfoScreenPi.Infrastructure.Repositories;
using InfoScreenPi.Infrastructure.Core;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace InfoScreenPi.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    public class ConfigController : Controller
    {
        private InfoScreenContext _context;
        private readonly IMembershipService _membershipService;
        private readonly IUserRepository _userRepository;
        private readonly ILoggingRepository _loggingRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IHostingEnvironment _hostEnvironment;

        public ConfigController(InfoScreenContext context, 
                                IMembershipService membershipService,
                                IUserRepository userRepository,
                                ILoggingRepository _errorRepository,
                                IItemRepository itemRepository,
                                IHostingEnvironment hostEnvironment)
        {
            _context = context;
            _membershipService = membershipService;
            _userRepository = userRepository;
            _loggingRepository = _errorRepository;
            _itemRepository = itemRepository;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            if(HttpContext.Session.GetString("Username") != null) ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.ActiveItems = (List<Item>) _itemRepository.AllIncluding(a => a.Background, a => a.Soort).Where(i => i.Soort.Description != "RSS" && i.Archieved == false).ToList();
            ViewBag.TickerItems = new List<string>(System.IO.File.ReadAllLines(_hostEnvironment.WebRootPath + "/data/ticker.txt")); 
            return View(_context.Users.ToList());
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel user)
        {
            IActionResult _result = new ObjectResult(false);
            GenericResult _authenticationResult = null;

            try
            {
                MembershipContext _userContext = _membershipService.ValidateUser(user.Username, user.Password);

                if (_userContext.User != null)
                {
                    IEnumerable<Role> _roles = _userRepository.GetUserRoles(user.Username);
                    List<Claim> _claims = new List<Claim>();
                    foreach (Role role in _roles)
                    {
                        Claim _claim = new Claim(ClaimTypes.Role, "Admin", ClaimValueTypes.String, user.Username);
                        _claims.Add(_claim);
                    }

                    await HttpContext.Authentication.SignInAsync("MyCookieMiddlewareInstance",
                        new ClaimsPrincipal(new ClaimsIdentity(_claims, CookieAuthenticationDefaults.AuthenticationScheme)),
                        new Microsoft.AspNetCore.Http.Authentication.AuthenticationProperties {IsPersistent = false });


                    _authenticationResult = new GenericResult()
                    {
                        Succeeded = true,
                        Message = "Authentication succeeded"
                    };

                    User loggedUser = _userRepository.GetSingleByUsername(user.Username);
                    HttpContext.Session.SetInt32("Id", loggedUser.Id);
                    HttpContext.Session.SetString("Username", loggedUser.Username);

                    return RedirectToAction("Index", "Config");
                }
                else
                {
                    _authenticationResult = new GenericResult()
                    {
                        Succeeded = false,
                        Message = "Authentication failed"
                    };
                    return View(user);
                }
            }
            catch (Exception ex)
            {
                _authenticationResult = new GenericResult()
                {
                    Succeeded = false,
                    Message = ex.Message
                };

                _loggingRepository.Add(new Error() { Message = ex.Message, StackTrace = ex.StackTrace, DateCreated = DateTime.Now });
                _loggingRepository.Commit();
            }

            _result = new ObjectResult(_authenticationResult);
            return View(user);
        }

        public async Task<IActionResult> LogOut()
        {
            try
            {
                await HttpContext.Authentication.SignOutAsync("MyCookieMiddlewareInstance");
                HttpContext.Session.Clear();
                return RedirectToAction("Login","Config");
            }
            catch (Exception ex)
            {
                _loggingRepository.Add(new Error() { Message = ex.Message, StackTrace = ex.StackTrace, DateCreated = DateTime.Now });
                _loggingRepository.Commit();

                return BadRequest();
            }
            
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

        [HttpPost]
        public ActionResult SaveTicker(List<string> listkey)
        {
            var n = listkey;
            System.IO.File.WriteAllLines(_hostEnvironment.WebRootPath + "/data/ticker.txt", listkey.Where(str => str != null));
            return Json(new {success= true, message="Ingevoerde data voor de infoticker opgeslagen!"});
        }

        [HttpPost]
        public ActionResult ChangeItemState(int id, bool state)
        {
            Item item = _itemRepository.GetSingle(id);
            if (item != null)
            {
                item.Active = state;
                _itemRepository.Edit(item);
                _itemRepository.Commit();
                return Json(new {success = true, message = (state? "Item status verandert naar actief!" : "Item status verandert naar inactief!")});    
            }

            return Json(new {success = false, message = "Update is niet gelukt!"});
        }

        [HttpPost]
        public ActionResult ArchiveItem(int id, bool state)
        {
            Item item = _itemRepository.GetSingle(id);
            if (item != null)
            {
                item.Archieved = state;
                _itemRepository.Edit(item);
                _itemRepository.Commit();
                return Json(new {success = true, message = (state? "Item verwijderd" : "Item terug geactiveerd")});    
            }

            return Json(new {success = false, message = "Archieveren is niet gelukt!"});
        }

        [HttpGet]
        public ActionResult ItemsArchive()
        {
            List<Item> model = _itemRepository.AllIncluding(a => a.Background, a => a.Soort).Where(i => i.Soort.Description != "RSS" && i.Archieved == true).ToList();
            return PartialView(model);
        }

        [HttpGet]
        public ActionResult ItemsTable()
        {
            List<Item> model = _itemRepository.AllIncluding(a => a.Background, a => a.Soort).Where(i => i.Soort.Description != "RSS" && i.Archieved == false).ToList();
            return PartialView(model);
        }

    }
}
