﻿using System;
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
        int AddPlant(NewPlantVm model);
        void AddPlantSeed(NewPlantSeedVm seed);
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
        NewPlantSeedVm FillProperties(int id,string userName);

  

    }
}
