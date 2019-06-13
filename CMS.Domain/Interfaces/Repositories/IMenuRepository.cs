using CMS.Domain.Entities;
using System.Collections.Generic;

namespace CMS.Domain.Interfaces.Repositories
{
    public interface IMenuRepository : IRepositoryBase<Menu>
    {
        IEnumerable<Menu> GetParent();
        IEnumerable<Menu> GetChildren(int parentId);
    }
}
