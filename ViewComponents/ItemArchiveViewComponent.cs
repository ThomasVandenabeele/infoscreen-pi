using InfoScreenPi.ViewModels;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using InfoScreenPi.Infrastructure;
using InfoScreenPi.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using InfoScreenPi.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace InfoScreenPi.ViewComponents
{
    public class ItemArchiveViewComponent : ViewComponent
    {
        private readonly IUserRepository _userRepository;
        private readonly IItemRepository _itemRepository;

        public ItemArchiveViewComponent(IUserRepository userRepository, IItemRepository itemRepository)
        {
            _userRepository = userRepository;
            _itemRepository = itemRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Item> model = _itemRepository.AllIncluding(a => a.Background, a => a.Soort).Where(i => i.Soort.Description != "RSS" && i.Archieved == true).ToList();
            return View(model);
        }        
    }
}
