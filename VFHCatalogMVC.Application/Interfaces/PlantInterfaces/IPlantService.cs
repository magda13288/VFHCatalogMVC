using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Application.ViewModels.Plant;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using VFHCatalogMVC.Application.ViewModels.Plant.PlantSeeds;
using VFHCatalogMVC.Application.ViewModels.Plant.PlantSeedlings;


namespace VFHCatalogMVC.Application.Interfaces.PlantInterfaces
{
    public interface IPlantService
    {
        ListPlantForListVm GetAllActivePlantsForList(int pageSize, int? pageNo, string searchString, int? typeId, int? groupId, int? sectionId);
        int AddPlant(NewPlantVm model, string user);
        void AddPlantSeed(PlantSeedVm seed);
        void AddPlantSeedling(PlantSeedlingVm seedling);
        NewPlantVm GetPlantToEdit(int id);
        void UpdatePlant(NewPlantVm model);
        PlantForListVm DeletePlant(int id);
        public T FillProperty<T>(
             int id,
             string userName
             )
                where T : PlantItemVm, new();

         PlantSeedsForListVm GetAllPlantSeeds(int id, int countryId, int regionId, int cityId, int pageSize, int? pageNo, bool isCompany, string userName);
        PlantSeedlingsForListVm GetAllPlantSeedlings(int id, int countryId, int regionId, int cityId, int pageSize, int? pageNo, bool isCompany);
        void ActivatePlant(int id);
        
    }
}
