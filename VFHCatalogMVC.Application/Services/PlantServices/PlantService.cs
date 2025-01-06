using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using VFHCatalogMVC.Application.Interfaces;
using VFHCatalogMVC.Domain.Model;
using VFHCatalogMVC.Application.ViewModels.Plant;
using VFHCatalogMVC.Domain.Interface;
using Microsoft.AspNetCore.Identity;
using System.Data;
using VFHCatalogMVC.Application.ViewModels.User;
using VFHCatalogMVC.Application.Interfaces.PlantInterfaces;
using VFHCatalogMVC.Application.Interfaces.UserInterfaces;
using VFHCatalogMVC.Application.ViewModels.Plant.PlantSeeds;
using VFHCatalogMVC.Application.ViewModels.Plant.PlantSeedlings;
using VFHCatalogMVC.Application.ViewModels.Plant.PlantDetails;
using VFHCatalogMVC.Application.ViewModels.Plant.Common;
using System.Threading.Tasks;
using VFHCatalogMVC.Application.Constants;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using NPOI.OpenXmlFormats.Dml;




namespace VFHCatalogMVC.Application.Services.PlantServices
{
    public class PlantService : IPlantService
    {

        private readonly IPlantRepository _plantRepo;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserPlantService _userPlantService;
        private readonly IImageService _imageService;
        private readonly IPlantDetailsService _plantDetailsService;
        private readonly IPlantItemProcessor<PlantSeedVm> _seedProcessor;
        private readonly IPlantItemProcessor<PlantSeedlingVm> _seedlingProcessor;

        public PlantService()
        {

        }
        public PlantService(
            IPlantRepository plantRepo,
            IMapper mapper, UserManager<ApplicationUser> userManager,
            IImageService imageService,
            IPlantDetailsService plantDetailsService,
            IUserPlantService userPlantService,
            IPlantItemProcessor<PlantSeedVm> seedProcessor ,
            IPlantItemProcessor<PlantSeedlingVm> seedlingProcessor)
        {
            _plantRepo = plantRepo;
            _mapper = mapper;
            _userManager = userManager;
            _imageService = imageService;
            _plantDetailsService = plantDetailsService;
            _userPlantService = userPlantService;
            _seedProcessor = seedProcessor;
            _seedlingProcessor = seedlingProcessor;
               
        }

        public async Task<int> AddPlantAsync(NewPlantVm model, string user)
        {
            int id = 0;

            model.SectionId = model.SectionId == 0 ? null : model.SectionId;

            //check if adding plant does't exist in database

            if (await DoesPlantExist(model.FullName))
                return 0;

            //Save to table Plant
            var newPlant = _mapper.Map<Plant>(model);

            if (model.Photo != null)
            {
                string fileName = await _imageService.AddPlantSearchPhotoAsync(model);
                newPlant.Photo = fileName;
            }

            var userInfo = _userManager.FindByNameAsync(user);
            var isAdmin = _userManager.IsInRoleAsync(userInfo.Result, UserRoles.ADMIN);

            await Task.WhenAll(userInfo, isAdmin);

            SetPropertiesAndAddNewUserPlant(newPlant, isAdmin.Result);

            id = await _plantRepo.AddEntityAsync<Plant>(newPlant);

            if (id > 0)
            {
                model.Id = id;

                if (!isAdmin.Result)
                {
                   await _userPlantService.AddNewUserPlantAsync(id, userInfo.Result.Id);
                }

                var plantDetailId = await _plantDetailsService.AddPlantDetailsAsync(model);
               
            }

            return id;

        }
        private async Task<bool> DoesPlantExist(string fullName) => (await GetAllActivePlantsForListAsync(1, 10, fullName, null, null, null)).Count > 0;
        private void SetPropertiesAndAddNewUserPlant(Plant plant, bool isAdmin)
        {
            plant.isActive = isAdmin;
            plant.isNew = !isAdmin;

        }
        public async Task<ListPlantForListVm> GetAllActivePlantsForListAsync(
            int pageSize, 
            int? pageNo, 
            string searchString, 
            int? typeId,
            int? groupId,
            int? sectionId)
        {

            var plants = await ActivePlantsFiltersAsync(searchString, typeId, groupId, sectionId);

            var plantsToShow = Paginate(plants, pageSize, pageNo);

            var plantsList = new ListPlantForListVm()
            {
                PageSize = pageSize,
                CurrentPage = pageNo,
                SearchString = searchString,
                Plants = plantsToShow,
                Count = plants.Count,
            };

            return plantsList;

        }
        private List<T> Paginate<T>(IEnumerable<T> items, int pageSize, int? pageNo)
        {
            if (!pageNo.HasValue || pageNo <= 0)
            {
                pageNo = 1; // default first page
            }

            return items.Skip(pageSize * (pageNo.Value - 1)).Take(pageSize).ToList();
        }

