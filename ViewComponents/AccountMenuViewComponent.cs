using InfoScreenPi.ViewModels;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using InfoScreenPi.Infrastructure;
using InfoScreenPi.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using InfoScreenPi.Entities;
using System.Threading.Tasks;

namespace InfoScreenPi.ViewComponents
{
    public class AccountMenuViewComponent : ViewComponent
    {
        private readonly IUserRepository _userRepository;

        public AccountMenuViewComponent(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            int? id = HttpContext.Session.GetInt32("Id");
         
            if (id != null && id != 0)
            {
                User gebruiker = await _userRepository.GetSingleAsync((int)id);
                return View(new AccountViewModel(gebruiker));
            }
            else return View();
        }        
    }
}
