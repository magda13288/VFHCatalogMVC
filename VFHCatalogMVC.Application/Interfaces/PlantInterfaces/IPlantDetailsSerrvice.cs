using System;
using System.Collections.Generic;
using System.Security.Cryptography;
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
        void UpdatePlantDestinations(NewPlantVm model);
        void UpdatePlantGrowingSeazons(NewPlantVm model);
        void UpdatePlantGrowthTypes(NewPlantVm model);
        void AddPlantOpinion(PlantOpinionsVm opinion);
        public PlantOpinionsVm FillPropertyOpinion(int id, string userName);




    }
}
