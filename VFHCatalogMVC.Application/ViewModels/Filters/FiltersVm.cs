using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Application.Mapping;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Application.ViewModels.Filters
{
    public class FiltersVm:IMapFrom<VFHCatalogMVC.Domain.Model.Filters>
    {
        public int Id { get; set; }
        public int PlantTypeId { get; set; }
        public int? PlantGroupId { get; set; }
        public int? PlantSectionId { get; set; }
        public bool Color { get; set; }
        public bool Destination { get; set; }
        public bool FruitSizeVisible { get; set; }
        public int? FruitSizeId { get; set; }
        public bool FruitTypeVisible { get; set; }
        public int? FruitTypeId { get; set; }
        public bool GrowingSeazon { get; set; }
        //public int? GrowingSeazonId { get; set; }
        public bool GrowthTypeVisible { get; set; }
        public int? GrowthTypeId { get; set; }
        public bool HeightVisible { get; set; }
        public int? HeightId { get; set; }
        public bool PollinationVisible { get; set; }
        public int? PollinationId { get; set; }
        public bool Position { get; set; }
        public bool AdditionalFeatures { get; set; }
        public FiltersValuesVm FiltersValues { get; set; }
        

        public void Mapping(Profile profile)
        {
            profile.CreateMap<VFHCatalogMVC.Domain.Model.Filters, FiltersVm>();
        }
    }
}
