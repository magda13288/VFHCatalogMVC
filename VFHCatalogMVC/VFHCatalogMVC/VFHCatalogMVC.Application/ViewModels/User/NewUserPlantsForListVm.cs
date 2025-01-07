using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Application.ViewModels.Plant;

namespace VFHCatalogMVC.Application.ViewModels.User
{
    public class NewUserPlantsForListVm
    {
        public List<NewUserPlantVm> NewUserPlants { get; set; }
        public int? CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public bool ViewAll { get; set; }      
        public PlantForListVm PlantForList { get; set; }
    }
}
