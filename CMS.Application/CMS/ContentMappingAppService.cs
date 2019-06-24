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
    public class ContentMappingAppService : IContentMappingAppService
    {
        IContentMappingRepository _repository;

        public ContentMappingAppService(IContentMappingRepository repository)
        {
            _repository = repository;
        }
        
        public async Task<ContentMappingViewModel> GetByIdAsync(object id)
        {
            return Mapper.Map<ContentMapping, ContentMappingViewModel>(await _repository.GetByIdAsync(id));
        }

        public IQueryable<ContentMapping> GetAllPaging()
        {
            return _repository.GetAllPaging();
        }

        public async Task<IEnumerable<ContentMappingViewModel>> GetAllAsync()
        {
            return Mapper.Map<IEnumerable<ContentMapping>, IEnumerable<ContentMappingViewModel>>(await _repository.GetAllAsync());
        }

        public void Add(ContentMappingViewModel entity)
        {
            var entityAdd = Mapper.Map<ContentMappingViewModel, ContentMapping>(entity);
            _repository.Add(entityAdd);
        }

        public void Update(ContentMappingViewModel entity)
        {
            var entityUpdt = Mapper.Map<ContentMappingViewModel, ContentMapping>(entity);
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
