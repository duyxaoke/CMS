using AutoMapper;
using CMS.Application.Application;
using CMS.Application.Application.ICMS;
using CMS.Application.ViewModels.CMS;
using CMS.Domain.Entities;
using CMS.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Application.CMS
{
    public class CategoryAppService : ICategoryAppService
    {
        ICategoryRepository _repository;

        public CategoryAppService(ICategoryRepository repository)
        {
            _repository = repository;
        }
        
        public async Task<CategoryModel> GetByIdAsync(object id)
        {
            return Mapper.Map<Category, CategoryModel>(await _repository.GetByIdAsync(id));
        }

        public IQueryable<Category> GetAllPaging()
        {
            return _repository.GetAllPaging();
        }

        public async Task<IEnumerable<CategoryModel>> GetAllAsync()
        {
            return Mapper.Map<IEnumerable<Category>, IEnumerable<CategoryModel>>(await _repository.GetAllAsync());
        }

        public void Add(CategoryModel entity)
        {
            var entityAdd = Mapper.Map<CategoryModel, Category>(entity);
            _repository.Add(entityAdd);
        }

        public void Update(CategoryModel entity)
        {
            var entityUpdt = Mapper.Map<CategoryModel, Category>(entity);
            _repository.Update(entityUpdt);
        }

        public void Remove(object id)
        {
            _repository.Remove(id);
        }

        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}
