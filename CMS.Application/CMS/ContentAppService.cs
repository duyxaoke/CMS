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
    public class ContentAppService : IContentAppService
    {
        IContentRepository _repository;

        public ContentAppService(IContentRepository repository)
        {
            _repository = repository;
        }
        
        public async Task<ContentModel> GetByIdAsync(object id)
        {
            return Mapper.Map<Content, ContentModel>(await _repository.GetByIdAsync(id));
        }

        public IQueryable<Content> GetAllPaging()
        {
            return _repository.GetAllPaging();
        }

        public async Task<IEnumerable<ContentModel>> GetAllAsync()
        {
            return Mapper.Map<IEnumerable<Content>, IEnumerable<ContentModel>>(await _repository.GetAllAsync());
        }

        public void Add(ContentModel entity)
        {
            var entityAdd = Mapper.Map<ContentModel, Content>(entity);
            _repository.Add(entityAdd);
        }

        public void Update(ContentModel entity)
        {
            var entityUpdt = Mapper.Map<ContentModel, Content>(entity);
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
