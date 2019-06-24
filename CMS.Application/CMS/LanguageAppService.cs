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
    public class LanguageAppService : ILanguageAppService
    {
        ILanguageRepository _repository;

        public LanguageAppService(ILanguageRepository repository)
        {
            _repository = repository;
        }
        
        public async Task<LanguageViewModel> GetByIdAsync(object id)
        {
            return Mapper.Map<Language, LanguageViewModel>(await _repository.GetByIdAsync(id));
        }

        public IQueryable<Language> GetAllPaging()
        {
            return _repository.GetAllPaging();
        }

        public async Task<IEnumerable<LanguageViewModel>> GetAllAsync()
        {
            return Mapper.Map<IEnumerable<Language>, IEnumerable<LanguageViewModel>>(await _repository.GetAllAsync());
        }

        public void Add(LanguageViewModel entity)
        {
            var entityAdd = Mapper.Map<LanguageViewModel, Language>(entity);
            _repository.Add(entityAdd);
        }

        public void Update(LanguageViewModel entity)
        {
            var entityUpdt = Mapper.Map<LanguageViewModel, Language>(entity);
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
