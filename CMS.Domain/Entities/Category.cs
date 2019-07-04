using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Domain.Entities
{
    [Table("Category", Schema = "CMS")]
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }
        [Column(TypeName = "nvarchar")]
        [StringLength(250)]
        public string CategoryName { get; set; }
        public int? ParentId { get; set; }
        public int ModuleId { get; set; }
        public bool IsActive { get; set; }
        public virtual IList<CategoryMapping> Locales { get; set; }

    }
}
