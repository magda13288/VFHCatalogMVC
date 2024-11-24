using System;
using System.Collections.Generic;
using System.Text;

namespace VFHCatalogMVC.Application.ViewModels.Plant
{
    public class PlantListVm<TVm>
    {
        public int PlantId { get; set; }
        public int PageSize { get; set; }
        public int? CurrentPage { get; set; }
        public int Count { get; set; }
        public bool isCompany { get; set; }
        public string LoggedUserName { get; set; }
        public List<TVm> Items { get; set; }
    }
}
