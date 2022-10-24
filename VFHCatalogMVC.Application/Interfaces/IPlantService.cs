using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Application.ViewModels.Plant;
using System.Linq;

namespace VFHCatalogMVC.Application.Interfaces
{
    public interface IPlantService
    {
        ListPlantForListVm GetAllActivePlantsForList(int pageSize,int? pageNo, string searchString, int? typeId,int? groupId,int? sectionId);
        PlantDetailsVm GetPlantDetails(int id);
        List<PlantTypesVm> GetPlantTypes();
        List<PlantGroupsVm> GetPlantGroups(int? typeId);
        List<PlantSectionsVm> GetPlantSections(int? groupId);
        int AddPlant(NewPlantVm model);
        List<GrowthTypeVm> GetGrowthTypes(int typeId, int groupId, int? sectionId);
        List<DestinationsVm> GetDestinations();
        List<ColorsVm> GetColors();
        List<GrowingSeazonVm> GetGrowingSeazons();
        List<FruitSizeVm> GetFruitSize(int typeId, int groupId, int? sectionId);
        List<FruitTypeVm> GetFruitType(int typeId, int groupId, int? sectionId);
        NewPlantVm GetPlantToEdit(int id);
        void UpdatePlant(NewPlantVm model);
        PlantForListVm DeletePlant(int id);

  

    }
}
