using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VFHCatalogMVC.Application.ViewModels.Plant.PlantDetails;
using VFHCatalogMVC.Application.ViewModels.User;

namespace VFHCatalogMVC.Application.ViewModels.Plant
{
    public class PlantItemVm
    {
        public int Id { get; set; }
        public int PlantId { get; set; }
        public int Count { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public string AccountName { get; set; }
        [NotMapped]
        public string Date { get; set; }
        [NotMapped]
        public DateTime DateAdded { get; set; }
        [NotMapped]
        public List<PlantOpinionsVm> PlantOpinions { get; set; }
        [NotMapped]
        public ContactDetailVm ContactDetail { get; set; }

        [NotMapped]
        public string Link { get; set; }
        [NotMapped]
        public int Price { get; set; }
    }
}
