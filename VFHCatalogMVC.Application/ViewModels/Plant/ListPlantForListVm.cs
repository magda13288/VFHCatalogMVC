using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VFHCatalogMVC.Application.ViewModels.Plant
{
    public class ListPlantForListVm
    {
        public List<PlantForListVm> Plants { get; set; }
        public int? CurrentPage  { get; set; }
        public int PageSize { get; set; }
        public string SearchString { get; set; }
        public int Count { get; set; } //przechowuje ilość rekordów w tabeli, wykorzystywane do paginacji na stronie
       //// [NotMapped]
       // public IFormFile Image { get; set; }
        public PlantForListVm PlantForList { get; set; }


    }
}
