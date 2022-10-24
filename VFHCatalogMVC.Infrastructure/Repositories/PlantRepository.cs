using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
            //var plantToDelete = _context.Plants.Find(id;
            //if (plantToDelete != null)
            //{
            //    _context.Plants.Remove(plantToDelete);
            //    _context.SaveChanges();
            //}
           _context.Attach(plant);
           _context.Entry(plant).Property("isActive").IsModified = true;
           _context.SaveChanges();
        }

        public int AddPlant(Plant plant)
        {
            _context.Plants.Add(plant);
            _context.SaveChanges();

            return plant.Id;
        }
        public int AddPlantDetails(PlantDetail plantDetail, int plantId)
        {
            plantDetail.PlantRef = plantId;
            _context.PlantDetails.Add(plantDetail);
            _context.SaveChanges();

            return plantDetail.Id;

        }
        public void AddPlantGrowthTypes(int[] growthTypesIds, int plantDetailId)
        {
            PlantGrowthType plantGrowthType = new PlantGrowthType();

            for (int i = 0; i < growthTypesIds.Length; i++)
            {
                _context.PlantGrowthTypes.Add(new PlantGrowthType { GrowthTypeId = growthTypesIds[i], PlantDetailId = plantDetailId });
                _context.SaveChanges();
            }
        }

        public void AddPlantDestinations(int[] plantDestinationsIds, int plantDestId)
        {
            PlantDestination plantDestination = new PlantDestination();

            for (int i = 0; i < plantDestinationsIds.Length; i++)
            {
                _context.PlantDestinations.Add(new PlantDestination { DestinationId = plantDestinationsIds[i], PlantDetailId = plantDestId });
                _context.SaveChanges();
            }
        }
        public void AddPlantGrowingSeazons(int[] growingSeazonsIds, int plantDestId)
        {
            PlantDestination plantDestination = new PlantDestination();

            for (int i = 0; i < growingSeazonsIds.Length; i++)
            {
                _context.PlantGrowingSeazons.Add(new PlantGrowingSeazon { GrowingSeazonId = growingSeazonsIds[i], PlantDetailId = plantDestId });
                _context.SaveChanges();
            }
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

        public string GetPlantColorName(int? id)
        {
            var color = _context.Colors.FirstOrDefault(p => p.Id == id);
            return color.Name;
        }

        public string GetPlantFruitSizeName(int? id)
        {
           var fruitSize = _context.FruitSizes.FirstOrDefault(p => p.Id == id);
            return fruitSize.Name;
        }

        public string GetPlantFriutTypeName(int? id)
        {
           var fruitType = _context.FruitTypes.FirstOrDefault(p => p.Id == id);
            return fruitType.Name;  
        }

        public IQueryable<PlantGrowthType> GetPlantGrowthTypes(int id)
        {
            var growthTypes = _context.PlantGrowthTypes.Where(p => p.PlantDetailId == id);
            return growthTypes;
        }
        public IQueryable<PlantDestination> GetPlantDestinations(int id)
        {
            var destinations = _context.PlantDestinations.Where(p => p.PlantDetailId == id);
            return destinations;
        }

        public IQueryable<PlantGrowingSeazon> GetPlantGrowingSeazons(int id)
        {
            var growingSeazons = _context.PlantGrowingSeazons.Where(p => p.PlantDetailId == id);
            return growingSeazons;
        }

        public IQueryable<PlantType> GetAllTypes()
        {
            var types = _context.PlantTypes;
            return types;
        }

        public IQueryable<PlantGroup> GetAllGroups()
        {
            var groups = _context.PlantGroups;
            return groups;
        }

        public IQueryable<PlantSection> GetAllSections()
        {
            var sections = _context.PlantSections;
            return sections;
        }

        public int EditPlant(Plant plant)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Plant> GetAllActivePlants()
        {
            var plants = _context.Plants.Where(p => p.isActive == true).OrderBy(p => p.Id);

            return plants;
        }

        public IQueryable<GrowthType> GetGrowthTypes()
        {
            var growthTypes = _context.GrowthTypes;

            return growthTypes;
        }

        public IQueryable<Destination> GetDestinations()
        {
            var destinations = _context.Destinations;
            return destinations;
        }

        public IQueryable<Color> GetColors()
        {
            var colors = _context.Colors;
            return colors;
        }

        public IQueryable<GrowingSeazon> GetGrowingSeazons()
        {
            var growingSeazons = _context.GrowingSeazons;
            return growingSeazons;
        }

        public IQueryable<FruitSize> GetFruitSizes()
        {
           var fruitSizes = _context.FruitSizes;
            return fruitSizes;
        }

        public IQueryable<FruitType> GetFruitTypes()
        {
            var fruitTypes = _context.FruitTypes;
            return fruitTypes;
        }

        public IQueryable<PlantDetailsImages> GetPlantDetailsImages(int plantDetailId)
        {
            var plantDetailsImages = _context.PlantDetailsImages.Where(p => p.PlantDetailId == plantDetailId);
            return plantDetailsImages;
        }

        public void UpdatePlant(Plant plant)
        {
            //_context.Entry(plant).State = EntityState.Detached;
            //_context.Set<Plant>().Update(plant);
            _context.Attach(plant);
            _context.Entry(plant).Property("FullName").IsModified = true;
            _context.Entry(plant).Property("Photo").IsModified = true;
            _context.SaveChanges();          
        }

        public void UpdatePlantDetails(PlantDetail plant)
        {
            _context.Attach(plant);
            _context.Entry(plant).Property("ColorId").IsModified = true;
            _context.Entry(plant).Property("FruitSizeId").IsModified = true;
            _context.Entry(plant).Property("FruitTypeId").IsModified = true;
            _context.Entry(plant).Property("Description").IsModified = true;
            _context.Entry(plant).Property("PlantPassportNumber").IsModified = true;
            _context.Entry(plant).Property("PlantRef").IsModified = false;
            _context.SaveChanges();
            
        }
        public void DeletePlantDestinations(int id)
        {
            var destinations = _context.PlantDestinations.Where(p => p.PlantDetailId == id);
            _context.PlantDestinations.RemoveRange(destinations);
            _context.SaveChanges();
        }
        public void DeletePlantGrowingSeazons(int id)
        {
            var growingSeazons = _context.PlantGrowingSeazons.Where(p => p.PlantDetailId == id);
            _context.PlantGrowingSeazons.RemoveRange(growingSeazons);
            _context.SaveChanges();
        }

        public void DeletePlantGrowthTypes(int id)
        {
            var growthTypes = _context.PlantGrowthTypes.Where(p => p.PlantDetailId == id);
            _context.PlantGrowthTypes.RemoveRange(growthTypes);
            _context.SaveChanges();
        }

        public void DeleteImageFromGallery(int id)
        {
            var imageToDelete = _context.PlantDetailsImages.FirstOrDefault(p => p.Id == id);
            _context.PlantDetailsImages.Remove(imageToDelete);
            _context.SaveChanges();

        }
        public IQueryable<PlantOpinion> GetPlantOpinions(int id)
        {
            var plantOpinions = _context.PlantOpinions.Where(p => p.PlantDetailId == id);
            return plantOpinions;
        }
    }
}
