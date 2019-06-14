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
    public class ConfigAppService : IConfigAppService
    {
        IConfigRepository _repository;

        public ConfigAppService(IConfigRepository repository)
        {
            _repository = repository;
        }
        
        public async Task<ConfigViewModel> GetByIdAsync(object id)
        {
            return Mapper.Map<Config, ConfigViewModel>(await _repository.GetByIdAsync(id));
        }

        public IQueryable<Config> GetAllPaging()
        {
            return _repository.GetAllPaging();
        }

        public async Task<IEnumerable<ConfigViewModel>> GetAllAsync()
        {
            return Mapper.Map<IEnumerable<Config>, IEnumerable<ConfigViewModel>>(await _repository.GetAllAsync());
        }

        public void Add(ConfigViewModel entity)
        {
            var entityAdd = Mapper.Map<ConfigViewModel, Config>(entity);
            _repository.Add(entityAdd);
        }

        public void Update(ConfigViewModel entity)
        {
            var entityUpdt = Mapper.Map<ConfigViewModel, Config>(entity);
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
