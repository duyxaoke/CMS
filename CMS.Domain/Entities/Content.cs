using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Domain.Entities
{
    [Table("Content", Schema = "CMS")]
    public class Content : BaseEntity
    {
        public string ContentName { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedTime { get; set; }
        public virtual IList<ContentMapping> Locales { get; set; }
    }
}
