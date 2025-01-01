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
