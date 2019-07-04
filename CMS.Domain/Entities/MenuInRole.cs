using CMS.Domain.Validations;
using System;

namespace CMS.Domain.Entities
{
    public class MenuInRole
    {
        public int MenuInRoleId { get; set; }
        public Guid RoleId { get; set; }
        public int MenuId { get; set; }
    }
}
