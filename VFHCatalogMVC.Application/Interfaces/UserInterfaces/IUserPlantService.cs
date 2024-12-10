using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Application.Services.PlantServices;
using VFHCatalogMVC.Application.ViewModels.Adresses;
using VFHCatalogMVC.Application.ViewModels.Plant.Common;
using VFHCatalogMVC.Application.ViewModels.Plant.PlantSeedlings;
using VFHCatalogMVC.Application.ViewModels.Plant.PlantSeeds;
//using VFHCatalogMVC.Application.ViewModels.Plant;
using VFHCatalogMVC.Application.ViewModels.User;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Application.Interfaces.UserInterfaces
{
    public interface IUserPlantService
    {       
        List<string> FilterUsers(int countryId, int regionId, int cityId, List<PlantItemVm> items);
        UserSeedsForListVm GetUserSeeds(int pageSize, int? pageNo, string searchString, int typeId, int groupId, int? sectionId, string userName);
        UserSeedlingsForListVm GetUserSeedlings(int pageSize, int? pageNo, string searchString, int typeId, int groupId, int? sectionId, string userName);
        UserSeedVm GetUserSeedToEdit(int id);
        void UpdateSeed(UserSeedVm seed);
        void DeleteSeed(int id);
        UserSeedlingVm GetUserSeedlingToEdit(int id);
        void UpdateSeedling(UserSeedlingVm seedling);
        void DeleteSeedling(int id);
        ContactDetail GetContactDetail(int? id);
        int? GetContactDetailForSeed(int id);
        int? GetContactDetailForSeedling(int id);
        void AddNewUserPlant(int plantId, string userId);
        NewUserPlantsForListVm GetNewUserPlants(int pageSize, int? pageNo, string searchString, int typeId, int groupId, int? sectionId, bool viewAll, string userName);


    }
}
