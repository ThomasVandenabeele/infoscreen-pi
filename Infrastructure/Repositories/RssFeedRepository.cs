using InfoScreenPi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfoScreenPi.Infrastructure.Repositories
{
    public class RssFeedRepository : EntityBaseRepository<RssFeed>, IRssFeedRepository
    {
        public RssFeedRepository(InfoScreenContext context)
            : base(context)
        { }
    }
}