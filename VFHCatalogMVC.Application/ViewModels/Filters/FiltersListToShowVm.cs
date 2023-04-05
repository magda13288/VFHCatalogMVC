using System;
using System.Collections.Generic;
using System.Text;

namespace VFHCatalogMVC.Application.ViewModels.Filters
{
    //W partiaView Index serwis będzie sprawdzzał, czy lista jest pusta. Jeśli tak to filtr niewidoczny jeśli nie to filtr będzie widoczny
    public class FiltersListToShowVm
    {
        //public List<ColorsVm> Colors { get; set; }
        //public List<DestinationsVm> Destinations { get; set; }
        //public List<FruitSizeVm> FruitSizes { get; set; }
        //public List<FruitTypeVm> FruitTypes { get; set; }
        //public List<GrowingSeazonVm> GrowingSeazons { get; set; }
        //public List<GrowthTypeVm> GrowthTypes { get; set; }
        //public List<HeightVm> Heights { get; set; }
        //public List<PollinationVm> Pollinations { get; set; }
        //public List<PositionVm> Positions { get; set; }
        //public List<AdditionalFeaturesVm> AdditionalFeatures { get; set; }
        public List<FiltersValuesVm> Filters { get; set; }
        public int? CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public FiltersValuesVm FilterValues { get; set; }
    }
}
