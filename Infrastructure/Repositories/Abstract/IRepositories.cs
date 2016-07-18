using InfoScreenPi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfoScreenPi.Infrastructure.Repositories
{
    public interface IItemRepository : IEntityBaseRepository<Item> { }

    public interface IBackgroundRepository : IEntityBaseRepository<Background> { }

    public interface ILoggingRepository : IEntityBaseRepository<Error> { }

    public interface IItemKindRepository : IEntityBaseRepository<ItemKind> { }

    public interface IRoleRepository : IEntityBaseRepository<Role> { }

    public interface IUserRepository : IEntityBaseRepository<User>
    {
        User GetSingleByUsername(string username);
        IEnumerable<Role> GetUserRoles(string username);
    }

    public interface IUserRoleRepository : IEntityBaseRepository<UserRole> { }
}