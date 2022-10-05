using System;
using System.Collections.Generic;
using System.Text;

namespace VFHCatalogMVC.Application.ViewModels.Plant
{
    public class EditPlantVm
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Type { get; set; }
        public string Color { get; set; }
        public string Destination { get; set; }
        public string Description { get; set; }
        public string Opinion { get; set; }
        public byte[] Photo { get; set; }
    }
}
