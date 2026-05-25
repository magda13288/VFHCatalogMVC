using AutoMapper;
using VFHCatalogMVC.Application.Mapping;
using VFHCatalogMVC.Application.ViewModels.Common;

namespace VFHCatalogMVC.Application.ViewModels.Adresses
{
    public class CityVm: SelectListItemVm, IMapFrom<VFHCatalogMVC.Domain.Model.City>
    {    
        public int RegionId { get; set; }
    
        public void Mapping(Profile profile)
        {
            profile.CreateMap<VFHCatalogMVC.Domain.Model.City, CityVm>();
        }
    }
}