        private async Task<List<PlantForListVm>> ActivePlantsFiltersAsync(string searchString, int? typeId, int? groupId, int? sectionId)
        {
            
            var plantTask = await _plantRepo.GetAllActivePlantsAsync();
            var plants = new List<PlantForListVm>();

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                plants = plantTask.Where(p => p.FullName.StartsWith(searchString))
                       .ProjectTo<PlantForListVm>(_mapper.ConfigurationProvider).ToList();
            }
            else
            {
                if (typeId > 0 && typeId != null)
                {
                    if (groupId > 0 && groupId != null)
                    {
                        //projectTo wykorzystywane przy kolekcjach IQueryable
                        if (sectionId > 0 && sectionId != null)
                        {
                            plants = plantTask.Where(p => p.PlantTypeId == typeId && p.PlantGroupId == groupId && p.PlantSectionId == sectionId)
                               .ProjectTo<PlantForListVm>(_mapper.ConfigurationProvider).ToList();
                        }
                        else
                        {
                            plants = plantTask.Where(p => p.PlantTypeId == typeId && p.PlantGroupId == groupId)
                               .ProjectTo<PlantForListVm>(_mapper.ConfigurationProvider).ToList();

                        }
                    }
                    else
                    {
                        plants = plantTask.Where(p => p.PlantTypeId == typeId).ProjectTo<PlantForListVm>(_mapper.ConfigurationProvider).ToList();
                    }
                }
            }
            return plants;

        }

        public async Task<PlantSeedsForListVm> GetAllPlantSeedsAsync(
            int id,
            int countryId,
            int regionId,
            int cityId,
            int pageSize, 
            int? pageNo, 
            bool isCompany, 
            string userName)
        {
            var seeds = _plantRepo.GetPlantSeedOrSeedling<PlantSeed>(id).ProjectTo<PlantSeedVm>(_mapper.ConfigurationProvider).ToList();
            var detailId = await _plantRepo.GetPlantDetailIdAsync(id);
            var processedSeeds = await _seedProcessor.ProcessItemsAsync(seeds, detailId, countryId, regionId, cityId, isCompany);
            var paginateList = Paginate(processedSeeds, pageSize, pageNo);

            return CreatePlantListVm<PlantSeedVm, PlantSeedsForListVm>(id, paginateList, seeds.Count, pageSize, pageNo, isCompany, userName);
        }

        public async Task<PlantSeedlingsForListVm> GetAllPlantSeedlingsAsync(
            int id,
            int countryId,
            int regionId, 
            int cityId, 
            int pageSize,
            int? pageNo,
            bool isCompany)
        {
            var seedlings = _plantRepo.GetPlantSeedOrSeedling<PlantSeedling>(id).ProjectTo<PlantSeedlingVm>(_mapper.ConfigurationProvider).ToList();
            var detailId = await _plantRepo.GetPlantDetailIdAsync(id);
            var processedSeedlings = await _seedlingProcessor.ProcessItemsAsync(seedlings, detailId, countryId, regionId, cityId, isCompany);
            var paginateList = Paginate(processedSeedlings, pageSize, pageNo);

            return CreatePlantListVm<PlantSeedlingVm, PlantSeedlingsForListVm>(id, paginateList, seedlings.Count, pageSize, pageNo, isCompany, null);
        }
        private TListVm CreatePlantListVm<TVm, TListVm>(
            int id, List<TVm> items, 
            int totalCount, 
            int pageSize,
            int? pageNo,
            bool isCompany,
            string userName)
            where TListVm : VFHCatalogMVC.Application.ViewModels.Plant.PlantListVm<TVm>, new()
        {
            return new TListVm
            {
                PlantId = id,
                PageSize = pageSize,
                CurrentPage = pageNo,
                Items = items,
                Count = totalCount,
                isCompany = isCompany,
                LoggedUserName = userName
            };
        }
        public async Task<NewPlantVm> GetPlantToEditAsync(int id)
        {

            var plant = await _plantRepo.GetPlantByIdAsync(id);
            var plantVm = _mapper.Map<NewPlantVm>(plant);

            var plantDetails = await _plantRepo.GetPlantDetailsAsync(id);
            var plantDetailsVm = _mapper.Map<PlantDetailsVm>(plantDetails);
            plantVm.PlantDetails = plantDetailsVm;

            if (plantDetails != null)
            {
                plantVm.PlantDetails.PlantDetailsImages = _plantRepo.GetPlantDetailsImages(plantDetailsVm.Id).ProjectTo<PlantDetailsImagesVm>(_mapper.ConfigurationProvider).ToList();

               var growthTypeTask = SetPlantGrowthTypeAsync(plantDetailsVm, plantDetailsVm.Id);
               var growingSeazonTask =  SetPlantGrowingSeazonsAsync(plantDetailsVm, plantDetailsVm.Id);
               var destinationTask = SetPlantDestinationsAsync(plantDetailsVm, plantDetailsVm.Id);

               await Task.WhenAll(growthTypeTask,growingSeazonTask,destinationTask);
            }

            return plantVm;
        }

