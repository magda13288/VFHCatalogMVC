using System;
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

        public void DeletePlant(int plantId)
        {
            var plantToDelete = _context.Plants.Find(plantId);
            if (plantToDelete != null)
            {
                _context.Plants.Remove(plantToDelete);
                _context.SaveChanges();
            }
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
        //IQueryable tworzy jedynie zapytanie do bazy danych ale nie pobiera tych danych z bazy. W serwisie bedzie metoda do pobierania danych/ wyorzystuje się to ponieważ mozna dane jeszcze przefiltrowac, co robi sie pozniej
        //public IQueryable<Plant> GetPlantByTypeId(int typeId)
        //{
        //    var plants = _context.Plants.Where(p=>p.PlantTypeId == typeId);
        //    return plants;

        //}

        //zwraca juz wynik zpytania, poniewaz nie da się już bardziej przefiltrwac danych/ zwracamy jedne wynik
        public Plant GetPlantById(int plantId)
        {
            var plant = _context.Plants.FirstOrDefault(p => p.Id == plantId);
            return plant;
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

      
    }
}
