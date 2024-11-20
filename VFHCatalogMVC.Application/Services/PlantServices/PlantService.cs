using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using VFHCatalogMVC.Application.Interfaces;
using VFHCatalogMVC.Domain.Model;
using VFHCatalogMVC.Application.ViewModels.Plant;
using VFHCatalogMVC.Domain.Interface;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Features;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.Options;
using System.Numerics;
using System.Drawing;
using VFHCatalogMVC.Application.ViewModels.User;
using VFHCatalogMVC.Application.Interfaces.PlantInterfaces;
using VFHCatalogMVC.Application.Interfaces.UserInterfaces;

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

        public PlantService()
        {

        }
        public PlantService(IPlantRepository plantRepo, IMapper mapper, UserManager<ApplicationUser> userManager, IUserContactDataService userContactDataService, IImageService imageService, IPlantDetailsService plantDetailsSerrvice, IUserPlantService userPlantService)
        {
            _plantRepo = plantRepo;
            _mapper = mapper;
            _userManager = userManager;
            _imageService = imageService;
            _plantDetailsSerrvice = plantDetailsSerrvice;
            _userContactDataService = userContactDataService;
            _userPlantService = userPlantService;
        }

        public int AddPlant(NewPlantVm model, string user)
        {
            int id = 0;

            if (model.SectionId == 0)
                model.SectionId = null;

            //check if adding plant does't exist in database

            var plantList = GetAllActivePlantsForList(1, 10, model.FullName, null, null, null);

            if (plantList.Count == 0)
            {
                //Save to table Plant
                var newPlant = _mapper.Map<Plant>(model);

                if (model.Photo != null)
                {
                    string fileName = _imageService.AddPlantSearchPhoto(model);
                    newPlant.Photo = fileName;
                }

                var userInfo = _userManager.FindByNameAsync(user);
                //_userManager.Dispose();
                var userRole = _userManager.IsInRoleAsync(userInfo.Result, "Admin");

                if (userRole.Result is true)
                {
                    newPlant.isActive = true;
                    newPlant.isNew = false;
                    id = _plantRepo.AddPlant(newPlant);
                    model.Id = id;
                }
                else
                {
                    newPlant.isActive = false;
                    newPlant.isNew = true;

                    id = _plantRepo.AddPlant(newPlant);
                    model.Id = id;

                    _userPlantService.AddNewUserPlant(id, userInfo.Result.Id);
                }

                if (id != 0)
                {
                    var plantDetailId = _plantDetailsSerrvice.AddPlantDetails(model);
                }
                return id;
            }
            else
            {
                return 0;
            }

        }
        public ListPlantForListVm GetAllActivePlantsForList(int pageSize, int? pageNo, string searchString, int? typeId, int? groupId, int? sectionId)
        {
            var plants = new List<PlantForListVm>();
            var plantsToShow = new List<PlantForListVm>();

            if (searchString == "")
            {
                if (typeId != 0 && typeId != null)
                {
                    if (groupId != 0 && groupId != null)
                    {
                        //projectTo wykorzystywane przy kolekcjach IQueryable
                        if (sectionId != 0 && sectionId != null)
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
            else
            {
                plants = _plantRepo.GetAllActivePlants().Where(p => p.FullName.StartsWith(searchString))
                       .ProjectTo<PlantForListVm>(_mapper.ConfigurationProvider).ToList();
            }

            plantsToShow = plants.Skip(pageSize * (pageNo.Value - 1)).Take(pageSize).ToList();

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
        public PlantSeedsForListVm GetAllPlantSeeds(int id, int countryId, int regionId, int cityId, int pageSize, int? pageNo, bool isCompany, string userName)
        {
            var seeds = new List<PlantSeedVm>();
            var seedsToShow = new List<PlantSeedVm>();
            var opinions = new List<PlantOpinionsVm>();
            var filteredUsersList = new List<string>();
            var filteredSeeds = new List<PlantSeedVm>();
            var seedsList = new List<PlantSeedVm>();

            var detailId = _plantRepo.GetPlantDetailId(id);
            seeds = _plantRepo.GetPlantSeeds(id).ProjectTo<PlantSeedVm>(_mapper.ConfigurationProvider).ToList();

            if (id != 0 && countryId == 0 && regionId == 0 && cityId == 0)
            {
                foreach (var item in seeds)
                {
                    var user = _userManager.FindByIdAsync(item.UserId);
                    item.PlantOpinions = new List<PlantOpinionsVm>();

                    if (isCompany is true)
                    {
                        var role = _userManager.IsInRoleAsync(user.Result, "Company");
                        if (role.Result == true)
                        {
                            item.AccountName = user.Result.CompanyName;
                            item.Date = item.DateAdded.ToShortDateString();
                            var contactId = _userPlantService.GetContactDetailForSeed(item.Id);

                            if (contactId != null)
                            {
                                var contactDetails = _userPlantService.GetContactDetail(contactId);
                                var contactDetailsVm = _mapper.Map<ContactDetailVm>(contactDetails);
                                item.ContactDetail = new ContactDetailVm();
                                item.ContactDetail = contactDetailsVm;
                            }
                            else
                            {
                                item.ContactDetail = new ContactDetailVm();
                                item.ContactDetail.ContactDetailInformation = "";
                            }



                            opinions = _plantRepo.GetPlantOpinions(detailId).Where(p => p.UserId == user.Result.Id).ProjectTo<PlantOpinionsVm>(_mapper.ConfigurationProvider).ToList();
                            item.PlantOpinions = opinions;

                            filteredUsersList.Add(user.Result.Id);

                            seedsList = FilterSeedsList(seeds, filteredUsersList);

                            seedsToShow = seedsList.Skip(pageSize * ((int)pageNo - 1)).Take(pageSize).ToList();

                        }
                    }
                    else
                    {
                        var role = _userManager.IsInRoleAsync(user.Result, "PRIVATE_USER");
                        if (role.Result == true)
                        {
                            item.AccountName = _userContactDataService.UserAccountName(user);
                            item.Date = item.DateAdded.ToShortDateString();

                            opinions = _plantRepo.GetPlantOpinions(detailId).Where(p => p.UserId == user.Result.Id).ProjectTo<PlantOpinionsVm>(_mapper.ConfigurationProvider).ToList();
                            item.PlantOpinions = opinions;

                            filteredUsersList.Add(user.Result.Id);
                            seedsList = FilterSeedsList(seeds, filteredUsersList);

                            seedsToShow = seedsList.Skip(pageSize * ((int)pageNo - 1)).Take(pageSize).ToList();
                        }
                    }
                }

            }
            else
            {
                filteredUsersList = _userPlantService.FilterUsers(countryId, regionId, cityId, seeds, null);

                seedsList = FilterSeedsList(seeds, filteredUsersList);
                var seedsListFiltered = new List<PlantSeedVm>();

                foreach (var item in seedsList)
                {
                    var user = _userManager.FindByIdAsync(item.UserId);
                    item.PlantOpinions = new List<PlantOpinionsVm>();

                    if (isCompany is true)
                    {
                        var role = _userManager.IsInRoleAsync(user.Result, "Company");
                        if (role.Result == true)
                        {
                            item.AccountName = user.Result.CompanyName;
                            item.Date = item.DateAdded.ToShortDateString();
                            var contactId = _userPlantService.GetContactDetailForSeed(item.Id);
                            var contactDetails = _userPlantService.GetContactDetail(contactId);
                            var contactDetailsVm = _mapper.Map<ContactDetailVm>(contactDetails);
                            item.ContactDetail = new ContactDetailVm();
                            item.ContactDetail = contactDetailsVm;

                            opinions = _plantRepo.GetPlantOpinions(detailId).Where(p => p.UserId == user.Result.Id).ProjectTo<PlantOpinionsVm>(_mapper.ConfigurationProvider).ToList();
                            item.PlantOpinions = opinions;
                            seedsListFiltered.Add(item);

                        }
                    }
                    else
                    {
                        var role = _userManager.IsInRoleAsync(user.Result, "PRIVATE_USER");
                        if (role.Result == true)
                        {
                            item.AccountName = _userContactDataService.UserAccountName(user);
                            item.Date = item.DateAdded.ToShortDateString();

                            opinions = _plantRepo.GetPlantOpinions(detailId).Where(p => p.UserId == user.Result.Id).ProjectTo<PlantOpinionsVm>(_mapper.ConfigurationProvider).ToList();
                            item.PlantOpinions = opinions;

                            seedsListFiltered.Add(item);
                        }
                    }
                }

                seedsToShow = seedsListFiltered.Skip(pageSize * ((int)pageNo - 1)).Take(pageSize).ToList();
            }

            string userId = null;

            if (userName != null)
            {
                var userInfo = _userManager.FindByNameAsync(userName);
                userId = userInfo.Result.Id;
            }

            var plantSeedsList = new PlantSeedsForListVm()
            {
                PageSize = pageSize,
                CurrentPage = pageNo,
                PlantSeeds = seedsToShow,
                Count = seeds.Count,
                PlantId = id,
                isCompany = isCompany,
                LoggedUserName = userId

            };

            return plantSeedsList;
        }
        public PlantSeedlingsForListVm GetAllPlantSeedlings(int id, int countryId, int regionId, int cityId, int pageSize, int? pageNo, bool isCompany)
        {
            var seedlings = new List<PlantSeedlingVm>();
            var seedlingsToShow = new List<PlantSeedlingVm>();
            var opinions = new List<PlantOpinionsVm>();
            var filteredUsersList = new List<string>();
            var seedlingsList = new List<PlantSeedlingVm>();

            var detailId = _plantRepo.GetPlantDetailId(id);
            seedlings = _plantRepo.GetPlantSeedlings(id).ProjectTo<PlantSeedlingVm>(_mapper.ConfigurationProvider).ToList();

            if (id != 0 && countryId == 0 && regionId == 0 && cityId == 0)
            {
                foreach (var item in seedlings)
                {
                    item.PlantOpinions = new List<PlantOpinionsVm>();
                    var user = _userManager.FindByIdAsync(item.UserId);
                    if (isCompany is true)
                    {
                        var role = _userManager.IsInRoleAsync(user.Result, "Company");
                        if (role.Result == true)
                        {
                            item.AccountName = user.Result.CompanyName;
                            item.Date = item.DateAdded.ToShortDateString();
                            var contactId = _userPlantService.GetContactDetailForSeedling(item.Id);
                            var contactDetails = _userPlantService.GetContactDetail(contactId);
                            var contactDetailsVm = _mapper.Map<ContactDetailVm>(contactDetails);
                            item.ContactDetail = new ContactDetailVm();
                            item.ContactDetail = contactDetailsVm;

                            opinions = _plantRepo.GetPlantOpinions(detailId).Where(p => p.UserId == user.Result.Id).ProjectTo<PlantOpinionsVm>(_mapper.ConfigurationProvider).ToList();
                            item.PlantOpinions = opinions;

                            filteredUsersList.Add(user.Result.Id);

                            seedlingsList = FilterSeedlingsList(seedlings, filteredUsersList);

                            seedlingsToShow = seedlingsList.Skip(pageSize * ((int)pageNo - 1)).Take(pageSize).ToList();
                        }
                    }
                    else
                    {
                        var role = _userManager.IsInRoleAsync(user.Result, "PRIVATE_USER");
                        if (role.Result == true)
                        {
                            item.AccountName = _userContactDataService.UserAccountName(user);
                            item.Date = item.DateAdded.ToShortDateString();

                            opinions = _plantRepo.GetPlantOpinions(detailId).Where(p => p.UserId == user.Result.Id).ProjectTo<PlantOpinionsVm>(_mapper.ConfigurationProvider).ToList();
                            item.PlantOpinions = opinions;

                            filteredUsersList.Add(user.Result.Id);
                            seedlingsList = FilterSeedlingsList(seedlings, filteredUsersList);


                            seedlingsToShow = seedlingsList.Skip(pageSize * ((int)pageNo - 1)).Take(pageSize).ToList();
                        }
                    }
                }
            }
            else
            {

                filteredUsersList = _userPlantService.FilterUsers(countryId, regionId, cityId, null, seedlings);

                seedlingsList = FilterSeedlingsList(seedlings, filteredUsersList);
                var seedlingsListFiltered = new List<PlantSeedlingVm>();

                foreach (var item in seedlingsList)
                {
                    var user = _userManager.FindByIdAsync(item.UserId);
                    if (isCompany is true)
                    {
                        var role = _userManager.IsInRoleAsync(user.Result, "Company");

                        if (role.Result == true)
                        {
                            item.AccountName = user.Result.CompanyName;
                            item.Date = item.DateAdded.ToShortDateString();
                            var contactId = _userPlantService.GetContactDetailForSeedling(item.Id);
                            var contactDetails = _userPlantService.GetContactDetail(contactId);
                            var contactDetailsVm = _mapper.Map<ContactDetailVm>(contactDetails);
                            item.ContactDetail = new ContactDetailVm();
                            item.ContactDetail = contactDetailsVm;

                            opinions = _plantRepo.GetPlantOpinions(detailId).Where(p => p.UserId == user.Result.Id).ProjectTo<PlantOpinionsVm>(_mapper.ConfigurationProvider).ToList();
                            item.PlantOpinions = opinions;
                            seedlingsListFiltered.Add(item);
                        }
                    }
                    else
                    {
                        var role = _userManager.IsInRoleAsync(user.Result, "PRIVATE_USER");
                        if (role.Result == true)
                        {
                            item.AccountName =  _userContactDataService.UserAccountName(user);
                            item.Date = item.DateAdded.ToShortDateString();

                            opinions = _plantRepo.GetPlantOpinions(detailId).Where(p => p.UserId == user.Result.Id).ProjectTo<PlantOpinionsVm>(_mapper.ConfigurationProvider).ToList();
                            item.PlantOpinions = opinions;
                            seedlingsListFiltered.Add(item);
                        }
                    }
                }

                seedlingsToShow = seedlingsListFiltered.Skip(pageSize * ((int)pageNo - 1)).Take(pageSize).ToList();
            }


            var plantSeedlingsList = new PlantSeedlingsForListVm()
            {
                PageSize = pageSize,
                CurrentPage = pageNo,
                PlantSeedlings = seedlingsToShow,
                Count = seedlings.Count,
                PlantId = id,
                isCompany = isCompany
            };

            return plantSeedlingsList;
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
                var plantDetailsImages = _plantRepo.GetPlantDetailsImages(plantDetailsVm.Id).ProjectTo<PlantDetailsImagesVm>(_mapper.ConfigurationProvider).ToList();

                if (plantDetailsImages != null)
                {
                    foreach (var image in plantDetailsImages)
                    {
                        plantVm.PlantDetails.PlantDetailsImages.Add(image);
                    }
                }

                var growthTypes = _plantRepo.GetPlantGrowthTypes(plantDetails.Id).ProjectTo<PlantGrowthTypeVm>(_mapper.ConfigurationProvider).ToList();
                if (growthTypes != null)
                {
                    plantDetailsVm.ListGrowthTypes = new ListGrowthTypesVm();
                    plantDetailsVm.ListGrowthTypes.GrowthTypesIds = new int[growthTypes.Count];
                    for (int i = 0; i < growthTypes.Count; i++)
                    {
                        plantDetailsVm.ListGrowthTypes.GrowthTypesIds[i] = growthTypes[i].GrowthTypeId;
                    }
                }

                var growingSeazons = _plantRepo.GetPlantGrowingSeazons(plantDetails.Id).ProjectTo<PlantGrowingSeazonsVm>(_mapper.ConfigurationProvider).ToList();
                if (growingSeazons != null)
                {
                    plantDetailsVm.ListGrowingSeazons = new ListGrowingSeazonsVm();
                    plantDetailsVm.ListGrowingSeazons.GrowingSeaznosIds = new int[growingSeazons.Count];
                    for (int i = 0; i < growingSeazons.Count; i++)
                    {
                        plantDetailsVm.ListGrowingSeazons.GrowingSeaznosIds[i] = growingSeazons[i].GrowingSeazonId;
                    }
                }

                var destinations = _plantRepo.GetPlantDestinations(plantDetails.Id).ProjectTo<PlantDestinationsVm>(_mapper.ConfigurationProvider).ToList();
                if (destinations != null)
                {
                    plantDetailsVm.ListPlantDestinations = new ListPlantDestinationsVm();
                    plantDetailsVm.ListPlantDestinations.DestinationsIds = new int[destinations.Count];
                    for (int i = 0; i < destinations.Count; i++)
                    {
                        plantDetailsVm.ListPlantDestinations.DestinationsIds[i] = destinations[i].DestinationId;
                    }
                }
            }

            return plantVm;
        }

        public void UpdatePlant(NewPlantVm model)
        {
            var getPhotoName = _plantRepo.GetPlantById(model.Id);
            var PhotoName = getPhotoName.Photo;

            string direction = null;
            if (model.Photo != null)
            {

                direction = "plantGallery/searchPhoto";
                var fileName = _imageService.UploadImage(model.Photo, model.FullName, direction);
                model.PhotoFileName = fileName;

                string imagePath = "plantGallery/searchPhoto/" + PhotoName;

                _imageService.DeleteImage(imagePath);

            }
            else
            {
                model.PhotoFileName = getPhotoName.Photo;
            }

            if (model.PlantDetails.Images != null)
            {
                if (model.PlantDetails.Images.Count > 0)
                {

                    foreach (var item in model.PlantDetails.Images)
                    {
                        direction = "plantGallery/plantDetailsGallery";
                        string fileName = _imageService.UploadImage(item, model.FullName, direction);
                        _plantRepo.AddPlantDetailsImages(fileName, model.PlantDetails.Id);
                    }
                }
            }

            if (model.PlantDetails.PlantDetailsImages != null)
            {
                if (model.PlantDetails.PlantDetailsImages.Count > 0)
                {
                    foreach (var image in model.PlantDetails.PlantDetailsImages)
                    {
                        if (image.IsChecked == true)
                        {
                            string imagePath = direction + "/" + image.ImageURL;
                            _imageService.DeleteImage(imagePath);
                            _plantRepo.DeleteImageFromGallery(image.Id);
                        }
                    }
                }
            }

            var plant = _mapper.Map<Plant>(model);

            if (model.PlantDetails.ColorId == 0)
                model.PlantDetails.ColorId = null;
            if (model.PlantDetails.FruitSizeId == 0)
                model.PlantDetails.FruitSizeId = null;
            if (model.PlantDetails.FruitTypeId == 0)
                model.PlantDetails.FruitTypeId = null;

            var plantDetails = _mapper.Map<PlantDetail>(model.PlantDetails);
            _plantRepo.UpdatePlant(plant);
            _plantRepo.UpdatePlantDetails(plantDetails);

            //Update Destinations
            if (model.PlantDetails.ListPlantDestinations != null)
                _plantDetailsSerrvice.UpdateEntity(
                                        model.PlantDetails.Id,
                                        model.PlantDetails.ListPlantDestinations.DestinationsIds,
                                        id => _plantRepo.GetPlantDestinations(model.PlantDetails.Id),
                                        (ids, plantId) => _plantRepo.AddPlantDestinations(model.PlantDetails.ListPlantDestinations.DestinationsIds, model.PlantDetails.Id),
                                        id => _plantRepo.DeletePlantDestinations(model.PlantDetails.Id)
                                                );

            //Update GrowingSeazons
            if (model.PlantDetails.ListGrowingSeazons != null)
                _plantDetailsSerrvice.UpdateEntity(
                                       model.PlantDetails.Id,
                                       model.PlantDetails.ListGrowingSeazons.GrowingSeaznosIds,
                                       id => _plantRepo.GetPlantGrowingSeazons(model.PlantDetails.Id),
                                       (ids, plantId) => _plantRepo.AddPlantGrowingSeazons(model.PlantDetails.ListGrowingSeazons.GrowingSeaznosIds, model.PlantDetails.Id),
                                       id => _plantRepo.DeletePlantDestinations(model.PlantDetails.Id)
                                               );

            //UpdateGrowthTypes
            if (model.PlantDetails.ListGrowthTypes != null)
                _plantDetailsSerrvice.UpdateEntity(
                                       model.PlantDetails.Id,
                                       model.PlantDetails.ListGrowthTypes.GrowthTypesIds,
                                       id => _plantRepo.GetPlantGrowthTypes(model.PlantDetails.Id),
                                       (ids, plantId) => _plantRepo.AddPlantGrowthTypes(model.PlantDetails.ListGrowthTypes.GrowthTypesIds, model.PlantDetails.Id),
                                       id => _plantRepo.DeletePlantGrowthTypes(model.PlantDetails.Id)
                                               );
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

                    _plantRepo.AddContactDetailForSeed(contactSeddToSave);
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
                    _plantRepo.AddContactDetailForSeedling(contactSeedlingToSave);

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
        public List<PlantSeedVm> FilterSeedsList(List<PlantSeedVm> seeds, List<string> filteredUsersList)
        {
            var filteredSeeds = new List<PlantSeedVm>();
            var seedsList = new List<PlantSeedVm>();

            foreach (var seed in seeds)
            {
                foreach (var items in filteredUsersList)
                {
                    if (seed.UserId == items)
                    {
                        filteredSeeds.Add(seed);
                    }
                }
            }

            if (filteredSeeds.Count > 1)
            {
                for (int i = 0; i < filteredSeeds.Count - 1; i++)
                {
                    if (i == 0)
                    {
                        seedsList.Add(filteredSeeds[i]);
                    }
                    if (filteredSeeds[i] != filteredSeeds[i + 1])
                    {
                        seedsList.Add(filteredSeeds[i + 1]);
                    }
                }
                return seedsList;
            }
            else
            {
                return filteredSeeds;
            }

        }
        public List<PlantSeedlingVm> FilterSeedlingsList(List<PlantSeedlingVm> seedlings, List<string> filteredUsersList)
        {
            var filteredSeedlings = new List<PlantSeedlingVm>();
            var seedlingsList = new List<PlantSeedlingVm>();

            foreach (var seedling in seedlings)
            {
                foreach (var items in filteredUsersList)
                {
                    if (seedling.UserId == items)
                    {
                        filteredSeedlings.Add(seedling);
                    }
                }
            }

            if (filteredSeedlings.Count > 1)
            {
                for (int i = 0; i < filteredSeedlings.Count - 1; i++)
                {
                    if (i == 0)
                    {
                        seedlingsList.Add(filteredSeedlings[i]);
                    }
                    if (filteredSeedlings[i] != filteredSeedlings[i + 1])
                    {
                        seedlingsList.Add(filteredSeedlings[i + 1]);
                    }
                }
                return seedlingsList;
            }
            else
            {
                return filteredSeedlings;
            }

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
