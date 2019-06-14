using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Domain.Entities
{
    [Table("Partner", Schema = "CMS")]
    public class Partner : BaseEntity
    {
        [Column(TypeName = "nvarchar")]
        [StringLength(250)]
        public string Name { get; set; }

        public string Logo { get; set; }

        [Column(TypeName = "nvarchar")]
        [StringLength(1000)]
        public string Description { get; set; }


    }
}
