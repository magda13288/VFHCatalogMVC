using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
//using System.Web.Mvc;
using VFHCatalogMVC.Application.HelperClasses;
using VFHCatalogMVC.Application.ViewModels.Plant.PlantDetails;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Application.Interfaces.PlantInterfaces
{
    public interface IPlantHelperService
    {
        List<SelectListItem> GetSelectList<TSource, TViewModel>()
        where TViewModel : SelectListItemVm
        where TSource : class;
        List<SelectListItem> GetGroups(int? typeId);
        List<SelectListItem> GetSections(int? groupId);        
        List<SelectListItem> GetGrowthTypes(int typeId, int? groupId, int? sectionId);
        List<SelectListItem> GetFruitSize(int typeId, int groupId, int? sectionId);
        List<SelectListItem> GetFruitTypes(int typeId, int groupId, int? sectionId);
        List<PlantGroupsVm> GetGroupsJR(int typeId);

        //methods with ends "JR" used in Views and used by JS
        List<PlantSectionsVm> GetSectionsJR(int groupId); 
        List<GrowthTypeVm> GetGrowthTypesJR(int typeId, int? groupId, int? sectionId);
        List<DestinationsVm> GetDestinationsJR();
        List<FruitSizeVm> GetFruitSizeJR(int typeId, int groupId, int? sectionId);
        List<FruitTypeVm> GetFruitTypeJR(int typeId, int groupId, int? sectionId);
        MessageDisplay MessagesToView(int type);
        IndexPlantType GetIndexPlantType(bool seeds, bool seedlings, bool newPlant);
     
    }
}
