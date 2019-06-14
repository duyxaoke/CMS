using AutoMapper;
using CMS.Application.ViewModels;
using CMS.Application.ViewModels.CMS;
using CMS.Domain.Entities;

namespace CMS.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Menu, MenuViewModel>();

            // CMS
            CreateMap<Category, CategoryViewModel>();
            CreateMap<Config, ConfigViewModel>();
            CreateMap<Partner, PartnerViewModel>();
        }
    }
}