using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
//using System.Web.Mvc;
using VFHCatalogMVC.Application.HelperClasses;
using VFHCatalogMVC.Application.ViewModels.Plant.Common;
using VFHCatalogMVC.Application.ViewModels.Plant.PlantDetails;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Application.Interfaces.PlantInterfaces
{
    public interface IPlantHelperService
    {
        List<SelectListItem> GetSelectList<TSource, TViewModel>()
        where TViewModel : SelectListItemVm
        where TSource : class;
        List<SelectListItem> GetSelectListItem<T>(IEnumerable<T> entity) where T : SelectListItemVm;
        List<SelectListItem> GetGroups(int? typeId);
        List<SelectListItem> GetSections(int? groupId);        
        List<SelectListItem> GetGrowthTypes(int typeId, int? groupId, int? sectionId);
        List<TVm> FilterFruitSizeOrType<TSource, TVm>(int typeId, int groupId, int? sectionId)
            where TSource : class
            where TVm : SelectListItemVm;
        List<SelectListItem> GetFruitPropertyList<TSource, TViewModel>(int typeId, int groupId, int? sectionId)
           where TViewModel : SelectListItemVm
           where TSource : class;
        //methods with ends "JR" used in Views and used by JS
        List<PlantGroupsVm> GetGroupsJR(int typeId);
        List<TVm> GetPropertiesListJR<TVm, TSource>(int typeId, int groupId, int? sectionId)
          where TSource : class
          where TVm : SelectListItemVm;   
        List<PlantSectionsVm> GetSectionsJR(int groupId); 
        List<GrowthTypeVm> GetGrowthTypesJR(int typeId, int? groupId, int? sectionId);
        List<DestinationsVm> GetDestinationsJR();
        MessageDisplay MessagesToView(int type);
        IndexPlantType GetIndexPlantType(bool seeds, bool seedlings, bool newPlant);
     
    }
}
