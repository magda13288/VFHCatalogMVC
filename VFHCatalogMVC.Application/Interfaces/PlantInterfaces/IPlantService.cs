using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Application.ViewModels.Plant;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using VFHCatalogMVC.Application.ViewModels.Plant.PlantSeeds;
using VFHCatalogMVC.Application.ViewModels.Plant.PlantSeedlings;
using VFHCatalogMVC.Application.ViewModels.Plant.Common;


namespace VFHCatalogMVC.Application.Interfaces.PlantInterfaces
{
    public interface IPlantService
    {
        Task<ListPlantForListVm> GetAllActivePlantsForListAsync(int pageSize, int? pageNo, string searchString, int? typeId, int? groupId, int? sectionId);
        Task<int> AddPlantAsync(NewPlantVm model, string user);
        Task AddPlantSeedAsync(PlantSeedVm seed);
        Task AddPlantSeedlingAsync(PlantSeedlingVm seedling);
        Task<NewPlantVm> GetPlantToEditAsync(int id);
        Task UpdatePlantAsync(NewPlantVm model);
        Task<PlantForListVm> DeletePlantAsync(int id);
        Task<T> FillPropertyAsync<T>(
             int id,
             string userName
             )
                where T : PlantItemVm, new();

        Task<PlantSeedsForListVm> GetAllPlantSeedsAsync(int id, int countryId, int regionId, int cityId, int pageSize, int? pageNo, bool isCompany, string userName);
        Task<PlantSeedlingsForListVm> GetAllPlantSeedlingsAsync(int id, int countryId, int regionId, int cityId, int pageSize, int? pageNo, bool isCompany);
        Task ActivatePlantAsync(int id);
        
    }
}