        private async Task<bool> SetPlantGrowthTypeAsync(PlantDetailsVm plant, int plantDetailId)
        {
            var growthTypes = await _plantRepo.GetPlantDetailsById<PlantGrowthType>(plantDetailId).ProjectTo<PlantGrowthTypeVm>(_mapper.ConfigurationProvider).ToListAsync();

            if (growthTypes != null)
            {
                plant.ListGrowthTypes = new ListGrowthTypesVm();
                plant.ListGrowthTypes.GrowthTypesIds = new int[growthTypes.Count];
                for (int i = 0; i < growthTypes.Count; i++)
                {
                    plant.ListGrowthTypes.GrowthTypesIds[i] = growthTypes[i].GrowthTypeId;
                }
            }

            var set = plant.ListGrowthTypes.GrowthTypesIds.Any() ? true: false;

            return set;
        }

        private async Task<bool> SetPlantGrowingSeazonsAsync(PlantDetailsVm plant, int plantDetailId)
        {
            var growingSeazons = await _plantRepo.GetPlantDetailsById<PlantGrowingSeazon>(plantDetailId).ProjectTo<PlantGrowingSeazonsVm>(_mapper.ConfigurationProvider).ToListAsync();

            if (growingSeazons != null)
            {
                plant.ListGrowingSeazons = new ListGrowingSeazonsVm();
                plant.ListGrowingSeazons.GrowingSeaznosIds = new int[growingSeazons.Count];
                for (int i = 0; i < growingSeazons.Count; i++)
                {
                    plant.ListGrowingSeazons.GrowingSeaznosIds[i] = growingSeazons[i].GrowingSeazonId;
                }
            }

            var set = plant.ListGrowingSeazons.GrowingSeaznosIds.Any() ? true : false;

            return set;
        }
        private async Task<bool> SetPlantDestinationsAsync(PlantDetailsVm plant, int plantDetailId)
        {
            var destinations = await _plantRepo.GetPlantDetailsById<PlantDestination>(plantDetailId).ProjectTo<PlantDestinationsVm>(_mapper.ConfigurationProvider).ToListAsync();

            if (destinations != null)
            {
                plant.ListPlantDestinations = new ListPlantDestinationsVm();
                plant.ListPlantDestinations.DestinationsIds = new int[destinations.Count];
                for (int i = 0; i < destinations.Count; i++)
                {
                    plant.ListPlantDestinations.DestinationsIds[i] = destinations[i].DestinationId;
                }
            }

            var set = plant.ListPlantDestinations.DestinationsIds.Any() ? true : false;

            return set;

        }
        private async Task UpdatePlantPhotoAsync(NewPlantVm model)
        {
            if (model.Photo != null)
            {
                var existingPlant = await _plantRepo.GetPlantByIdAsync(model.Id);
                string direction = "plantGallery/searchPhoto";
                string newPhoto = await _imageService.UploadImageAsync(model.Photo, model.FullName, direction);
                await _imageService.DeleteImageAsync($"plantGallery/searchPhoto/{existingPlant.Photo}");
                model.PhotoFileName = newPhoto;
            }
            else
            {
                var task = await _plantRepo.GetPlantByIdAsync(model.Id);
                model.PhotoFileName = task.Photo;
            }
        }
        private async Task UpdatePlantDetailsImagesAsync(NewPlantVm model)
        {
            string direction = "plantGallery/plantDetailsGallery";

            if (model.PlantDetails.Images != null)
            {
                foreach (var image in model.PlantDetails.Images)
                {
                    string fileName = await _imageService.UploadImageAsync(image, model.FullName, direction);
                    await _plantRepo.AddPlantDetailsImagesAsync(fileName, model.PlantDetails.Id);
                }
            }

            if (model.PlantDetails.PlantDetailsImages != null)
            {
                foreach (var image in model.PlantDetails.PlantDetailsImages.Where(i => i.IsChecked))
                {
                    string imagePath = direction + "/" + image.ImageURL;
                    await _imageService.DeleteImageAsync(imagePath);
                    await _plantRepo.DeleteImageFromGalleryAsync(image.Id);
                }
            }

        }
        public async Task UpdatePlantAsync(NewPlantVm model)
        {
            await UpdatePlantPhotoAsync(model);
  
            await UpdatePlantDetailsImagesAsync(model);

            var plant = _mapper.Map<Plant>(model);

            var plantDetails = _mapper.Map<PlantDetail>(model.PlantDetails);
            var plantTask = _plantRepo.UpdatePlantAsync(plant);
            var plantDetailsTask = _plantRepo.UpdatePlantDetailsAsync(plantDetails);

            await Task.WhenAll(plantTask, plantDetailsTask);

            Task<int> destinationTask = Task.FromResult(0);
            Task<int> growingTask = Task.FromResult(0);
            Task<int> growthTask = Task.FromResult(0);

            Task<bool> destinationTaskBool = Task.FromResult(true);
            Task<bool> growingTaskBool = Task.FromResult(true);
            Task<bool> growthTaskBool = Task.FromResult(true);


            //Update Destinations
            if (model.PlantDetails.ListPlantDestinations != null)
            {
                destinationTask = _plantDetailsService.UpdateEntityAsync(
                                         model.PlantDetails.Id,
                                         model.PlantDetails.ListPlantDestinations.DestinationsIds,
                                         id => _plantRepo.GetPlantDetailsById<PlantDestination>(model.PlantDetails.Id),
                                         (ids, plantId) => _plantRepo.AddPlantDestinationsAsync(model.PlantDetails.ListPlantDestinations.DestinationsIds, model.PlantDetails.Id),
                                         id => _plantRepo.DeletePlantDetailEntityAsync<PlantDestination>(model.PlantDetails.Id)
                                               );
            }
            else destinationTaskBool = SetPlantDestinationsAsync(model.PlantDetails, model.PlantDetails.Id);

            //Update GrowingSeazons
            if (model.PlantDetails.ListGrowingSeazons != null)
                growingTask = _plantDetailsService.UpdateEntityAsync(
                                       model.PlantDetails.Id,
                                       model.PlantDetails.ListGrowingSeazons.GrowingSeaznosIds,
                                       id => _plantRepo.GetPlantDetailsById<PlantGrowingSeazon>(model.PlantDetails.Id),
                                       (ids, plantId) => _plantRepo.AddPlantGrowingSeazonsAsync(model.PlantDetails.ListGrowingSeazons.GrowingSeaznosIds, model.PlantDetails.Id),
                                       id => _plantRepo.DeletePlantDetailEntityAsync<PlantGrowingSeazon>(model.PlantDetails.Id)
                                               );
            else growingTaskBool = SetPlantGrowingSeazonsAsync(model.PlantDetails, model.PlantDetails.Id);

            //UpdateGrowthTypes
            if (model.PlantDetails.ListGrowthTypes != null)
                growthTask =  _plantDetailsService.UpdateEntityAsync(
                                       model.PlantDetails.Id,
                                       model.PlantDetails.ListGrowthTypes.GrowthTypesIds,
                                       id => _plantRepo.GetPlantDetailsById<PlantGrowthType>(model.PlantDetails.Id),
                                       (ids, plantId) => _plantRepo.AddPlantGrowthTypesAsync(model.PlantDetails.ListGrowthTypes.GrowthTypesIds, model.PlantDetails.Id),
                                       id => _plantRepo.DeletePlantDetailEntityAsync<PlantGrowthType>(model.PlantDetails.Id)
                                               );
            else growthTaskBool = SetPlantGrowthTypeAsync(model.PlantDetails,model.PlantDetails.Id);
            //_plantDetailsSerrvice.UpdatePlantGrowthTypes(model);

            try
            {
                await Task.WhenAll(destinationTask, growthTask, growingTask);
                await Task.WhenAll(destinationTaskBool, growthTaskBool, growingTaskBool);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating the plant: {ex.Message}");
                throw; // Optionally, propagate the exception further.
            }          
        }
        public async Task<PlantForListVm> DeletePlantAsync(int id)
        {
            var getPlantToDelete = await _plantRepo.GetPlantByIdAsync(id);
            var plantVm = _mapper.Map<PlantForListVm>(getPlantToDelete);
            plantVm.isActive = false;
            //var plantVm = _mapper.Map<PlantForListVm>(getPlantToDelete);

            if (getPlantToDelete != null)
            {
                getPlantToDelete.isActive = false;
                //plantVm.isActive = false;
                //var plantToDelete = _mapper.Map<Plant>(plantVm);
                await _plantRepo.DeletePlantAsync(getPlantToDelete);
            }

            return plantVm;
        }

