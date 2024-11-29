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



namespace VFHCatalogMVC.Application.Services.PlantServices
{
    public class PlantService : IPlantService
    {

        private readonly IPlantRepository _plantRepo;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserContactDataService _userContactDataService;
        private readonly IUserPlantService _userPlantService;
        private readonly IImageService _imageService;
        private readonly IPlantDetailsService _plantDetailsSerrvice;
        private readonly IPlantItemProcessor<PlantSeedVm> _seedProcessor;
        private readonly IPlantItemProcessor<PlantSeedlingVm> _seedlingProcessor;

        public PlantService()
        {

        }
        public PlantService(
            IPlantRepository plantRepo,
            IMapper mapper, UserManager<ApplicationUser> userManager,
            IUserContactDataService userContactDataService,
            IImageService imageService,
            IPlantDetailsService plantDetailsSerrvice,
            IUserPlantService userPlantService,
            IPlantItemProcessor<PlantSeedVm> seedProcessor ,
            IPlantItemProcessor<PlantSeedlingVm> seedlingProcessor)
        {
            _plantRepo = plantRepo;
            _mapper = mapper;
            _userManager = userManager;
            _imageService = imageService;
            _plantDetailsSerrvice = plantDetailsSerrvice;
            _userContactDataService = userContactDataService;
            _userPlantService = userPlantService;
            _seedProcessor = seedProcessor;
            _seedlingProcessor = seedlingProcessor;
               ;
        }

