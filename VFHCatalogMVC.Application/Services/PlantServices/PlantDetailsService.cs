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
using VFHCatalogMVC.Application.ViewModels.Plant.PlantDetails;
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

        }
        public PlantDetailsVm GetPlantDetails(int id)
        {
            var plantDetails = _plantRepo.GetPlantDetails(id);
            var plantDetailsVm = _mapper.Map<PlantDetailsVm>(plantDetails);

            if (plantDetailsVm != null)
            {

                var plant = _plantRepo.GetPlantById(id);
                var plantVm = _mapper.Map<PlantForListVm>(plant);

                plantDetailsVm.ColorName = GetNameOrNull(plantDetailsVm.ColorId, _plantRepo.GetPlantDetailsPropertyName<Color>);
                plantDetailsVm.FruitSizeName = GetNameOrNull(plantDetailsVm.FruitSizeId, _plantRepo.GetPlantDetailsPropertyName<FruitSize>);
                plantDetailsVm.FruitTypeName = GetNameOrNull(plantDetailsVm.FruitTypeId, _plantRepo.GetPlantDetailsPropertyName<FruitType>);
                plantDetailsVm.Plant = plantVm;

                plantDetailsVm.ListGrowthTypes = BuildGrowthTypesVm(plantDetailsVm.Id);
                plantDetailsVm.ListPlantDestinations = BuildDestinationsVm(plantDetailsVm.Id);
                plantDetailsVm.ListGrowingSeazons = BuildGrowingSeaznosVm(plantDetailsVm.Id);
                plantDetailsVm.PlantDetailsImages = BuildGalleryVm(plantDetailsVm.Id);
                plantDetailsVm.PlantOpinions = BuildOpinionsVm(plantDetailsVm.Id);
            }
                return plantDetailsVm;

        }

        /// <summary>
        /// Updates the related entities for a given plantDetailId.
        /// First, it removes existing entities if they exist, then adds new entities from the provided entityIds.
        /// </summary>
        /// <typeparam name="T">The type of entities being updated.</typeparam>
        /// <param name="plantDetailId">The identifier of the plant detail for which entities are being updated.</param>
        /// <param name="entityIds">A list of identifiers for the new entities to be assigned.</param>
        /// <param name="getPropertiesAction">A function that retrieves the existing entities associated with the plantDetailId.</param>
        /// <param name="addAction">An action that adds new entities, taking an array of entity IDs and the plantDetailId.</param>
        /// <param name="deleteAction">An action that deletes the existing entities associated with the plantDetailId.</param>

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
        public void AddPlantOpinion(PlantOpinionsVm opinion)
        {
            if (opinion != null)
            {
                opinion.DateAdded = DateTime.Now;
                var plantOpinion = _mapper.Map<PlantOpinion>(opinion);
                _plantRepo.AddEntity<PlantOpinion>(plantOpinion);
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
        /// Action - A delegate that points to a function performing the actual addition of these entities to the database or another data source.  
        /// This function takes two parameters:  
        /// - An array of identifiers (int[]) – the entities to be added.  
        /// - The identifier of the main object (int) – to which these entities should be assigned.  
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

        /// <summary>
        /// Builds a view model containing names of related entities for a given plant detail ID.
        /// Each method retrieves related entity names using the `GetPropertyNames` helper method.
        /// </summary>
        /// <param name="id">The identifier of the plant detail for which related entity names are retrieved.</param>
        /// <returns>A view model containing the names of the associated entities.</returns>
        private ListGrowthTypesVm BuildGrowthTypesVm(int id) => new ListGrowthTypesVm { GrowthTypesNames = GetPropertyNames(_plantRepo.GetPlantDetailsById<PlantGrowthType>, _plantRepo.GetAllEntities<GrowthType>, x => x.GrowthTypeId, x => x.Id, x => x.Name, id) };
        private ListPlantDestinationsVm BuildDestinationsVm(int id) => new ListPlantDestinationsVm { DestinationsNames = GetPropertyNames(_plantRepo.GetPlantDetailsById<PlantDestination>, _plantRepo.GetAllEntities<Destination>, x => x.DestinationId, x => x.Id, x => x.Name, id) };
        private ListGrowingSeazonsVm BuildGrowingSeaznosVm(int id) => new ListGrowingSeazonsVm { GrwoingSeazonsNames = GetPropertyNames(_plantRepo.GetPlantDetailsById<PlantGrowingSeazon>, _plantRepo.GetAllEntities<GrowingSeazon>, x => x.GrowingSeazonId, x => x.Id, x => x.Name, id) };

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

        /// <summary>
        /// Retrieves a list of property names associated with a specific plant detail ID.  
        /// It maps plant properties to their corresponding names from a list of all available entities.
        /// </summary>
        /// <typeparam name="TVm">The type representing the plant property details.</typeparam>
        /// <typeparam name="TEntity">The type representing the full list of available entities.</typeparam>
        /// <param name="getPlantProperties">A function that retrieves plant-specific properties based on the provided ID.</param>
        /// <param name="getAllProperties">A function that retrieves all available properties.</param>
        /// <param name="getPropertyId">A function that extracts the property ID from the plant property.</param>
        /// <param name="getEntityId">A function that extracts the entity ID from the full list of entities.</param>
        /// <param name="getEntityName">A function that extracts the name of the entity.</param>
        /// <param name="id">The identifier of the plant detail for which property names should be retrieved.</param>
        /// <returns>
        /// A list of property names, separated by commas, corresponding to the plant's associated properties.
        /// Returns an empty list if no matching properties are found.
        /// </returns>
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