        public async Task AddPlantSeedAsync(PlantSeedVm seed)
        {

           var plantEntityId = await AddPlantEntityAsync<PlantSeedVm,PlantSeed>(seed, _plantRepo.AddEntityAsync<PlantSeed>);

            var contactId = await AddEntityContactDetailsAsync(
                plantEntityId,
                seed,
                (plantEntityId, contactDetailId) => new ContactDetailForSeed
                {
                    PlantSeedId = plantEntityId,
                    ContactDetailId = contactDetailId
                },
                contactEntity => _plantRepo.AddContactDetailsEntityAsync(contactEntity)
                );
                      
        }
        public async Task AddPlantSeedlingAsync(PlantSeedlingVm seedling)
        {
            var plantEntityId = await AddPlantEntityAsync<PlantSeedlingVm, PlantSeedling>(seedling, _plantRepo.AddEntityAsync<PlantSeedling>);

            var contactId = await AddEntityContactDetailsAsync(
                plantEntityId,
                seedling,
                (plantEntityId, contactDetailId) => new ContactDetailForSeedling
                {
                    PlantSeedlingId = plantEntityId,
                    ContactDetailId = contactDetailId
                },
                contactEntity => _plantRepo.AddContactDetailsEntityAsync(contactEntity)
                );
        }

