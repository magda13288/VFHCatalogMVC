using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VFHCatalogMVC.Domain.Common;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Domain.Interface
{
    public interface IPlantRepository
    {
        Task<int> AddEntityAsync<T>(T entity) where T : BaseEntity;
        Task<int> AddContactDetailsEntityAsync<T>(T entity) where T : class;
        IQueryable<T> GetPlantDetailsById<T>(int id) where T : BasePlantDetailKeyProperty;
        Task<string> GetPlantDetailsPropertyNameAsync<T>(int? id) where T : BasePlantEntityNameProperty;
        IQueryable<T> GetAllEntities<T>() where T : class;
        IQueryable<PlantOpinion> GetPlantOpinions(int id);
        Task<int> DeletePlantDetailEntityAsync<T>(int id) where T : BasePlantDetailKeyProperty;
        Task DeletePlantAsync(Plant plant);
        Task<int> AddContactDetailAsync(ContactDetail contact);
        Task<int> UpdatePlantAsync(Plant plant);
        Task<int> UpdatePlantDetailsAsync(PlantDetail plant); 
        Task<int> AddPlantDetailsAsync(PlantDetail plant, int plantId);
        Task<int> AddPlantGrowthTypesAsync(int[] growthTypesIds, int plantDetailId);
        Task<int> AddPlantDestinationsAsync(int[] plantDestinationsIds, int plantDestId);
        Task<int> AddPlantGrowingSeazonsAsync(int[] growingSeazonsIds, int plantDestId);
        Task AddPlantDetailsImagesAsync(string fileName, int plantDetailId);
        Task<int> GetPlantDetailIdAsync(int id);
        Task DeleteImageFromGalleryAsync(int id);
        IQueryable<PlantDetailsImages> GetPlantDetailsImages(int plantDetailId);
        Task<PlantDetail> GetPlantDetailsAsync(int id);     
        Task<IQueryable<Plant>> GetAllActivePlantsAsync(); //zwraca konkretny model bazodanowy (z konkretnej tabeli w bazie)    
        Task<Plant> GetPlantByIdAsync(int plantId);
        IQueryable<T> GetPlantSeedOrSeedling<T>(int id) where T : BasePlantSeedSeedlingProperty;
        Task<Plant> GetPlantToActivateAsync(int id);
        void ActivatePlant(Plant plant);

        IQueryable<T> GetEntitiesForListFilters<T>(int typeId, int? groupId, int? sectionId) where T : BasePropertyForListFilters;

    }
}
