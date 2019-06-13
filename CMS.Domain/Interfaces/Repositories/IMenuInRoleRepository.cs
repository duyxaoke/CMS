using CMS.Domain.Entities;
using System;
using System.Collections.Generic;

namespace CMS.Domain.Interfaces.Repositories
{
    public interface IMenuInRoleRepository : IRepositoryBase<MenuInRole>
    {
        IEnumerable<MenuInRole> GetMenuByRoleId(Guid roleId);
    }
}
