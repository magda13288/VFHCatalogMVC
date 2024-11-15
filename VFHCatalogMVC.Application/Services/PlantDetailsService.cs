using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using VFHCatalogMVC.Application.Interfaces;
using VFHCatalogMVC.Application.ViewModels.Plant;
using VFHCatalogMVC.Domain.Interface;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Application.Services
{
    public class PlantDetailsService : IPlantDetailsSerrvice
    {
        private readonly IPlantRepository _plantRepo;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;
        private readonly UserManager<ApplicationUser> _userManager;

        public PlantDetailsService()
        {
                
        }

        public PlantDetailsService(IPlantRepository plantRepo, IMapper mapper, IImageService imageService, UserManager<ApplicationUser> userManager)
        {
            _plantRepo = plantRepo;
            _mapper = mapper;
            _imageService = imageService;
            _userManager = userManager;
        }
        public int AddPlantDetails(NewPlantVm model)
        {
            //Save to table PlantDetails
            if (model.PlantDetails.ColorId == 0)
                model.PlantDetails.ColorId = null;
            if (model.PlantDetails.FruitSizeId == 0)
                model.PlantDetails.FruitSizeId = null;
            if (model.PlantDetails.FruitTypeId == 0)
                model.PlantDetails.FruitTypeId = null;

            var newPlantDetail = _mapper.Map<PlantDetail>(model.PlantDetails);
            var plantDetailId = _plantRepo.AddPlantDetails(newPlantDetail, model.Id);

            //Save to PlantGrowthTypes
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
                    string _DIR = "plantGallery/plantDetailsGallery";

                    foreach (var item in model.PlantDetails.Images)
                    {
                        string fileName = _imageService.UploadImage(item, model.FullName, _DIR);
                        _plantRepo.AddPlantDetailsImages(fileName, plantDetailId);
                    }
                }
            }

            return plantDetailId;
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

        public PlantDetailsVm GetPlantDetails(int id)
        {
            var plantDetails = _plantRepo.GetPlantDetails(id);
            var plantDetailsVm = _mapper.Map<PlantDetailsVm>(plantDetails);

            if (plantDetails != null)
            {

                var plant = _plantRepo.GetPlantById(id);
                var plantVm = _mapper.Map<PlantForListVm>(plant);

                if (plantDetailsVm.ColorId != null)
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

                plantDetailsVm.Plant = plantVm;

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


    }
}
