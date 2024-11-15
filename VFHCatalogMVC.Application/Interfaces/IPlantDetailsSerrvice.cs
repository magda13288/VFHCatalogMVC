using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Application.ViewModels.Plant;

namespace VFHCatalogMVC.Application.Interfaces
{
    public interface IPlantDetailsSerrvice
    {
        int AddPlantDetails(NewPlantVm model);
        PlantDetailsVm GetPlantDetails(int id);
        List<string> GetGrowthTypesNames(int id);
        List<string> GetDestinationsNames(int id);
        List<string> GetGrowingSeaznosNames(int id);


    }
}
