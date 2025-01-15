using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Application.ViewModels.Common;

namespace VFHCatalogMVC.Application.Interfaces
{
    public interface IListService
    {
        List<SelectListItem> GetSelectListItem<T>(IEnumerable<T> entity) where T : SelectListItemVm;
        List<T> Paginate<T>(IEnumerable<T> items, int pageSize, int? pageNo);

    }
}
