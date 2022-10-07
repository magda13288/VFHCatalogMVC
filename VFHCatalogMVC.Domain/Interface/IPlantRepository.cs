using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Domain.Interface
{
    public interface IPlantRepository
    {
        void DeletePlant(int plantId);
        int AddPlant(Plant plant);
        int EditPlant(Plant plant);
        int AddPlantDetails(PlantDetail plant, int plantId);
        void AddPlantGrowthTypes(int[] growthTypesIds, int plantDetailId);
        void AddPlantDestinations(int[] plantDestinationsIds, int plantDestId);
        void AddPlantGrowingSeazons(int[] growingSeazonsIds, int plantDestId);
        PlantDetail GetPlantDetails(int id);
        string GetPlantColorName(int id);
        string GetPlantFruitSizeName(int id);
        string GetPlantFriutTypeName(int id);
        IQueryable<PlantGrowthType> GetPlantGrowthTypes(int id);
        IQueryable<PlantDestination> GetPlantDestinations(int id);
        IQueryable<PlantGrowingSeazon> GetPlantGrowingSeazons(int id);
        IQueryable<Plant> GetAllActivePlants(); //zwraca konkretny model bazodanowy (z konkretnej tabeli w bazie)
        //IQueryable<Plant> GetPlantByTypeId(int typeId);
        //IQueryable<Plant> GetPlantByGroupId(int groupId);
        //IQueryable<Plant> GetPlantBySectionId(int sectionId);
        Plant GetPlantById(int plantId);
        IQueryable<PlantType> GetAllTypes();
        IQueryable<PlantGroup> GetAllGroups();
        IQueryable<PlantSection> GetAllSections();
        IQueryable<GrowthType> GetGrowthTypes();
        IQueryable<Destination> GetDestinations();
        IQueryable<Color> GetColors();
        IQueryable<GrowingSeazon> GetGrowingSeazons();
        IQueryable<FruitSize> GetFruitSizes();
        IQueryable<FruitType> GetFruitTypes();
     
    }
}
