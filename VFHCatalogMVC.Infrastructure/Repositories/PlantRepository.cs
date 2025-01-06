using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using VFHCatalogMVC.Domain.Common;
using VFHCatalogMVC.Domain.Interface;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Infrastructure.Repositories
{
    public class PlantRepository : IPlantRepository
    {
        private Context _context;
        public PlantRepository(Context context)
        {
            _context = context;
        }

        public async Task DeletePlantAsync(Plant plant)
        {
            
           _context.Attach(plant);
           _context.Entry(plant).Property(e=>e.isActive).IsModified = true;
           await _context.SaveChangesAsync();
        }

        public async Task<int> AddEntityAsync<T>(T entity) where T : BaseEntity
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity.Id;
        }

        public async Task<int> AddContactDetailsEntityAsync<T>(T entity) where T : class
        {
            _context.Set<T>().Add(entity);
            return await _context.SaveChangesAsync();
        }
        public async Task<int> AddPlantDetailsAsync(PlantDetail plantDetail, int plantId)
        {
            plantDetail.PlantRef = plantId;
            await _context.PlantDetails.AddAsync(plantDetail);
            await _context.SaveChangesAsync();

            return plantDetail.Id;

        }
        public async Task<int> AddEntitiesAsync<T>(int[] entityIds, int plantDetailId, Func<int, int, T> createEntity) where T : class
        {
            var entities = entityIds.Select(id => createEntity(id, plantDetailId)).ToList(); //createEntity - funkcja, która tworzy instancję encji na podstawie przekazanych danych
            _context.Set<T>().AddRange(entities); //AddRange dodaje zbiorczo wszystkie encje zamiast dodwać je w pętli
            return await _context.SaveChangesAsync();
        }
        public async Task<int> AddPlantGrowthTypesAsync(int[] growthTypesIds, int plantDetailId)
        {
           return await AddEntitiesAsync<PlantGrowthType>(
                growthTypesIds,
                plantDetailId,
                (id, detailId) => new PlantGrowthType { GrowthTypeId = id, PlantDetailId = detailId });
        }

        public async Task<int> AddPlantDestinationsAsync(int[] plantDestinationsIds, int plantDetailId)
        {
           return await AddEntitiesAsync<PlantDestination>(
                plantDestinationsIds,
                plantDetailId,
                (id, detailId) => new PlantDestination { DestinationId = id, PlantDetailId = detailId });
        }

        public async Task<int> AddPlantGrowingSeazonsAsync(int[] growingSeazonsIds, int plantDetailId)
        {
          return await AddEntitiesAsync<PlantGrowingSeazon>(
                growingSeazonsIds,
                plantDetailId,
                (id, detailId) => new PlantGrowingSeazon { GrowingSeazonId = id, PlantDetailId = detailId });
        }
        public async Task AddPlantDetailsImagesAsync(string fileName, int plantDetailId)
        {

            _context.PlantDetailsImages.Add(new PlantDetailsImages { PlantDetailId = plantDetailId, ImageURL = fileName });
             await _context.SaveChangesAsync();
        }
        public async Task<PlantDetail> GetPlantDetailsAsync(int id)
        {
          return await _context.PlantDetails.FirstOrDefaultAsync(p=>p.PlantRef == id);
           
        }

        //IQueryable tworzy jedynie zapytanie do bazy danych ale nie pobiera tych danych z bazy. W serwisie bedzie metoda do pobierania danych/ wyorzystuje się to ponieważ mozna dane jeszcze przefiltrowac, co robi sie pozniej
        //public IQueryable<Plant> GetPlantByTypeId(int typeId)
        //{
        //    var plants = _context.Plants.Where(p=>p.PlantTypeId == typeId);
        //    return plants;

        //}

        //zwraca juz wynik zpytania, poniewaz nie da się już bardziej przefiltrwac danych/ zwracamy jedne wynik
        public async Task<Plant> GetPlantByIdAsync(int plantId)
        {
            var plant = await _context.Plants.AsNoTracking().FirstOrDefaultAsync(p => p.Id == plantId);
            return plant;
        }
        public async Task<string> GetPlantDetailsPropertyNameAsync<T>(int? id) where T: BasePlantEntityNameProperty
        {
            var entity = await _context.Set<T>().FirstOrDefaultAsync(p =>p.Id== id);
            if (entity == null) return null;

            //var nameProperty = typeof(T).GetProperty("Name");
            //return nameProperty?.GetValue(entity)?.ToString();

            return entity.Name;        

        }
        /// <summary>
        /// Metoda Set<T>() w Entity Framework pozwala na uzyskanie dostępu do zestawu danych dla dowolnego typu encji.
        /// Funkcja EF.Property<T>() umożliwia dostęp do właściwości w czasie wykonywania, co jest przydatne w przypadku dynamicznie określanych typów.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public IQueryable<T> GetPlantDetailsById<T>(int id) where T : BasePlantDetailKeyProperty
        {
            return _context.Set<T>().Where(p => p.PlantDetailId == id);
        }
        public IQueryable<PlantOpinion> GetPlantOpinions(int id)
        {
            return _context.PlantOpinions.Where(p=>p.PlantDetailId== id);
        }

        public IQueryable<T> GetEntitiesForListFilters<T>(int typeId, int? groupId, int? sectionId) where T:BasePropertyForListFilters
        {
            var entities = _context.Set<T>().Where(p => p.PlantTypeId == typeId && p.PlantGroupId == groupId && p.PlantSectionId == sectionId);

            return entities;

        }
        public IQueryable<T> GetAllEntities<T>() where T : class
        {
            return _context.Set<T>();
        }
        public async Task<IQueryable<Plant>> GetAllActivePlantsAsync()
        {
            var plants = _context.Plants.Where(p => p.isActive == true).OrderBy(p => p.Id);

            return plants;
        }
        public IQueryable<PlantDetailsImages> GetPlantDetailsImages(int plantDetailId)
        {
            var plantDetailsImages = _context.PlantDetailsImages.Where(p => p.PlantDetailId == plantDetailId);
            return plantDetailsImages;
        }

        public async Task<int> UpdatePlantAsync(Plant plant)
        {
            _context.Attach(plant);
            _context.Entry(plant).Property(e=>e.FullName).IsModified = true;
            _context.Entry(plant).Property(e=>e.Photo).IsModified = true;
            return await _context.SaveChangesAsync();          
        }

        public async Task<int> UpdatePlantDetailsAsync(PlantDetail plant)
        {
            _context.Attach(plant);
            _context.Entry(plant).Property(e=>e.ColorId).IsModified = true;
            _context.Entry(plant).Property(e=>e.FruitSizeId).IsModified = true;
            _context.Entry(plant).Property(e=>e.FruitSizeId).IsModified = true;
            _context.Entry(plant).Property(e=>e.Description).IsModified = true;
            _context.Entry(plant).Property(e=>e.PlantPassportNumber).IsModified = true;
            _context.Entry(plant).Property(e=>e.PlantRef).IsModified = false;
            return await _context.SaveChangesAsync();
            
        }

        public async Task<int> DeletePlantDetailEntityAsync<T>(int id) where T : BasePlantDetailKeyProperty
        {
            var entity = _context.Set<T>().Where(p => p.PlantDetailId == id); 
            _context.Set<T>().RemoveRange(entity);
           return await _context.SaveChangesAsync();
        }
        public async Task DeleteImageFromGalleryAsync(int id)
        {
            var imageToDelete = await _context.PlantDetailsImages.FirstOrDefaultAsync(p => p.Id == id);
            _context.PlantDetailsImages.Remove(imageToDelete);
            await _context.SaveChangesAsync();

        }       
        public async Task<int> GetPlantDetailIdAsync(int id)
        {
            var plant = await _context.PlantDetails.FirstOrDefaultAsync(p => p.PlantRef == id);

            return plant.Id;
        }

        public IQueryable<T> GetPlantSeedOrSeedling<T>(int id) where T : BasePlantSeedSeedlingProperty
        {
            return _context.Set<T>().Where(p => p.PlantId == id);
        }             
        public async Task<int> AddContactDetailAsync(ContactDetail contact)
        {
            _context.ContactDetails.Add(contact);
            await _context.SaveChangesAsync();
            return contact.Id;
        }

        public async Task<Plant> GetPlantToActivateAsync(int id)
        {
            var plant = await _context.Plants.FirstOrDefaultAsync(e => e.Id == id);
            return plant;
        }

        public void ActivatePlant(Plant plant)
        {
            _context.Attach(plant);
            _context.Entry(plant).Property(e=>e.isActive).IsModified = true;
            _context.Entry(plant).Property(e=>e.isNew).IsModified = true;
            _context.SaveChanges();
        }
    }
}
