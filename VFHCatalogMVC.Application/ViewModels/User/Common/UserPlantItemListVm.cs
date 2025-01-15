using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Application.ViewModels.Plant;
using VFHCatalogMVC.Application.ViewModels.User;

namespace VFHCatalogMVC.Application.ViewModels.User.Common
{
    public class UserPlantItemListVm<TVm> where TVm : class
    {
        public List<TVm> PlantItems { get; set; }
        public int? CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public string SearchString { get; set; }
        public PlantForListVm PlantForList { get; set; }
    }
}
