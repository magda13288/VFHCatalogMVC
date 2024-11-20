﻿using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Application.ViewModels.Plant;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

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
        PlantSeedVm FillProperties(int id, string userName);
        PlantSeedlingVm FillPropertiesSeedling(int id, string userName);
        PlantSeedsForListVm GetAllPlantSeeds(int id, int countryId, int regionId, int cityId, int pageSize, int? pageNo, bool isCompany, string userName);
        PlantSeedlingsForListVm GetAllPlantSeedlings(int id, int countryId, int regionId, int cityId, int pageSize, int? pageNo, bool isCompany);
        List<PlantSeedVm> FilterSeedsList(List<PlantSeedVm> seeds, List<string> filteredUsersList);
        List<PlantSeedlingVm> FilterSeedlingsList(List<PlantSeedlingVm> seedlings, List<string> filteredUsersList);
        void ActivatePlant(int id);
        
    }
}