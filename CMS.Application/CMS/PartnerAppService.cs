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
    public class PartnerAppService : IPartnerAppService
    {
        IPartnerRepository _repository;

        public PartnerAppService(IPartnerRepository repository)
        {
            _repository = repository;
        }
        
        public async Task<PartnerViewModel> GetByIdAsync(object id)
        {
            return Mapper.Map<Partner, PartnerViewModel>(await _repository.GetByIdAsync(id));
        }

        public IQueryable<Partner> GetAllPaging()
        {
            return _repository.GetAllPaging();
        }

        public async Task<IEnumerable<PartnerViewModel>> GetAllAsync()
        {
            return Mapper.Map<IEnumerable<Partner>, IEnumerable<PartnerViewModel>>(await _repository.GetAllAsync());
        }

        public void Add(PartnerViewModel entity)
        {
            var entityAdd = Mapper.Map<PartnerViewModel, Partner>(entity);
            _repository.Add(entityAdd);
        }

        public void Update(PartnerViewModel entity)
        {
            var entityUpdt = Mapper.Map<PartnerViewModel, Partner>(entity);
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
