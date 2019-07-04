﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Domain.Entities
{
    [Table("ContentMapping", Schema = "CMS")]
    public class ContentMapping
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ContentMappingId { get; set; }
        public int ContentId { get; set; }
        public virtual Content Content { get; set; }

        public int LanguageId { get; set; }
        public virtual Language Language { get; set; }

        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Description { get; set; }
    }
}
