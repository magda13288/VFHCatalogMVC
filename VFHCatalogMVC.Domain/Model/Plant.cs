using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Web.Mvc;

namespace VFHCatalogMVC.Domain.Model
{
    public class Plant
    {
        public int Id { get; set; }
        public int PlantTypeId { get; set; }
        public virtual PlantType PlantType { get; set; }
        public int PlantGroupId { get; set; }
        public virtual PlantGroup PlantGroup { get; set; }
        public int? PlantSectionId { get; set; }
        public virtual PlantSection PlantSection { get; set; }
        public string FullName { get; set; }
        public string Photo { get; set; }
        public bool isActive { get; set; }


        // TypeId,GroupId, NameId, AvailabilityId jeden do wielu - jeden typ może być przypisany do wielu plantów     

        //wiele do wielu - kilka plantów może mieć przypisane kilka tagów i na odwrót

        public PlantDetail PlantDetail { get; set; }
        public TypeOfAvailability TypeOfAvailability { get; set; }
        public ICollection<PlantTag> PlantTags { get; set; }
        public ICollection<UserPlantSharing> UserPlantSharings { get; set; }
        public ICollection<CustomerPlantsForSale> CustomerPlantsForSale { get; set; }
        
    }
}
