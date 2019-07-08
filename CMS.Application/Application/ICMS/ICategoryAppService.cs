using CMS.Application.ViewModels.CMS;
using CMS.Domain.Entities;

namespace CMS.Application.Application.ICMS
{
    public interface ICategoryAppService : IApplication<CategoryModel, Category>
    {
        void UpdateMapping(CategoryModel entity);
    }
}
