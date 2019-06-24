﻿using CMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Application.ViewModels.CMS
{
    public class ContentModel : ILocalizedModel<ContentMappingModel>
    {
        public ContentModel()
        {
            Locales = new List<ContentMappingModel>();
            Languages = new List<Language>();
        }

        public int Id { get; set; }
        public string ContentName { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedTime { get; set; }
        public List<ContentMappingModel> Locales { get; set; }
        public List<Language> Languages { get; set; }
    }

    public class ContentMappingModel : ILocalizedModelLocal
    {
        public int Id { get; set; }
        public int ContentId { get; set; }
        public int LanguageId { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Description { get; set; }
    }
}
