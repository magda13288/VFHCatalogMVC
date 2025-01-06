using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
 
        public async Task<int> AddPlantDetailsAsync(NewPlantVm model)
        {
            //Save to table PlantDetails
            await SetPlantDetailsModelFieldsAsync(model.PlantDetails);
            var newPlantDetail = _mapper.Map<PlantDetail>(model.PlantDetails);
            var plantDetailId =  await _plantRepo.AddPlantDetailsAsync(newPlantDetail, model.Id);

            // Add related entities
            Task<int> destinationTask = Task.FromResult(0);
            Task<int> growingTask = Task.FromResult(0);
            Task<int> growthTask = Task.FromResult(0);

            growthTask = AddRealatedEntityAsync(plantDetailId, model.PlantDetails.ListGrowthTypes?.GrowthTypesIds, _plantRepo.AddPlantGrowthTypesAsync);

            growingTask = AddRealatedEntityAsync(plantDetailId, model.PlantDetails.ListGrowingSeazons?.GrowingSeaznosIds,_plantRepo.AddPlantGrowingSeazonsAsync);

            destinationTask = AddRealatedEntityAsync(plantDetailId,model.PlantDetails.ListPlantDestinations?.DestinationsIds,_plantRepo.AddPlantDestinationsAsync);

            if (HasElements(model.PlantDetails.Images))
            {
                var fileNamesTask = await _imageService.AddPlantGaleryPhotosAsync(model, plantDetailId);
            }

            await Task.WhenAll(growthTask,growingTask, destinationTask);

            return plantDetailId;

        }
        public async Task<PlantDetailsVm> GetPlantDetailsAsync(int id)
        {
            var plantDetails = await _plantRepo.GetPlantDetailsAsync(id);
            var plantDetailsVm = _mapper.Map<PlantDetailsVm>(plantDetails);

            if (plantDetailsVm != null)
            {
                Task<string> colorTask = Task.FromResult(string.Empty);
                Task<string> fruitSizeTask = Task.FromResult(string.Empty);
                Task<string> fruitTypeTask = Task.FromResult(string.Empty);

                var plant = await _plantRepo.GetPlantByIdAsync(id);
                var plantVm = _mapper.Map<PlantForListVm>(plant);

                colorTask = GetNameOrNullAsync(plantDetailsVm.ColorId, _plantRepo.GetPlantDetailsPropertyNameAsync<Color>);
                fruitSizeTask = GetNameOrNullAsync(plantDetailsVm.FruitSizeId, _plantRepo.GetPlantDetailsPropertyNameAsync<FruitSize>);
                fruitTypeTask = GetNameOrNullAsync(plantDetailsVm.FruitTypeId, _plantRepo.GetPlantDetailsPropertyNameAsync<FruitType>);
               
                plantDetailsVm.ListGrowthTypes = BuildGrowthTypesVm(plantDetailsVm.Id);
                plantDetailsVm.ListPlantDestinations = BuildDestinationsVm(plantDetailsVm.Id);
                plantDetailsVm.ListGrowingSeazons = BuildGrowingSeaznosVm(plantDetailsVm.Id);
                plantDetailsVm.PlantDetailsImages = BuildGalleryVm(plantDetailsVm.Id);
                plantDetailsVm.PlantOpinions = BuildOpinionsVm(plantDetailsVm.Id);

                await Task.WhenAll(colorTask, fruitSizeTask, fruitTypeTask);

                plantDetailsVm.ColorName = colorTask.Result;
                plantDetailsVm.FruitSizeName = fruitSizeTask.Result;
                plantDetailsVm.FruitTypeName = fruitTypeTask.Result;
                plantDetailsVm.Plant = plantVm;
            }
                return plantDetailsVm;

        }
        public async Task<int> UpdateEntityAsync<T>(
            int plantDetailId,
            IEnumerable<int> entityIds, 
            Func<int, IEnumerable<T>> getPropertiesAction,
            Func<int[], int,Task<int>> addAction,
            Func<int, Task<int>> deleteAction)
        {
            var existingEntities = getPropertiesAction(plantDetailId);

            if (HasElements(existingEntities) && existingEntities.Any())
            {
                await deleteAction(plantDetailId);          
            }
           return await addAction(entityIds.ToArray(), plantDetailId);
        }
        public async Task AddPlantOpinionAsync(PlantOpinionsVm opinion)
        {
            if (opinion != null)
            {
                opinion.DateAdded = DateTime.Now;
                var plantOpinion = _mapper.Map<PlantOpinion>(opinion);
                await _plantRepo.AddEntityAsync<PlantOpinion>(plantOpinion);
            }
            else { throw new NullReferenceException(); }
        }
        public async Task<PlantOpinionsVm> FillPropertyOpinionAsync(int id, string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var plantOpinion = new PlantOpinionsVm() { PlantDetailId = id, UserId = user.Id };
            return plantOpinion;
        }

        /// <summary>
        /// Jeśli details jest null, to każda próba dostępu do jego właściwości lub metod spowodowałaby wyjątek typu NullReferenceException. Taki zapis pozwala na   bezpieczne zakończenie działania metody w takim przypadku.
        /// Operator trójargumentowy
        /// 
        /// </summary>
        /// <param name="details"></param>
        private async Task SetPlantDetailsModelFieldsAsync(PlantDetailsVm details)
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
        private async Task<int> AddRealatedEntityAsync(int plantDetailId, IEnumerable<int> entityIds, Func<int[], int, Task<int>> addAction)
        {

            if (HasElements(entityIds))
            {
               return await addAction(entityIds.ToArray(), plantDetailId);
            }

            return 0;
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

            //var plantPropertiesTask = Task.Run(() => getPlantProperties(id)?.ToList());
            //var allPropertiesTask = Task.Run(() => getAllProperties()?.ToList());

            //// Pobieranie danych asynchronicznie
            //var plantProperties = await plantPropertiesTask;
            //var allProperties = await allPropertiesTask;

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
        private async Task<string> GetNameOrNullAsync(int? id, Func<int?, Task<string>> fetchNameFunc) => id.HasValue ? await fetchNameFunc(id) : null;
    }
}
