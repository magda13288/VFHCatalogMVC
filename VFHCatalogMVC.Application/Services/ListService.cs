using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VFHCatalogMVC.Application.Interfaces;
using VFHCatalogMVC.Application.ViewModels.Common;

namespace VFHCatalogMVC.Application.Services
{
    public class ListService:IListService
    {
        public List<SelectListItem> GetSelectListItem<T>(IEnumerable<T> entity) where T : SelectListItemVm
        {

            if (!entity.Any()) return new List<SelectListItem>();

            return entity
                           .OrderBy(p => p.Id)
                           .Where(e => e.Id != 0 && !string.IsNullOrEmpty(e.Name))
                           .Select(e => new SelectListItem
                           {
                               Value = e.Id.ToString(),
                               Text = e.Name
                           })
                           .Prepend(new SelectListItem { Text = "-Select-", Value = "0", Disabled = true })
                           .ToList();
        }

        public List<T> Paginate<T>(IEnumerable<T> items, int pageSize, int? pageNo)
        {
            if (!pageNo.HasValue || pageNo <= 0)
            {
                pageNo = 1; // default first page
            }

            return items.Skip(pageSize * (pageNo.Value - 1)).Take(pageSize).ToList();
        }
    }

}
