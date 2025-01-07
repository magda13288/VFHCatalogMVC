using System;
using System.Collections.Generic;
using System.Text;

namespace VFHCatalogMVC.Application.ViewModels.Plant.Common
{
    public class SelectListItemVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PlantTypeId { get; set; }
        public int? PlantGroupId { get; set; }
        public int? PlantSectionId { get; set; }
    }
}
