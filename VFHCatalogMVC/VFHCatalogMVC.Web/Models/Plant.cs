namespace VFHCatalogMVC.Web.Models
{
    public class Plant
    {
   
        public int Id { get; set; }     
        public int IdType { get; set; }    
        public int GroupId { get; set; }
        public int NameId { get; set; }       
        public string FullName { get; set; }
        public string Type { get; set; }
        public string Color { get; set; }     
        public string Destination { get; set; }     
        public string Description { get; set; }      
        public string Opinion { get; set; }

        public Plant()
        { }

        public Plant(PlantMethodParameters plantParameters)
        {
            Id = plantParameters.Id;
            IdType = plantParameters.IdType;
            GroupId = plantParameters.GroupId;
            NameId = plantParameters.NameId;
            FullName = plantParameters.FullName;
            Type = plantParameters.Type;
            Color = plantParameters.Color;
            Destination = plantParameters.Destination;
            Description = plantParameters.Description;
            Opinion = plantParameters.Opinion;
        }
        public class PlantMethodParameters : Plant
        {
            public PlantMethodParameters()
            { }
            public PlantMethodParameters(Plant plant)
            {
                Id = plant.Id;
                IdType = plant.IdType;
                GroupId = plant.GroupId;
                NameId = plant.NameId;
                FullName = plant.FullName;
                Type = plant.Type;
                Color = plant.Color;
                Destination = plant.Destination;
                Description = plant.Description;
                Opinion = plant.Opinion;
            }
        }
    }
}
