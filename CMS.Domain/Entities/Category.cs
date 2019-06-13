using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Domain.Entities
{
    [Table("Category", Schema = "CMS")]
    public class Category : BaseEntity
    {
        public int PartnerID { get; set; }

        [StringLength(50)]
        public string CategoryCode { get; set; }

        [Column(TypeName = "nvarchar")]
        [StringLength(250)]
        public string CategoryName { get; set; }
        public decimal PercentCommission { get; set; }
        public bool IsActive { get; set; }
    }
}
