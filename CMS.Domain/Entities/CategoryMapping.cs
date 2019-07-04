using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Domain.Entities
{
    [Table("CategoryMapping", Schema = "CMS")]
    public class CategoryMapping : BaseEntity
    {
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public int LanguageId { get; set; }
        public virtual Language Language { get; set; }

        [Column(TypeName = "nvarchar")]
        [StringLength(250)]
        public string Title { get; set; }
        [Column(TypeName = "nvarchar")]
        [StringLength(4000)]
        public string Description { get; set; }
    }
}
