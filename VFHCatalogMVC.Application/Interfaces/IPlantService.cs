using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Application.ViewModels.Plant;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace VFHCatalogMVC.Application.Interfaces
{
    public interface IPlantService
    {
        ListPlantForListVm GetAllActivePlantsForList(int pageSize,int? pageNo, string searchString, int? typeId,int? groupId,int? sectionId);
        //Task<PlantDetailsVm> GetPlantDetailsAsync(int id);
        PlantDetailsVm GetPlantDetails(int id);
        List<PlantTypesVm> GetPlantTypes();
        List<PlantGroupsVm> GetPlantGroups(int? typeId);
        List<PlantSectionsVm> GetPlantSections(int? groupId);
        int AddPlant(NewPlantVm model, string user);
        void AddPlantSeed(PlantSeedVm seed);
        void AddPlantSeedling(PlantSeedlingVm seedling);
        void AddPlantOpinion(PlantOpinionsVm opinion);
        List<GrowthTypeVm> GetGrowthTypes(int typeId, int groupId, int? sectionId);
        List<DestinationsVm> GetDestinations();
        List<ColorsVm> GetColors();
        List<GrowingSeazonVm> GetGrowingSeazons();
        List<FruitSizeVm> GetFruitSize(int typeId, int groupId, int? sectionId);
        List<FruitTypeVm> GetFruitType(int typeId, int groupId, int? sectionId);
        NewPlantVm GetPlantToEdit(int id);
        void UpdatePlant(NewPlantVm model);
        PlantForListVm DeletePlant(int id);
        List<SelectListItem> FillPropertyList(List<PlantTypesVm> list, List<ColorsVm> colorList, List<GrowingSeazonVm> seazonList);
        PlantSeedVm FillProperties(int id,string userName);
        PlantSeedlingVm FillPropertiesSeedling(int id, string userName);
        PlantOpinionsVm FillPropertyOpinion(int id, string userName);
        PlantSeedsForListVm GetAllPlantSeeds(int id, int countryId, int regionId, int cityId, int pageSize, int? pageNo, bool isCompany, string userName);
        PlantSeedlingsForListVm GetAllPlantSeedlings(int id, int countryId, int regionId, int cityId, int pageSize, int? pageNo,bool isCompany);
        List<PlantSeedVm> FilterSeedsList(List<PlantSeedVm> seeds, List<string> filteredUsersList);
        List<PlantSeedlingVm> FilterSeedlingsList(List<PlantSeedlingVm> seedlings, List<string> filteredUsersList);
        void ActivatePlant(int id);
        //List<string> Fill<T>(List<T> list) where T : class;
    }
}
