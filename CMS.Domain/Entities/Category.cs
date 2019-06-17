using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Domain.Entities
{
    [Table("Category", Schema = "CMS")]
    public class Category : BaseEntity
    {
        public int ParentID { get; set; }
        public int ModuleID { get; set; }
        [Column(TypeName = "nvarchar")]
        [StringLength(250)]
        public string CategoryName { get; set; }
        [Column(TypeName = "nvarchar")]
        [StringLength(4000)]
        public string Description { get; set; }
        public bool IsActive { get; set; }

    }
}
