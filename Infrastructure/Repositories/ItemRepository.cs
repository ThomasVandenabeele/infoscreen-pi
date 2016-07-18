using InfoScreenPi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfoScreenPi.Infrastructure.Repositories
{
    public class ItemRepository : EntityBaseRepository<Item>, IItemRepository
    {
        public ItemRepository(InfoScreenContext context)
            : base(context)
        { }
    }
}