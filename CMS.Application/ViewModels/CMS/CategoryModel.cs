using CMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Application.ViewModels.CMS
{
    public class CategoryModel : ILocalizedModel<CategoryMappingModel>
    {
        public CategoryModel()
        {
            Locales = new List<CategoryMappingModel>();
            Languages = new List<Language>();
        }

        public int Id { get; set; }
        public string CategoryName { get; set; }
        public int? ParentId { get; set; }
        public int ModuleId { get; set; }
        public bool IsActive { get; set; }
        public IList<CategoryMappingModel> Locales { get; set; }
        public IList<Language> Languages { get; set; }
    }

    public class CategoryMappingModel : ILocalizedModelLocal
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int LanguageId { get; set; }
        [Column(TypeName = "nvarchar")]
        [StringLength(250)]
        public string Title { get; set; }
        [Column(TypeName = "nvarchar")]
        [StringLength(4000)]
        public string Description { get; set; }
    }
}
