using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Application.ViewModels.Plant;

namespace VFHCatalogMVC.Application.Interfaces.PlantInterfaces
{
    public interface IPlantDetailsSerrvice
    {
        int AddPlantDetails(NewPlantVm model);
        PlantDetailsVm GetPlantDetails(int id);
        List<string> GetGrowthTypesNames(int id);
        List<string> GetDestinationsNames(int id);
        List<string> GetGrowingSeaznosNames(int id);
        //List<GrowthTypeVm> GetGrowthTypes(int typeId, int? groupId, int? sectionId);
        //List<DestinationsVm> GetDestinations();
        //List<ColorsVm> GetColors();
        //List<GrowingSeazonVm> GetGrowingSeazons();
        //List<FruitSizeVm> GetFruitSize(int typeId, int groupId, int? sectionId);
        //List<FruitTypeVm> GetFruitType(int typeId, int groupId, int? sectionId);




    }
}
