using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
//using System.Web.Mvc;
using VFHCatalogMVC.Application.HelperClasses;
using VFHCatalogMVC.Application.ViewModels.Common;
using VFHCatalogMVC.Application.ViewModels.Plant.Common;
using VFHCatalogMVC.Domain.Common;

namespace VFHCatalogMVC.Application.Interfaces.PlantInterfaces
{
    public interface IPlantHelperService
    {
        List<SelectListItem> GetSelectList<TSource, TViewModel>()
        where TViewModel : SelectListItemVm
        where TSource : class;
        //List<SelectListItem> GetSelectListItem<T>(IEnumerable<T> entity) where T : SelectListItemVm;
        List<SelectListItem> GetGroups(int? typeId);
        List<SelectListItem> GetSections(int? groupId);
        List<SelectListItem> GetDestinations();
        MessageDisplay MessagesToView(int type);
        IndexPlantType GetIndexPlantType(bool seeds, bool seedlings, bool newPlant);

        List<SelectListItem> GetPlantPropertySelectListItem<TSource, TVm, TSourceList, TVmList>(int typeId, int? groupId, int? sectionId)
           where TSource : class
           where TSourceList : BasePropertyForListFilters
           where TVm : SelectListItemVm
           where TVmList : PlantPropertyForListFiltersVm;


    }
}
