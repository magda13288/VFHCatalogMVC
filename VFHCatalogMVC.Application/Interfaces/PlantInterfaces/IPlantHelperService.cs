using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
//using System.Web.Mvc;
using VFHCatalogMVC.Application.HelperClasses;
using VFHCatalogMVC.Application.ViewModels.Plant.Common;
using VFHCatalogMVC.Application.ViewModels.Plant.PlantDetails;
using VFHCatalogMVC.Domain.Common;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Application.Interfaces.PlantInterfaces
{
    public interface IPlantHelperService
    {
        Task<List<SelectListItem>> GetSelectListAsync<TSource, TViewModel>()
        where TViewModel : SelectListItemVm
        where TSource : class;
        Task<List<SelectListItem>> GetSelectListItemAsync<T>(IEnumerable<T> entity) where T : SelectListItemVm;
        Task<List<SelectListItem>> GetGroupsAsync(int? typeId);
        Task<List<SelectListItem>> GetSectionsAsync(int? groupId);
        Task<List<SelectListItem>> GetDestinationsAsync();
        MessageDisplay MessagesToView(int type);
        IndexPlantType GetIndexPlantType(bool seeds, bool seedlings, bool newPlant);

        Task<List<SelectListItem>> GetPlantPropertySelectListItemAsync<TSource, TVm, TSourceList, TVmList>(int typeId, int? groupId, int? sectionId)
           where TSource : class
           where TSourceList : BasePropertyForListFilters
           where TVm : SelectListItemVm
           where TVmList : PlantPropertyForListFiltersVm;


    }
}
