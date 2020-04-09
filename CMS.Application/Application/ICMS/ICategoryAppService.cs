using CMS.Application.ViewModels.CMS;
using CMS.Domain.Entities;
using System.Collections.Generic;

namespace CMS.Application.Application.ICMS
{
    public interface ICategoryAppService : IApplication<CategoryModel, Category>
    {
        void UpdateMapping(CategoryModel entity);
        IEnumerable<CategoryModel> GetAll();
    }
}
