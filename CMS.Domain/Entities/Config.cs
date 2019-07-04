using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Domain.Entities
{
    [Table("Config", Schema = "CMS")]
    public class Config
    {
        public int ConfigId { get; set; }
        [Column(TypeName = "nvarchar")]
        [StringLength(250)]
        public string Title { get; set; }

        [Column(TypeName = "nvarchar")]
        [StringLength(4000)]
        public string Keyword { get; set; }

        [Column(TypeName = "nvarchar")]
        [StringLength(4000)]
        public string Description { get; set; }

        public string Logo { get; set; }

    }
}
