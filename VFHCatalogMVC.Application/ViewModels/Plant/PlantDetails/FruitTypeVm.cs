using AutoMapper;
using VFHCatalogMVC.Application.Mapping;
using VFHCatalogMVC.Application.ViewModels.Common;

namespace VFHCatalogMVC.Application.ViewModels.Plant.PlantDetails
{
    public class FruitTypeVm : SelectListItemVm, IMapFrom<Domain.Model.FruitType>
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Model.FruitType, FruitTypeVm>();
        }
    }
}
