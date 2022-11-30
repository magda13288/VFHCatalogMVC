using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Application.ViewModels.Adresses;

namespace VFHCatalogMVC.Application.ViewModels.Plant
{
    public class PlantSeedlingsForListVm
    {
        public List<PlantSeedlingVm> PlantSeedlings { get; set; }
        public int? CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public AddressVm Address { get; set; }
        public int Id { get; set; }
        public bool isCustomer { get; set; }

    }
}
