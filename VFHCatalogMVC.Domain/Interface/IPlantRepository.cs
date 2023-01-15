using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Domain.Interface
{
    public interface IPlantRepository
    {
        void DeletePlant(Plant plant);
        int AddPlant(Plant plant);
        int AddPlantSeed(PlantSeed seed);
        int AddContactDetail(ContactDetail contact);
        void AddContactDetailForSeed(ContactDetailForSeed contact);
        void AddContactDetailForSeedling(ContactDetailForSeedling contact);
        int AddPlantSeedling(PlantSeedling seedling);
        void AddPlantOpinion(PlantOpinion opinion);
        int EditPlant(Plant plant);
        void UpdatePlant(Plant plant);
        void UpdatePlantDetails(PlantDetail plant); 
        int AddPlantDetails(PlantDetail plant, int plantId);
        void DeletePlantDestinations(int id);
        void DeletePlantGrowingSeazons(int id);
        void DeletePlantGrowthTypes(int id);
        void AddPlantGrowthTypes(int[] growthTypesIds, int plantDetailId);
        void AddPlantDestinations(int[] plantDestinationsIds, int plantDestId);
        void AddPlantGrowingSeazons(int[] growingSeazonsIds, int plantDestId);
        void AddPlantDetailsImages(string fileName, int plantDetailId);
        int GetPlantDetailId(int id);
        void DeleteImageFromGallery(int id);
        IQueryable<PlantDetailsImages> GetPlantDetailsImages(int plantDetailId);
        PlantDetail GetPlantDetails(int id);
        string GetPlantColorName(int? id);
        string GetPlantFruitSizeName(int? id);
        string GetPlantFriutTypeName(int? id);
        IQueryable<PlantGrowthType> GetPlantGrowthTypes(int id);
        IQueryable<PlantDestination> GetPlantDestinations(int id);
        IQueryable<PlantGrowingSeazon> GetPlantGrowingSeazons(int id);
        IQueryable<Plant> GetAllActivePlants(); //zwraca konkretny model bazodanowy (z konkretnej tabeli w bazie)    
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
        IQueryable<PlantOpinion> GetPlantOpinions(int id);
        IQueryable<PlantSeed> GetPlantSeeds(int id);
        IQueryable<PlantSeedling> GetPlantSeedlings(int id);
        Plant GetPlantToActivate(int id);
        void ActivatePlant(Plant plant);



    }
}
