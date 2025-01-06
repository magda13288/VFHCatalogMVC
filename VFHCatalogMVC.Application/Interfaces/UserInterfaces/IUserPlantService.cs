using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VFHCatalogMVC.Application.Services.PlantServices;
using VFHCatalogMVC.Application.ViewModels.Adresses;
using VFHCatalogMVC.Application.ViewModels.Plant.Common;
using VFHCatalogMVC.Application.ViewModels.Plant.PlantSeedlings;
using VFHCatalogMVC.Application.ViewModels.Plant.PlantSeeds;
//using VFHCatalogMVC.Application.ViewModels.Plant;
using VFHCatalogMVC.Application.ViewModels.User;
using VFHCatalogMVC.Domain.Common;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Application.Interfaces.UserInterfaces
{
    public interface IUserPlantService
    {       
        Task<List<string>> FilterUsersAsync(int countryId, int regionId, int cityId, List<PlantItemVm> items);
        Task<UserSeedsForListVm> GetUserSeedsAsync(int pageSize, int? pageNo, string searchString, int typeId, int groupId, int? sectionId, string userName);
        Task<UserSeedlingsForListVm> GetUserSeedlingsAsync(int pageSize, int? pageNo, string searchString, int typeId, int groupId, int? sectionId, string userName);
        Task<UserSeedVm> GetUserSeedToEditAsync(int id);
        Task<UserSeedlingVm> GetUserSeedlingToEditAsync(int id);
        Task DeleteItemAsync<T>(int id)  where T : BaseEntityProperty;
        Task UpdateSeedAsync(UserSeedVm seed);
        Task UpdateSeedlingAsync(UserSeedlingVm seedling);       
        Task<ContactDetail> GetContactDetailAsync(int? id);
        Task<int?> GetContactDetailForPlantAsync(int id, Func<int, Task<int?>> getContact);
        Task AddNewUserPlantAsync(int plantId, string userId);
        Task<NewUserPlantsForListVm> GetNewUserPlantsAsync(int pageSize, int? pageNo, int typeId, int groupId, int? sectionId, bool viewAll, string userName);


    }
}
