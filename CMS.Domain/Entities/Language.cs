﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Domain.Entities
{
    [Table("Language", Schema = "CMS")]
    public class Language : BaseEntity
    {
        [Column(TypeName = "nvarchar")]
        [StringLength(100)]
        public string Name { get; set; }
        public string LanguageCulture { get; set; }
        public string UniqueSeoCode { get; set; }
        public bool IsActive { get; set; }
        public bool IsDefault { get; set; }
        public int DisplayOrder { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedTime { get; set; }
        public virtual IList<ContentMapping> ContentMappings { get; set; }

    }
}
