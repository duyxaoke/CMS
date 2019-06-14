using AutoMapper;
using CMS.Application.ViewModels;
using CMS.Application.ViewModels.CMS;
using CMS.Domain.Entities;

namespace CMS.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<MenuViewModel, Menu>();

            // CMS
            CreateMap<CategoryViewModel, Category>();
            CreateMap<ConfigViewModel, Config>();
            CreateMap<PartnerViewModel, Partner>();
        }
    }
}