        private async Task<int> AddPlantEntityAsync<TVm, TSource>(TVm entity, Func<TSource, Task<int>> addEntity)
           where TVm : PlantItemVm
           where TSource : class
        {
            if (entity != null)
            {
                entity.DateAdded = DateTime.Now;
                var plantEntity = _mapper.Map<TSource>(entity);
                var plantEntityId = await addEntity(plantEntity);
                return plantEntityId;

            }
            else
            {
                throw new NullReferenceException();
            }

        }

        private async Task<int> AddEntityContactDetailsAsync<TVm, TContactEntity>(
             int entityId,
             TVm entity,
             Func<int, int, TContactEntity> createContactEntity,
             Func<TContactEntity, Task<int>> saveContactEntity)
            where TVm : PlantItemVm
            where TContactEntity : class
        {

            if (entity.ContactDetail != null)
            {
                entity.ContactDetail.ContactDetailTypeID = 1;
                entity.ContactDetail.UserId = entity.UserId;
                entity.ContactDetail.ContactDetailInformation = entity.Link;

                var contactDetail = _mapper.Map<ContactDetail>(entity.ContactDetail);
                var contactDetailId = await _plantRepo.AddContactDetailAsync(contactDetail);

                var contactEntity = createContactEntity(entityId, contactDetailId);
                var id = await saveContactEntity(contactEntity);
                return id;
            }
            else
            {
                throw new NullReferenceException();
            }

        }
        public async Task<T> FillPropertyAsync<T>(
            int id, 
            string userName
            )
               where T : PlantItemVm, new()
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
                throw new Exception($"User with name {userName} not found.");
            var plantSeddling = new T() { PlantId = id, UserId = user.Id };
            return plantSeddling;
        }
      
        public async Task ActivatePlantAsync(int id)
        {
            var plant = await _plantRepo.GetPlantToActivateAsync(id);
            var plantVm = _mapper.Map<NewPlantVm>(plant);

            plantVm.isActive = true;
            plantVm.isNew = false;

            var plantToSave = _mapper.Map<Plant>(plantVm);

            _plantRepo.ActivatePlant(plantToSave);
        }
    }
}
