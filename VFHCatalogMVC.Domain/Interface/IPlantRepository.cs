using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VFHCatalogMVC.Domain.Common;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Domain.Interface
{
    public interface IPlantRepository
    {
        IQueryable<T> GetPlantDetailsById<T>(int id) where T : BasePlantDetailKeyProperty;
        string GetPlantDetailsPropertyName<T>(int? id) where T : BasePlantEntityNameProperty;
        IQueryable<T> GetAllEntities<T>() where T : class;
        IQueryable<PlantOpinion> GetPlantOpinions(int id);
        void DeletePlantDetailEntity<T>(int id) where T : BasePlantDetailKeyProperty;
        int AddContactDetailsEntity<T>(T entity) where T : class;
        void DeletePlant(Plant plant);
        int AddPlant(Plant plant);
        int AddPlantSeed(PlantSeed seed);
        int AddContactDetail(ContactDetail contact);
        int AddPlantSeedling(PlantSeedling seedling);
        void AddPlantOpinion(PlantOpinion opinion);
        void UpdatePlant(Plant plant);
        void UpdatePlantDetails(PlantDetail plant); 
        int AddPlantDetails(PlantDetail plant, int plantId);
        void AddPlantGrowthTypes(int[] growthTypesIds, int plantDetailId);
        void AddPlantDestinations(int[] plantDestinationsIds, int plantDestId);
        void AddPlantGrowingSeazons(int[] growingSeazonsIds, int plantDestId);
        void AddPlantDetailsImages(string fileName, int plantDetailId);
        int GetPlantDetailId(int id);
        void DeleteImageFromGallery(int id);
        IQueryable<PlantDetailsImages> GetPlantDetailsImages(int plantDetailId);
        PlantDetail GetPlantDetails(int id);     
        IQueryable<Plant> GetAllActivePlants(); //zwraca konkretny model bazodanowy (z konkretnej tabeli w bazie)    
        Plant GetPlantById(int plantId);
        IQueryable<PlantSeed> GetPlantSeeds(int id);
        IQueryable<PlantSeedling> GetPlantSeedlings(int id);
        Plant GetPlantToActivate(int id);
        void ActivatePlant(Plant plant);



    }
}
