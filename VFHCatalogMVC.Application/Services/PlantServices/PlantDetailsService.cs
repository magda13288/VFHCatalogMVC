using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using VFHCatalogMVC.Application.Interfaces;
using VFHCatalogMVC.Application.Interfaces.PlantInterfaces;
using VFHCatalogMVC.Application.ViewModels.Plant;
using VFHCatalogMVC.Domain.Interface;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Application.Services.PlantServices
{
    public class PlantDetailsService : IPlantDetailsService
    {
        private readonly IPlantRepository _plantRepo;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;
        private readonly UserManager<ApplicationUser> _userManager;        
        public PlantDetailsService()
        {

        }

        public PlantDetailsService(
            IPlantRepository plantRepo, 
            IMapper mapper,
            IImageService imageService,
            UserManager<ApplicationUser> userManager
            )
        {
            _plantRepo = plantRepo;
            _mapper = mapper;
            _imageService = imageService;
            _userManager = userManager;
           
        }
 
        public int AddPlantDetails(NewPlantVm model)
        {
            //Save to table PlantDetails
            SetPlantDetailsModelFields(model.PlantDetails);
            var newPlantDetail = _mapper.Map<PlantDetail>(model.PlantDetails);
            var plantDetailId = _plantRepo.AddPlantDetails(newPlantDetail, model.Id);

            // Add related entities

            AddRealatedEntity(plantDetailId, model.PlantDetails.ListGrowthTypes?.GrowthTypesIds, _plantRepo.AddPlantGrowthTypes);
            AddRealatedEntity(plantDetailId, model.PlantDetails.ListGrowingSeazons?.GrowingSeaznosIds,_plantRepo.AddPlantGrowingSeazons);
            AddRealatedEntity(plantDetailId,model.PlantDetails.ListPlantDestinations?.DestinationsIds,_plantRepo.AddPlantDestinations);

            if (HasElements(model.PlantDetails.Images))
            {
                var fileNames = _imageService.AddPlantGaleryPhotos(model, plantDetailId);
            }


            return plantDetailId;

           /* //Save to table PlantDetails

            if (model.PlantDetails.ColorId == 0)
                model.PlantDetails.ColorId = null;
            if (model.PlantDetails.FruitSizeId == 0)
                model.PlantDetails.FruitSizeId = null;
            if (model.PlantDetails.FruitTypeId == 0)
                model.PlantDetails.FruitTypeId = null;

            var newPlantDetail = _mapper.Map<PlantDetail>(model.PlantDetails);
            var plantDetailId = _plantRepo.AddPlantDetails(newPlantDetail, model.Id);

            Save to PlantGrowthTypes
            if (model.PlantDetails.ListGrowthTypes != null)
            {
                if (model.PlantDetails.ListGrowthTypes.GrowthTypesIds.Length > 0)
                {
                    _plantRepo.AddPlantGrowthTypes(model.PlantDetails.ListGrowthTypes.GrowthTypesIds, plantDetailId);
                }
            }
            //Save to PlantDestinations
            if (model.PlantDetails.ListPlantDestinations != null)
            {
                if (model.PlantDetails.ListPlantDestinations.DestinationsIds.Length > 0)
                {
                    _plantRepo.AddPlantDestinations(model.PlantDetails.ListPlantDestinations.DestinationsIds, plantDetailId);
                }
            }
            //Save to PlantGrowingSeaznos
            if (model.PlantDetails.ListGrowingSeazons != null)
            {
                if (model.PlantDetails.ListGrowingSeazons.GrowingSeaznosIds.Length > 0)
                {
                    _plantRepo.AddPlantGrowingSeazons(model.PlantDetails.ListGrowingSeazons.GrowingSeaznosIds, plantDetailId);
                }
            }

            if (model.PlantDetails.Images != null)
            {
                if (model.PlantDetails.Images.Count > 0)
                {
                    var fileNames = _imageService.AddPlantGaleryPhotos(model, plantDetailId);
                }
            }*/

        }
        public PlantDetailsVm GetPlantDetails(int id)
        {
            var plantDetails = _plantRepo.GetPlantDetails(id);
            var plantDetailsVm = _mapper.Map<PlantDetailsVm>(plantDetails);

            if (plantDetailsVm != null)
            {

                var plant = _plantRepo.GetPlantById(id);
                var plantVm = _mapper.Map<PlantForListVm>(plant);

                plantDetailsVm.ColorName = GetNameOrNull(plantDetailsVm.ColorId, _plantRepo.GetPlantColorName);
                plantDetailsVm.FruitSizeName = GetNameOrNull(plantDetailsVm.FruitSizeId, _plantRepo.GetPlantFriutTypeName);
                plantDetailsVm.FruitTypeName = GetNameOrNull(plantDetailsVm.FruitTypeId, _plantRepo.GetPlantFriutTypeName);
                plantDetailsVm.Plant = plantVm;

                plantDetailsVm.ListGrowthTypes = BuildGrowthTypesVm(plantDetailsVm.Id);
                plantDetailsVm.ListPlantDestinations = BuildDestinationsVm(plantDetailsVm.Id);
                plantDetailsVm.ListGrowingSeazons = BuildGrowingSeaznosVm(plantDetailsVm.Id);
                plantDetailsVm.PlantDetailsImages = BuildGalleryVm(plantDetailsVm.Id);
                plantDetailsVm.PlantOpinions = BuildOpinionsVm(plantDetailsVm.Id);
            }
                return plantDetailsVm;

                /*
                 * if (plantDetailsVm.ColorId != null)
                    plantDetailsVm.ColorName = _plantRepo.GetPlantColorName(plantDetailsVm.ColorId);
                else
                    plantDetailsVm.ColorName = null;

                if (plantDetailsVm.FruitSizeId != null)
                    plantDetailsVm.FruitSizeName = _plantRepo.GetPlantFruitSizeName(plantDetailsVm.FruitSizeId);
                else
                    plantDetailsVm.FruitSizeName = null;

                if (plantDetailsVm.FruitTypeId != null)
                    plantDetailsVm.FruitTypeName = _plantRepo.GetPlantFriutTypeName(plantDetailsVm.FruitTypeId);
                else
                    plantDetailsVm.FruitTypeName = null;

                //GrowthTypesNames

                var propertyNames = new List<string>();
                propertyNames = GetGrowthTypesNames(plantDetailsVm.Id);
                plantDetailsVm.ListGrowthTypes = new ListGrowthTypesVm();

                if (propertyNames != null)
                {

                    plantDetailsVm.ListGrowthTypes.GrowthTypesNames = new List<string>();

                    foreach (var item in propertyNames)
                    {
                        plantDetailsVm.ListGrowthTypes.GrowthTypesNames.Add(item);

                    }

                }
                propertyNames.Clear();

                //DestiantionsNames

                propertyNames = GetDestinationsNames(plantDetailsVm.Id);
                plantDetailsVm.ListPlantDestinations = new ListPlantDestinationsVm();

                if (propertyNames != null)
                {
                    plantDetailsVm.ListPlantDestinations.DestinationsNames = new List<string>();
                    foreach (var item in propertyNames)
                    {
                        plantDetailsVm.ListPlantDestinations.DestinationsNames.Add(item);

                    }
                }

                propertyNames.Clear();

                //GrowingSeazonsNames

                propertyNames = GetGrowingSeaznosNames(plantDetailsVm.Id);
                plantDetailsVm.ListGrowingSeazons = new ListGrowingSeazonsVm();

                if (propertyNames != null)
                {
                    plantDetailsVm.ListGrowingSeazons.GrwoingSeazonsNames = new List<string>();
                    foreach (var item in propertyNames)
                    {
                        plantDetailsVm.ListGrowingSeazons.GrwoingSeazonsNames.Add(item);
                    }
                }

                propertyNames.Clear();

                //PlantGallery
                var plantGallery = new List<PlantDetailsImagesVm>();

                plantGallery = _plantRepo.GetPlantDetailsImages(plantDetails.Id).ProjectTo<PlantDetailsImagesVm>(_mapper.ConfigurationProvider).ToList();

                if (plantGallery != null)
                {
                    if (plantGallery.Count > 0)
                    {
                        foreach (var image in plantGallery)
                        {
                            plantDetailsVm.PlantDetailsImages.Add(image);
                        }
                    }
                }

                var plantOpinions = new List<PlantOpinionsVm>();
                plantOpinions = _plantRepo.GetPlantOpinions(plantDetails.Id).ProjectTo<PlantOpinionsVm>(_mapper.ConfigurationProvider).ToList();

                if (plantOpinions != null)
                {
                    foreach (var item in plantOpinions)
                    {
                        var userInfo = _userManager.FindByIdAsync(item.UserId);
                        item.Date = item.DateAdded.ToShortDateString();
                        item.AccountName = userInfo.Result.AccountName;
                        plantDetailsVm.PlantOpinions.Add(item);
                    }
                }
            }
            return plantDetailsVm;
        }
       
        public List<string> GetDestinationsNames(int id)
        {
            var plantDestinations = new List<PlantDestinationsVm>();
            plantDestinations = _plantRepo.GetPlantDestinations(id).ProjectTo<PlantDestinationsVm>(_mapper.ConfigurationProvider).ToList();
            var destinations = _plantRepo.GetDestinations().ProjectTo<DestinationsVm>(_mapper.ConfigurationProvider).ToList();

            var propertyNames = new List<string>();

            if (plantDestinations != null)
            {
                var destinationsForPlants = new List<DestinationsVm>();
                var propertyIds = new List<int>();

                foreach (var items in plantDestinations)
                {
                    propertyIds.Add(items.DestinationId);
                }

                foreach (var items in propertyIds)
                {
                    foreach (var item in destinations)
                    {
                        if (item.Id == items)
                        {
                            destinationsForPlants.Add(destinations.FirstOrDefault(p => p.Id == items));


                        }
                    }
                }
                foreach (var i in destinationsForPlants)
                {
                    propertyNames.Add(i.Name);
                    propertyNames.Add(", ");
                }

                propertyNames = propertyNames.Take(propertyNames.Count - 1).ToList();
            }

            return propertyNames;
                */
        }

        /* public List<string> GetDestinationsNames(int id)
         {
             var plantDestinations = new List<PlantDestinationsVm>();
             plantDestinations = _plantRepo.GetPlantDestinations(id).ProjectTo<PlantDestinationsVm>(_mapper.ConfigurationProvider).ToList();
             var destinations = _plantRepo.GetDestinations().ProjectTo<DestinationsVm>(_mapper.ConfigurationProvider).ToList();

             var propertyNames = new List<string>();

             if (plantDestinations != null)
             {
                 var destinationsForPlants = new List<DestinationsVm>();
                 var propertyIds = new List<int>();

                 foreach (var items in plantDestinations)
                 {
                     propertyIds.Add(items.DestinationId);
                 }

                 foreach (var items in propertyIds)
                 {
                     foreach (var item in destinations)
                     {
                         if (item.Id == items)
                         {
                             destinationsForPlants.Add(destinations.FirstOrDefault(p => p.Id == items));


                         }
                     }
                 }
                 foreach (var i in destinationsForPlants)
                 {
                     propertyNames.Add(i.Name);
                     propertyNames.Add(", ");
                 }

                 propertyNames = propertyNames.Take(propertyNames.Count - 1).ToList();
             }

             return propertyNames;
         }

         public List<string> GetGrowingSeaznosNames(int id)
         {
             var plantGrowingSeazons = new List<PlantGrowingSeazonsVm>();
             plantGrowingSeazons = _plantRepo.GetPlantGrowingSeazons(id).ProjectTo<PlantGrowingSeazonsVm>(_mapper.ConfigurationProvider).ToList();
             var growingSeazons = _plantRepo.GetGrowingSeazons().ProjectTo<GrowingSeazonVm>(_mapper.ConfigurationProvider).ToList();

             var propertyNames = new List<string>();

             if (plantGrowingSeazons != null)
             {
                 var growingSeaznosForPlant = new List<GrowingSeazonVm>();
                 var propertyIds = new List<int>();

                 foreach (var items in plantGrowingSeazons)
                 {
                     propertyIds.Add(items.GrowingSeazonId);
                 }

                 foreach (var items in propertyIds)
                 {
                     foreach (var item in growingSeazons)
                     {
                         if (item.Id == items)
                         {
                             growingSeaznosForPlant.Add(growingSeazons.FirstOrDefault(p => p.Id == items));

                         }
                     }
                 }
                 foreach (var i in growingSeaznosForPlant)
                 {
                     propertyNames.Add(i.Name);
                     propertyNames.Add(", ");
                 }

                 propertyNames = propertyNames.Take(propertyNames.Count - 1).ToList();
             }

             return propertyNames;
         }

         public List<string> GetGrowthTypesNames(int id)
         {
             var plantGrowthTypes = new List<PlantGrowthTypeVm>();

             plantGrowthTypes = _plantRepo.GetPlantGrowthTypes(id).ProjectTo<PlantGrowthTypeVm>(_mapper.ConfigurationProvider).ToList();
             var growthTypes = _plantRepo.GetGrowthTypes().ProjectTo<GrowthTypeVm>(_mapper.ConfigurationProvider).ToList();
             var propertyNames = new List<string>();

             if (plantGrowthTypes != null)
             {
                 var propertyIds = new List<int>();

                 foreach (var items in plantGrowthTypes)
                 {
                     propertyIds.Add(items.GrowthTypeId);
                 }

                 var growthTypesForPlant = new List<GrowthTypeVm>();

                 foreach (var items in propertyIds)
                 {
                     foreach (var item in growthTypes)
                     {
                         if (item.Id == items)
                         {
                             growthTypesForPlant.Add(growthTypes.FirstOrDefault(p => p.Id == items));

                         }
                     }
                 }

                 foreach (var i in growthTypesForPlant)
                 {
                     propertyNames.Add(i.Name);
                     propertyNames.Add(", ");
                 }
                 propertyNames = propertyNames.Take(propertyNames.Count - 1).ToList();
             }

             return propertyNames;
         }
        */

        public void UpdateEntity<T>(
            int plantDetailId,
            IEnumerable<int> entityIds, 
            Func<int, IEnumerable<T>> getPropertiesAction,
            Action<int[], int> addAction,
            Action<int> deleteAction)
        {
            var existingEntities = getPropertiesAction(plantDetailId);

            if (HasElements(existingEntities) && existingEntities.Any())
            {
                deleteAction(plantDetailId);          
            }
            addAction(entityIds.ToArray(), plantDetailId);
        }
   /*     public void UpdatePlantDestinations(NewPlantVm model)
        {
            var plantDestinations = _plantRepo.GetPlantDestinations(model.PlantDetails.Id);

            if (plantDestinations != null)
            {
                _plantRepo.DeletePlantDestinations(model.PlantDetails.Id);
                _plantRepo.AddPlantDestinations(model.PlantDetails.ListPlantDestinations.DestinationsIds, model.PlantDetails.Id);
            }
            else
            {
                _plantRepo.AddPlantDestinations(model.PlantDetails.ListPlantDestinations.DestinationsIds, model.PlantDetails.Id);
            }
        }

        public void UpdatePlantGrowingSeazons(NewPlantVm model)
        {
            var growingS = _plantRepo.GetPlantGrowingSeazons(model.PlantDetails.Id);

            if (growingS != null)
            {
                _plantRepo.DeletePlantGrowingSeazons(model.PlantDetails.Id);
                _plantRepo.AddPlantGrowingSeazons(model.PlantDetails.ListGrowingSeazons.GrowingSeaznosIds, model.PlantDetails.Id);
            }
            else
            {
                _plantRepo.AddPlantGrowingSeazons(model.PlantDetails.ListGrowingSeazons.GrowingSeaznosIds, model.PlantDetails.Id);
            }
        }

        public void UpdatePlantGrowthTypes(NewPlantVm model)
        {
            var plantGrowthTypes = _plantRepo.GetPlantGrowthTypes(model.PlantDetails.Id);

            if (plantGrowthTypes != null)
            {
                _plantRepo.DeletePlantGrowthTypes(model.PlantDetails.Id);
                _plantRepo.AddPlantGrowthTypes(model.PlantDetails.ListGrowthTypes.GrowthTypesIds, model.PlantDetails.Id);
            }
            else
            {
                _plantRepo.AddPlantGrowthTypes(model.PlantDetails.ListGrowthTypes.GrowthTypesIds, model.PlantDetails.Id);
            }

        }
   */
        public void AddPlantOpinion(PlantOpinionsVm opinion)
        {
            if (opinion != null)
            {
                opinion.DateAdded = DateTime.Now;
                var plantOpinion = _mapper.Map<PlantOpinion>(opinion);
                _plantRepo.AddPlantOpinion(plantOpinion);
            }
            else { throw new NullReferenceException(); }
        }
        public PlantOpinionsVm FillPropertyOpinion(int id, string userName)
        {
            var user = _userManager.FindByNameAsync(userName);
            var plantOpinion = new PlantOpinionsVm() { PlantDetailId = id, UserId = user.Result.Id };
            return plantOpinion;
        }

        /// <summary>
        /// Jeśli details jest null, to każda próba dostępu do jego właściwości lub metod spowodowałaby wyjątek typu NullReferenceException. Taki zapis pozwala na   bezpieczne zakończenie działania metody w takim przypadku.
        /// Operator trójargumentowy
        /// 
        /// </summary>
        /// <param name="details"></param>
        private void SetPlantDetailsModelFields(PlantDetailsVm details)
        {
            if (details == null) return;
            details.ColorId = details.ColorId == 0 ? null : details.ColorId;
            details.FruitSizeId = details.FruitSizeId == 0 ? null : details.FruitSizeId;
            details.FruitTypeId = details.FruitTypeId == 0 ? null : details.FruitTypeId;
        }

        /// <summary>
        ///  Action - Delegat, który wskazuje funkcję wykonującą faktyczne dodanie tych encji do bazy danych lub innego źródła. Funkcja ta przyjmuje dwa parametry:Tablicę   identyfikatorów(int[]) – encje, które mają być dodane. Identyfikator głównego obiektu (int) – do którego te encje mają być przypisane.
        /// </summary>
        /// <param name="plantDetailId"></param>
        /// <param name="entityIds"></param>
        /// <param name="addAction"></param>
        private void AddRealatedEntity(int plantDetailId, IEnumerable<int> entityIds, Action<int[], int> addAction)
        {
            if (HasElements(entityIds))
            {
                addAction(entityIds.ToArray(), plantDetailId);
            }
        }
        private bool HasElements<T>(IEnumerable<T> list) => list != null && list.Any();
        private ListGrowthTypesVm BuildGrowthTypesVm(int id) => new ListGrowthTypesVm { GrowthTypesNames = GetPropertyNames(_plantRepo.GetPlantGrowthTypes, _plantRepo.GetGrowthTypes, x => x.GrowthTypeId, x => x.Id, x => x.Name, id) };
        private ListPlantDestinationsVm BuildDestinationsVm(int id) => new ListPlantDestinationsVm { DestinationsNames = GetPropertyNames(_plantRepo.GetPlantDestinations, _plantRepo.GetDestinations, x => x.DestinationId, x => x.Id, x => x.Name, id) };
        private ListGrowingSeazonsVm BuildGrowingSeaznosVm(int id) => new ListGrowingSeazonsVm { GrwoingSeazonsNames = GetPropertyNames(_plantRepo.GetPlantGrowingSeazons, _plantRepo.GetGrowingSeazons, x => x.GrowingSeazonId, x => x.Id, x => x.Name, id) };

        private List<PlantDetailsImagesVm> BuildGalleryVm(int plantDetailsId) =>
           _plantRepo.GetPlantDetailsImages(plantDetailsId).ProjectTo<PlantDetailsImagesVm>(_mapper.ConfigurationProvider).ToList();

        private List<PlantOpinionsVm> BuildOpinionsVm(int plantDetailsId)
        {
            var plantOpinions = _plantRepo.GetPlantOpinions(plantDetailsId)
                .ProjectTo<PlantOpinionsVm>(_mapper.ConfigurationProvider)
                .ToList();

            if (plantOpinions != null)
            {
                
                foreach (var item in plantOpinions)
                {
                    var userInfo = _userManager.FindByIdAsync(item.UserId);
                    item.Date = item.DateAdded.ToShortDateString();
                    item.AccountName = userInfo.Result.AccountName;
                }
            }

            return plantOpinions;
        }

        private List<string> GetPropertyNames<TVm, TEntity>(
            Func<int, IEnumerable<TVm>> getPlantProperties,
            Func<IEnumerable<TEntity>> getAllProperties,
            Func<TVm, int> getPropertyId,
            Func<TEntity, int> getEntityId,
            Func<TEntity, string> getEntityName,
            int id)
        {
            var plantProperties = getPlantProperties(id)?.ToList();
            var allProperties = getAllProperties()?.ToList();

            if (plantProperties == null || allProperties == null)
                return new List<string>();

           var propertyIds = plantProperties.Select(getPropertyId).ToHashSet();

           var AllPlantPropertiesNames = allProperties
                                        .Where(p => propertyIds.Contains(getEntityId(p)))
                                        .Select(getEntityName)
                                        .ToList();

            var plantPropertiesNames = new List<string>();

            foreach (var item in AllPlantPropertiesNames)
            { 
                plantPropertiesNames.Add(item);
                plantPropertiesNames.Add(",");
            }

           return plantPropertiesNames = plantPropertiesNames.Take(plantPropertiesNames.Count - 1).ToList();
        }
        private string GetNameOrNull(int? id, Func<int?, string> fetchNameFunc) => id.HasValue ? fetchNameFunc(id) : null;
    }
}
