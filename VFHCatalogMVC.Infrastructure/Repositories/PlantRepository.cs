using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public void DeletePlant(Plant plant)
        {
            
           _context.Attach(plant);
           _context.Entry(plant).Property(e=>e.isActive).IsModified = true;
           _context.SaveChanges();
        }

        public int AddEntity<T>(T entity) where T : BaseEntity
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();

            return entity.Id;
        }    
        public int AddPlantDetails(PlantDetail plantDetail, int plantId)
        {
            plantDetail.PlantRef = plantId;
            _context.PlantDetails.Add(plantDetail);
            _context.SaveChanges();

            return plantDetail.Id;

        }
        public void AddEntities<T>(int[] entityIds, int plantDetailId, Func<int, int, T> createEntity) where T : class
        {
            var entities = entityIds.Select(id => createEntity(id, plantDetailId)).ToList(); //createEntity - funkcja, która tworzy instancję encji na podstawie przekazanych danych
            _context.Set<T>().AddRange(entities); //AddRange dodaje zbiorczo wszystkie encje zamiast dodwać je w pętli
            _context.SaveChanges();
        }
        public void AddPlantGrowthTypes(int[] growthTypesIds, int plantDetailId)
        {
            AddEntities<PlantGrowthType>(
                growthTypesIds,
                plantDetailId,
                (id, detailId) => new PlantGrowthType { GrowthTypeId = id, PlantDetailId = detailId });
        }

        public void AddPlantDestinations(int[] plantDestinationsIds, int plantDetailId)
        {
            AddEntities<PlantDestination>(
                plantDestinationsIds,
                plantDetailId,
                (id, detailId) => new PlantDestination { DestinationId = id, PlantDetailId = detailId });
        }

        public void AddPlantGrowingSeazons(int[] growingSeazonsIds, int plantDetailId)
        {
            AddEntities<PlantGrowingSeazon>(
                growingSeazonsIds,
                plantDetailId,
                (id, detailId) => new PlantGrowingSeazon { GrowingSeazonId = id, PlantDetailId = detailId });
        }
        public void AddPlantDetailsImages(string fileName, int plantDetailId)
        {

            _context.PlantDetailsImages.Add(new PlantDetailsImages { PlantDetailId = plantDetailId, ImageURL = fileName });
           _context.SaveChanges();
        }
        public PlantDetail GetPlantDetails(int id)
        {
            var plantDetails = _context.PlantDetails.FirstOrDefault(p=>p.PlantRef == id);
            return plantDetails;
        }

        //IQueryable tworzy jedynie zapytanie do bazy danych ale nie pobiera tych danych z bazy. W serwisie bedzie metoda do pobierania danych/ wyorzystuje się to ponieważ mozna dane jeszcze przefiltrowac, co robi sie pozniej
        //public IQueryable<Plant> GetPlantByTypeId(int typeId)
        //{
        //    var plants = _context.Plants.Where(p=>p.PlantTypeId == typeId);
        //    return plants;

        //}

        //zwraca juz wynik zpytania, poniewaz nie da się już bardziej przefiltrwac danych/ zwracamy jedne wynik
        public Plant GetPlantById(int plantId)
        {
            var plant = _context.Plants.AsNoTracking().FirstOrDefault(p => p.Id == plantId);
            return plant;
        }
        public string GetPlantDetailsPropertyName<T>(int? id) where T: BasePlantEntityNameProperty
        {
            var entity = _context.Set<T>().FirstOrDefault(p =>p.Id== id);
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
        public IQueryable<Plant> GetAllActivePlants()
        {
            var plants = _context.Plants.Where(p => p.isActive == true).OrderBy(p => p.Id);

            return plants;
        }
        public IQueryable<PlantDetailsImages> GetPlantDetailsImages(int plantDetailId)
        {
            var plantDetailsImages = _context.PlantDetailsImages.Where(p => p.PlantDetailId == plantDetailId);
            return plantDetailsImages;
        }

        public void UpdatePlant(Plant plant)
        {
            _context.Attach(plant);
            _context.Entry(plant).Property(e=>e.FullName).IsModified = true;
            _context.Entry(plant).Property(e=>e.Photo).IsModified = true;
            _context.SaveChanges();          
        }

        public void UpdatePlantDetails(PlantDetail plant)
        {
            _context.Attach(plant);
            _context.Entry(plant).Property(e=>e.ColorId).IsModified = true;
            _context.Entry(plant).Property(e=>e.FruitSizeId).IsModified = true;
            _context.Entry(plant).Property(e=>e.FruitSizeId).IsModified = true;
            _context.Entry(plant).Property(e=>e.Description).IsModified = true;
            _context.Entry(plant).Property(e=>e.PlantPassportNumber).IsModified = true;
            _context.Entry(plant).Property(e=>e.PlantRef).IsModified = false;
            _context.SaveChanges();
            
        }

        public void DeletePlantDetailEntity<T>(int id) where T : BasePlantDetailKeyProperty
        {
            var entity = _context.Set<T>().Where(p => p.PlantDetailId == id); 
            _context.Set<T>().RemoveRange(entity);
            _context.SaveChanges();
        }
        public void DeleteImageFromGallery(int id)
        {
            var imageToDelete = _context.PlantDetailsImages.FirstOrDefault(p => p.Id == id);
            _context.PlantDetailsImages.Remove(imageToDelete);
            _context.SaveChanges();

        }       
        public int GetPlantDetailId(int id)
        {
            var plant = _context.PlantDetails.FirstOrDefault(p => p.PlantRef == id);

            return plant.Id;
        }

        public IQueryable<T> GetPlantSeedOrSeedling<T>(int id) where T : BasePlantSeedSeedlingProperty
        {
            return _context.Set<T>().Where(p => p.PlantId == id);
        }      
        public int AddContactDetailsEntity<T>(T entity) where T : class
        {
            _context.Set<T>().Add(entity);
            return _context.SaveChanges();
        }
        public int AddContactDetail(ContactDetail contact)
        {
            _context.ContactDetails.Add(contact);
            _context.SaveChanges();
            return contact.Id;
        }

        public Plant GetPlantToActivate(int id)
        {
            var plant = _context.Plants.FirstOrDefault(e => e.Id == id);
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