        public int AddPlant(NewPlantVm model, string user)
        {
            int id = 0;

            model.SectionId = model.SectionId == 0 ? null : model.SectionId;

            //check if adding plant does't exist in database

            if (DoesPlantExist(model.FullName))
                return 0;

            //Save to table Plant
            var newPlant = _mapper.Map<Plant>(model);

            if (model.Photo != null)
            {
                string fileName = _imageService.AddPlantSearchPhoto(model);
                newPlant.Photo = fileName;
            }

            var userInfo = _userManager.FindByNameAsync(user);
            var isAdmin = _userManager.IsInRoleAsync(userInfo.Result, "Admin").Result;

            SetPropertiesAndAddNewUserPlant(newPlant, isAdmin);
            id = _plantRepo.AddPlant(newPlant);

            if (id > 0)
            {
                model.Id = id;
                var plantDetailId = _plantDetailsSerrvice.AddPlantDetails(model);

                if (!isAdmin)
                {
                    _userPlantService.AddNewUserPlant(id, userInfo.Result.Id);
                }

            }

            return id;

        }
        private bool DoesPlantExist(string fullName) => GetAllActivePlantsForList(1, 10, fullName, null, null, null).Count > 0;
        private void SetPropertiesAndAddNewUserPlant(Plant plant, bool isAdmin)
        {
            plant.isActive = isAdmin;
            plant.isNew = !isAdmin;

        }
        public ListPlantForListVm GetAllActivePlantsForList(int pageSize, int? pageNo, string searchString, int? typeId, int? groupId, int? sectionId)
        {

            var plants = ActivePlantsFilters(searchString, typeId, groupId, sectionId);

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

        private List<PlantForListVm> ActivePlantsFilters(string searchString, int? typeId, int? groupId, int? sectionId)
        {
            var plants = new List<PlantForListVm>();

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                plants = _plantRepo.GetAllActivePlants().Where(p => p.FullName.StartsWith(searchString))
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
                            plants = _plantRepo.GetAllActivePlants().Where(p => p.PlantTypeId == typeId && p.PlantGroupId == groupId && p.PlantSectionId == sectionId)
                               .ProjectTo<PlantForListVm>(_mapper.ConfigurationProvider).ToList();
                        }
                        else
                        {
                            plants = _plantRepo.GetAllActivePlants().Where(p => p.PlantTypeId == typeId && p.PlantGroupId == groupId)
                               .ProjectTo<PlantForListVm>(_mapper.ConfigurationProvider).ToList();

                        }
                    }
                    else
                    {
                        plants = _plantRepo.GetAllActivePlants().Where(p => p.PlantTypeId == typeId).ProjectTo<PlantForListVm>(_mapper.ConfigurationProvider).ToList();
                    }
                }
            }
            return plants;

        }

        public PlantSeedsForListVm GetAllPlantSeeds(int id, int countryId, int regionId, int cityId, int pageSize, int? pageNo, bool isCompany, string userName)
        {
            var seeds = _plantRepo.GetPlantSeeds(id).ProjectTo<PlantSeedVm>(_mapper.ConfigurationProvider).ToList();
            var detailId = _plantRepo.GetPlantDetailId(id);
            var processedSeeds = _seedProcessor.ProcessItems(seeds, detailId, countryId, regionId, cityId, isCompany);
            var paginateList = Paginate(processedSeeds, pageSize, pageNo);

            return CreatePlantListVm<PlantSeedVm, PlantSeedsForListVm>(id, paginateList, seeds.Count, pageSize, pageNo, isCompany, userName);
        }

        public PlantSeedlingsForListVm GetAllPlantSeedlings(int id, int countryId, int regionId, int cityId, int pageSize, int? pageNo, bool isCompany)
        {
            var seedlings = _plantRepo.GetPlantSeedlings(id).ProjectTo<PlantSeedlingVm>(_mapper.ConfigurationProvider).ToList();
            var detailId = _plantRepo.GetPlantDetailId(id);
            var processedSeedlings = _seedlingProcessor.ProcessItems(seedlings, detailId, countryId, regionId, cityId, isCompany);
            var paginateList = Paginate(processedSeedlings, pageSize, pageNo);

            return CreatePlantListVm<PlantSeedlingVm, PlantSeedlingsForListVm>(id, paginateList, seedlings.Count, pageSize, pageNo, isCompany, null);
        }
        private TListVm CreatePlantListVm<TVm, TListVm>(
            int id, List<TVm> items, int totalCount, int pageSize, int? pageNo, bool isCompany, string userName)
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
        public NewPlantVm GetPlantToEdit(int id)
        {

            var plant = _plantRepo.GetPlantById(id);
            var plantVm = _mapper.Map<NewPlantVm>(plant);

            var plantDetails = _plantRepo.GetPlantDetails(id);
            var plantDetailsVm = _mapper.Map<PlantDetailsVm>(plantDetails);
            plantVm.PlantDetails = plantDetailsVm;

            if (plantDetails != null)
            {
                plantVm.PlantDetails.PlantDetailsImages = _plantRepo.GetPlantDetailsImages(plantDetailsVm.Id).ProjectTo<PlantDetailsImagesVm>(_mapper.ConfigurationProvider).ToList();

                SetPlantGrowthType(plantDetailsVm, plantDetailsVm.Id);
                SetPlantGrowingSeazons(plantDetailsVm, plantDetailsVm.Id);
                SetPlantDestinations(plantDetailsVm, plantDetailsVm.Id);
            }

            return plantVm;
        }

        private bool SetPlantGrowthType(PlantDetailsVm plant, int plantDetailId)
        {
            var growthTypes = _plantRepo.GetPlantDetailsById<PlantGrowthType>(plantDetailId).ProjectTo<PlantGrowthTypeVm>(_mapper.ConfigurationProvider).ToList();
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

        private bool SetPlantGrowingSeazons(PlantDetailsVm plant, int plantDetailId)
        {
            var growingSeazons = _plantRepo.GetPlantDetailsById<PlantGrowingSeazon>(plantDetailId).ProjectTo<PlantGrowingSeazonsVm>(_mapper.ConfigurationProvider).ToList();
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
        private bool SetPlantDestinations(PlantDetailsVm plant, int plantDetailId)
        {
            var destinations = _plantRepo.GetPlantDetailsById<PlantDestination>(plantDetailId).ProjectTo<PlantDestinationsVm>(_mapper.ConfigurationProvider).ToList();
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
        private void UpdatePlantPhoto(NewPlantVm model)
        {
            if (model.Photo != null)
            {
                var existingPlant = _plantRepo.GetPlantById(model.Id);
                string direction = "plantGallery/searchPhoto";
                string newPhoto = _imageService.UploadImage(model.Photo, model.FullName, direction);
                _imageService.DeleteImage($"plantGallery/searchPhoto/{existingPlant.Photo}");
                model.PhotoFileName = newPhoto;
            }
            else
            {
                model.PhotoFileName = _plantRepo.GetPlantById(model.Id).Photo;
            }
        }
        private void UpdatePlantDetailsImages(NewPlantVm model)
        {
            string direction = "plantGallery/plantDetailsGallery";

            if (model.PlantDetails.Images != null)
            {
                foreach (var image in model.PlantDetails.Images)
                {
                    string fileName = _imageService.UploadImage(image, model.FullName, direction);
                    _plantRepo.AddPlantDetailsImages(fileName, model.PlantDetails.Id);
                }
            }

            if (model.PlantDetails.PlantDetailsImages != null)
            {
                foreach (var image in model.PlantDetails.PlantDetailsImages.Where(i => i.IsChecked))
                {
                    string imagePath = direction + "/" + image.ImageURL;
                    _imageService.DeleteImage(imagePath);
                    _plantRepo.DeleteImageFromGallery(image.Id);
                }
            }
        }
        public void UpdatePlant(NewPlantVm model)
        {
            UpdatePlantPhoto(model);
  
            UpdatePlantDetailsImages(model);

            var plant = _mapper.Map<Plant>(model);

            var plantDetails = _mapper.Map<PlantDetail>(model.PlantDetails);
            _plantRepo.UpdatePlant(plant);
            _plantRepo.UpdatePlantDetails(plantDetails);

            //Update Destinations
            if (model.PlantDetails.ListPlantDestinations != null)
                _plantDetailsSerrvice.UpdateEntity(
                                        model.PlantDetails.Id,
                                        model.PlantDetails.ListPlantDestinations.DestinationsIds,
                                        id => _plantRepo.GetPlantDetailsById<PlantDestination>(model.PlantDetails.Id),
                                        (ids, plantId) => _plantRepo.AddPlantDestinations(model.PlantDetails.ListPlantDestinations.DestinationsIds, model.PlantDetails.Id),
                                        id => _plantRepo.DeletePlantDetailEntity<Destination>(model.PlantDetails.Id)
                                                );
            else SetPlantDestinations(model.PlantDetails, model.PlantDetails.Id);

            //Update GrowingSeazons
            if (model.PlantDetails.ListGrowingSeazons != null)
                _plantDetailsSerrvice.UpdateEntity(
                                       model.PlantDetails.Id,
                                       model.PlantDetails.ListGrowingSeazons.GrowingSeaznosIds,
                                       id => _plantRepo.GetPlantDetailsById<PlantGrowingSeazon>(model.PlantDetails.Id),
                                       (ids, plantId) => _plantRepo.AddPlantGrowingSeazons(model.PlantDetails.ListGrowingSeazons.GrowingSeaznosIds, model.PlantDetails.Id),
                                       id => _plantRepo.DeletePlantDetailEntity<GrowingSeazon>(model.PlantDetails.Id)
                                               );
            else SetPlantGrowingSeazons(model.PlantDetails, model.PlantDetails.Id);

            //UpdateGrowthTypes
            if (model.PlantDetails.ListGrowthTypes != null)
                _plantDetailsSerrvice.UpdateEntity(
                                       model.PlantDetails.Id,
                                       model.PlantDetails.ListGrowthTypes.GrowthTypesIds,
                                       id => _plantRepo.GetPlantDetailsById<PlantGrowthType>(model.PlantDetails.Id),
                                       (ids, plantId) => _plantRepo.AddPlantGrowthTypes(model.PlantDetails.ListGrowthTypes.GrowthTypesIds, model.PlantDetails.Id),
                                       id => _plantRepo.DeletePlantDetailEntity<GrowthType>(model.PlantDetails.Id)
                                               );
            else SetPlantGrowthType(model.PlantDetails,model.PlantDetails.Id); 
            //_plantDetailsSerrvice.UpdatePlantGrowthTypes(model);

        }
        public PlantForListVm DeletePlant(int id)
        {
            var getPlantToDelete = _plantRepo.GetPlantById(id);
            var plantVm = _mapper.Map<PlantForListVm>(getPlantToDelete);
            plantVm.isActive = false;
            //var plantVm = _mapper.Map<PlantForListVm>(getPlantToDelete);

            if (getPlantToDelete != null)
            {
                getPlantToDelete.isActive = false;
                //plantVm.isActive = false;
                //var plantToDelete = _mapper.Map<Plant>(plantVm);
                _plantRepo.DeletePlant(getPlantToDelete);
            }

            return plantVm;
        }

        public PlantSeedVm FillProperties(int id, string userName)
        {
            var user = _userManager.FindByNameAsync(userName);
            var plantSedd = new PlantSeedVm() { PlantId = id, UserId = user.Result.Id };
            return plantSedd;
        }

        public void AddPlantSeed(PlantSeedVm seed)
        {

            if (seed != null)
            {
                seed.DateAdded = DateTime.Now;
                var plantSeed = _mapper.Map<PlantSeed>(seed);
                var seedId = _plantRepo.AddPlantSeed(plantSeed);

                //if (seed.ContactDetail != null)
                if (seed.Link != null)
                {
                    seed.ContactDetail.ContactDetailTypeID = 1;
                    seed.ContactDetail.UserId = seed.UserId;
                    seed.ContactDetail.ContactDetailInformation = seed.Link;
                    var contactDetails = _mapper.Map<ContactDetail>(seed.ContactDetail);
                    var contactId = _plantRepo.AddContactDetail(contactDetails);

                    ContactDetailForSeedVm contactSeed = new ContactDetailForSeedVm();
                    contactSeed.PlantSeedId = seedId;
                    contactSeed.ContactDetailId = contactId;

                    var contactSeddToSave = _mapper.Map<ContactDetailForSeed>(contactSeed);

                    _plantRepo.AddContactDetailsEntity<ContactDetailForSeed>(contactSeddToSave);
                }
            }
            else
            {
                throw new NullReferenceException();
            }
        }

        public void AddPlantSeedling(PlantSeedlingVm seedling)
        {
            if (seedling != null)
            {
                seedling.DateAdded = DateTime.Now;
                var plantSeedling = _mapper.Map<PlantSeedling>(seedling);
                var seedlingId = _plantRepo.AddPlantSeedling(plantSeedling);

                if (seedling.ContactDetail != null)
                {
                    seedling.ContactDetail.ContactDetailTypeID = 1;
                    seedling.ContactDetail.UserId = seedling.UserId;
                    var contactDetails = _mapper.Map<ContactDetail>(seedling.ContactDetail);
                    var contactId = _plantRepo.AddContactDetail(contactDetails);

                    ContactDetailForSeedlingVm contactSeedling = new ContactDetailForSeedlingVm();
                    contactSeedling.PlantSeedlingId = seedlingId;
                    contactSeedling.ContactDetailId = contactId;

                    var contactSeedlingToSave = _mapper.Map<ContactDetailForSeedling>(contactSeedling);
                    _plantRepo.AddContactDetailsEntity<ContactDetailForSeedling>(contactSeedlingToSave);

                }

            }
            else
            {
                throw new NullReferenceException();
            }
        }

        public PlantSeedlingVm FillPropertiesSeedling(int id, string userName)
        {
            var user = _userManager.FindByNameAsync(userName);
            var plantSeddling = new PlantSeedlingVm() { PlantId = id, UserId = user.Result.Id };
            return plantSeddling;
        }      
       
        public void ActivatePlant(int id)
        {
            var plant = _plantRepo.GetPlantToActivate(id);
            var plantVm = _mapper.Map<NewPlantVm>(plant);

            plantVm.isActive = true;
            plantVm.isNew = false;

            var plantToSave = _mapper.Map<Plant>(plantVm);

            _plantRepo.ActivatePlant(plantToSave);
        }
    }
}
