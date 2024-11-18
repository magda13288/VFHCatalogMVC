using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VFHCatalogMVC.Application.HelperClasses;
using VFHCatalogMVC.Application.ViewModels.Plant;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Application.Interfaces.PlantInterfaces
{
    public interface IPlantHelperService
    {
        List<PlantTypesVm> GetPlantTypes();
        List<PlantGroupsVm> GetPlantGroups(int? typeId);
        List<PlantSectionsVm> GetPlantSections(int? groupId);
        List<SelectListItem> PlantTypes();
        List<SelectListItem> PlantColors();
        List<SelectListItem> PlantGrowingSeaznos();
        List<GrowthTypeVm> GetGrowthTypes(int typeId, int? groupId, int? sectionId);
        List<DestinationsVm> GetDestinations();
        List<ColorsVm> GetColors();
        List<GrowingSeazonVm> GetGrowingSeazons();
        List<FruitSizeVm> GetFruitSize(int typeId, int groupId, int? sectionId);
        List<FruitTypeVm> GetFruitType(int typeId, int groupId, int? sectionId);
        //string UserAccountName(Task<ApplicationUser> user);
        MessageDisplay MessagesToView(int type);
        IndexPlantType GetIndexPlantType(bool seeds, bool seedlings, bool newPlant);
        List<SelectListItem> FillPropertyList(List<PlantTypesVm> list, List<ColorsVm> colorList, List<GrowingSeazonVm> seazonList);
    }
}
