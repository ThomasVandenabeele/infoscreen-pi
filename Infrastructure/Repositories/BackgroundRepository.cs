using InfoScreenPi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfoScreenPi.Infrastructure.Repositories
{
    public class BackgroundRepository : EntityBaseRepository<Background>, IBackgroundRepository
    {
        public BackgroundRepository(InfoScreenContext context)
            : base(context)
        { }
    }
}