using System;
using System.Collections.Generic;
using System.Text;

namespace VFHCatalogMVC.Domain.Model
{
    public class PlantPosition
    {
        public int PositionId { get; set; }
        public Position Position { get; set; }
        public int PlantDetailId { get; set; }
        public PlantDetail PlantDetail { get; set; }
    }
}
