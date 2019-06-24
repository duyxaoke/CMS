using CMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Application.ViewModels.CMS
{
    public class ContentMappingViewModel : BaseViewModel, ILocalizedModelLocal
    {
        public int ContentId { get; set; }
        public int LanguageId { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Description { get; set; }
    }
}
