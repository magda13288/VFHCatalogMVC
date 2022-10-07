using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VFHCatalogMVC.Application.Interfaces;
using VFHCatalogMVC.Application.Mapping;
using VFHCatalogMVC.Domain.Model;
using VFHCatalogMVC.Application.ViewModels.Plant;
using VFHCatalogMVC.Domain.Interface;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace VFHCatalogMVC.Application.Services
{
    public class PlantService : IPlantService
    {
        private readonly IPlantRepository _plantRepo;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PlantService(IPlantRepository plantRepo, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _plantRepo = plantRepo;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }

        public int AddPlant(NewPlantVm model)
        {
            if (model.SectionId == 0)
                model.SectionId = null;
            //Save to table Plant
            var newPlant = _mapper.Map<Plant>(model);

            if (model.Photo != null)
            {
                var fileName = UploadImage(model);     
                newPlant.Photo = fileName;
            }       

            var id = _plantRepo.AddPlant(newPlant);

            //Save to table PlantDetails
            if (model.PlantDetails.ColorId == 0)
                model.PlantDetails.ColorId = null;
            if (model.PlantDetails.FruitSizeId == 0)
                model.PlantDetails.ColorId = null;
            if (model.PlantDetails.FruitTypeId == 0)
                model.PlantDetails.FruitTypeId = null;

            var newPlantDetail = _mapper.Map<PlantDetail>(model.PlantDetails);
            var plantDetailId = _plantRepo.AddPlantDetails(newPlantDetail, id);

            //Save to PlantGrowthTypes
            if (model.PlantDetails.ListGrowthTypes.GrowthTypesIds.Length > 0)
            {
                _plantRepo.AddPlantGrowthTypes(model.PlantDetails.ListGrowthTypes.GrowthTypesIds, plantDetailId);
            }
            //Save to PlantDestinations
            if (model.PlantDetails.ListPlantDestinations.DestinationsIds.Length > 0)
            {
                _plantRepo.AddPlantDestinations(model.PlantDetails.ListPlantDestinations.DestinationsIds, plantDetailId);
            }
            //Save to PlantGrowingSeaznos
            if (model.PlantDetails.ListGrowingSeazons.GrowingSeaznosIds.Length > 0)
            {
                _plantRepo.AddPlantGrowingSeazons(model.PlantDetails.ListGrowingSeazons.GrowingSeaznosIds, plantDetailId);
            }
            return id;
        }
        private string UploadImage(NewPlantVm model)
        {
            string fileName = null;
            if (model.Photo != null)
            {
                string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                string extension = Path.GetExtension(model.Photo.FileName);
                fileName = Guid.NewGuid().ToString() + "-" + model.FullName + extension ;  
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Photo.CopyTo(fileStream);
                }
            }

            return fileName;
        }

        public int EditPlant(EditPlantVm plant)
        {
            throw new NotImplementedException();
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
                }
            }
            else
            {
                plants = _plantRepo.GetAllActivePlants().Where(p => p.FullName.StartsWith(searchString))
                       .ProjectTo<PlantForListVm>(_mapper.ConfigurationProvider).ToList();
            }

            plantsToShow = plants.Skip((int)(pageSize * (pageNo - 1))).Take(pageSize).ToList();

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

        public PlantDetailsVm GetPlantDetails(int id)
        {
            var plantDetails = _plantRepo.GetPlantDetails(id);
            var plantDetailsVm = _mapper.Map<PlantDetailsVm>(plantDetails);
            plantDetailsVm.ColorName = _plantRepo.GetPlantColorName(plantDetailsVm.Id);
            plantDetailsVm.FruitSizeName = _plantRepo.GetPlantFruitSizeName(plantDetailsVm.Id);
            plantDetailsVm.FruitTypeName = _plantRepo.GetPlantFriutTypeName(plantDetailsVm.Id);

            //GrowthTypesNames
       
            var propertyNames = new List<string>();
            propertyNames = GetGrowthTypesNames(plantDetails.Id);

            foreach (var item in propertyNames)
            {
                plantDetailsVm.ListGrowthTypes.GrowthTypesNames.Add(item);
            }

            propertyNames.Clear();

            //DestiantionsNames

            propertyNames = GetDestinationsNames(plantDetails.Id);

            foreach (var item in propertyNames)
            {
                plantDetailsVm.ListPlantDestinations.DestinationsNames.Add(item);
            }

            propertyNames.Clear();

            //GrowingSeazonsNames

            propertyNames = GetGrowthTypesNames(plantDetails.Id);

            foreach (var item in propertyNames)
            {
                plantDetailsVm.ListGrowingSeazons.GrwoingSeazonsNames.Add(item);
            }

            propertyNames.Clear();

            return plantDetailsVm;
        }

        public List<string> GetGrowthTypesNames(int id)
        {
            var plantGrowthTypes = new List<PlantGrowthTypeVm>();
            plantGrowthTypes = _plantRepo.GetPlantGrowthTypes(id).ProjectTo<PlantGrowthTypeVm>(_mapper.ConfigurationProvider).ToList();
            var growthTypes = _plantRepo.GetGrowthTypes().ProjectTo<GrowthTypeVm>(_mapper.ConfigurationProvider).ToList();

            var propertyIds = new List<int>();

            foreach (var items in plantGrowthTypes)
            {
                propertyIds.Add(items.GrowthTypeId);
            }

            var propertyNames = new List<string>();

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
                foreach (var i in growthTypesForPlant)
                {
                    propertyNames.Add(i.Name);
                }
            }

            return propertyNames;
        }

        public List<string> GetDestinationsNames(int id)
        {
            var plantDestinations = new List<PlantDestinationsVm>();
            plantDestinations = _plantRepo.GetPlantDestinations(id).ProjectTo<PlantDestinationsVm>(_mapper.ConfigurationProvider).ToList();
            var destinations = _plantRepo.GetDestinations().ProjectTo<DestinationsVm>(_mapper.ConfigurationProvider).ToList();

            var destinationsForPlants = new List<DestinationsVm>();
            var propertyIds = new List<int>();
            var propertyNames = new List<string>();

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
                foreach (var i in destinationsForPlants)
                {
                    propertyNames.Add(i.Name);
                }
            }

            return propertyNames;
        }

        public List<string> GetGrowingSeaznosNames(int id)
        {
            var plantGrowingSeazons = new List<PlantGrowingSeazonsVm>();
            plantGrowingSeazons = _plantRepo.GetPlantGrowingSeazons(id).ProjectTo<PlantGrowingSeazonsVm>(_mapper.ConfigurationProvider).ToList();
            var growingSeazons = _plantRepo.GetGrowingSeazons().ProjectTo<GrowingSeazonVm>(_mapper.ConfigurationProvider).ToList();

            var growingSeaznosForPlant = new List<GrowingSeazonVm>();
            var propertyIds = new List<int>();
            var propertyNames = new List<string>();

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
                foreach (var i in growingSeaznosForPlant)
                {
                    propertyNames.Add(i.Name);
                }
            }
            return propertyNames;
        }


        //public List<string> GetPropertyNames(List<PlantGrowthTypeVm> growthTypesList, List<PlantGrowingSeazonsVm> growingSeazonsList, List<PlantDestinationsVm> destinationsList)
        //{
        //    var plantGrowthTypes = new List<PlantGrowthTypeVm>();
        //    plantGrowthTypes = _plantRepo.GetPlantGrowthTypes(plantDetailsVm.Id).ProjectTo<PlantGrowthTypeVm>(_mapper.ConfigurationProvider).ToList();
        //    var growthTypes = _plantRepo.GetGrowthTypes().ProjectTo<GrowthTypeVm>(_mapper.ConfigurationProvider).ToList();
        //    var propertyIdsIds = new List<int>();

        //    foreach (var items in plantGrowthTypes)
        //    {
        //        propertyIdsIds.Add(items.GrowthTypeId);
        //    }

        //    var growthTypesNames = new List<string>();
        //    var growthTypesForPlant = new List<GrowthTypeVm>();

        //    foreach (var items in propertyIdsIds)
        //    {
        //        foreach (var item in growthTypes)
        //        {
        //            if (item.Id == items)
        //            {
        //                growthTypesForPlant.Add(growthTypes.FirstOrDefault(p => p.Id == items));
        //            }
        //        }
        //        foreach (var i in growthTypesForPlant)
        //        {
        //            growthTypesNames.Add(i.Name);
        //        }
        //    }
        //}


        public List<PlantGroupsVm> GetPlantGroups(int? typeId)
        {
            var groups = _plantRepo.GetAllGroups().Where(e => e.PlantTypeId == typeId).ProjectTo<PlantGroupsVm>(_mapper.ConfigurationProvider).ToList();

            //var groupsList = new ListPlantGroupsVm()
            //{
            //    Groups = groups
            //};

            return groups;
        }

        public List<PlantTypesVm> GetPlantTypes()
        {
            var types = _plantRepo.GetAllTypes().OrderBy(p=>p.Id).ProjectTo<PlantTypesVm>(_mapper.ConfigurationProvider).ToList();

            //var typesList = new ListPlantTypesVm()
            //{
            //    Types = types
            //};

            return types;
        }

        public List<PlantSectionsVm> GetPlantSections(int? groupId)
        {
            var sections = _plantRepo.GetAllSections().Where(e => e.PlantGroupId == groupId).ProjectTo<PlantSectionsVm>(_mapper.ConfigurationProvider).ToList();

            //var sectionsList = new ListPlantSectionsVm()
            //{
            //    Sections = sections
            //};

            return sections;
        }

        public List<GrowthTypeVm> GetGrowthTypes(int typeId, int groupId, int sectionId)
        {
            List<GrowthTypeVm> growthTyes = new List<GrowthTypeVm>();

            if (typeId == 1)
            {
                growthTyes = _plantRepo.GetGrowthTypes().Where(e => e.PlantTypeId == typeId && e.PlantGroupId == groupId && e.PlantSectionId == sectionId).OrderBy(e=>e.PlantTypeId).ProjectTo<GrowthTypeVm>(_mapper.ConfigurationProvider).ToList();
            }
            else if (typeId == 2 || typeId == 3)
            {
                growthTyes = _plantRepo.GetGrowthTypes().Where(e => e.PlantTypeId == typeId).OrderBy(e=>e.PlantTypeId).ProjectTo<GrowthTypeVm>(_mapper.ConfigurationProvider).ToList();
            }

            return growthTyes;
        }

        public List<DestinationsVm> GetDestinations()
        {
            List<DestinationsVm> destinationsList = new List<DestinationsVm>();

            destinationsList = _plantRepo.GetDestinations().OrderBy(p => p.Id).ProjectTo<DestinationsVm>(_mapper.ConfigurationProvider).ToList();

            return destinationsList;
        }

        public List<ColorsVm> GetColors()
        {
            List<ColorsVm> colorsList = new List<ColorsVm>();

            colorsList = _plantRepo.GetColors().OrderBy(p => p.Id).ProjectTo<ColorsVm>(_mapper.ConfigurationProvider).ToList();

            return colorsList;
        }

        public List<GrowingSeazonVm> GetGrowingSeazons()
        {
            List<GrowingSeazonVm> growingSeazonList = new List<GrowingSeazonVm>();

            growingSeazonList = _plantRepo.GetGrowingSeazons().OrderBy(p => p.Id).ProjectTo<GrowingSeazonVm>(_mapper.ConfigurationProvider).ToList();

            return growingSeazonList;
        }

        public List<FruitSizeVm> GetFruitSize(int typeId, int groupId, int? sectionId)
        {
            List<FruitSizeVm> fruitSizeList = new List<FruitSizeVm>();
            if (!sectionId.HasValue)
            {
                fruitSizeList = _plantRepo.GetFruitSizes().Where(p => p.PlantTypeId == typeId && p.PlantGroupId == groupId).OrderBy(p => p.Id).ProjectTo<FruitSizeVm>(_mapper.ConfigurationProvider).ToList();

            }
            if (sectionId.HasValue)
            {
                fruitSizeList = _plantRepo.GetFruitSizes().Where(p => p.PlantTypeId == typeId && p.PlantGroupId == groupId && p.PlantSectionId == sectionId).OrderBy(p => p.Id).ProjectTo<FruitSizeVm>(_mapper.ConfigurationProvider).ToList();
            }

            
            return fruitSizeList;
        }

        public List<FruitTypeVm> GetFruitType(int typeId, int groupId, int? sectionId)
        {
            List<FruitTypeVm> fruitTypeList = new List<FruitTypeVm>();

            if (!sectionId.HasValue)
            {
                fruitTypeList = _plantRepo.GetFruitTypes().Where(p => p.PlantTypeId == typeId && p.PlantGroupId == groupId).OrderBy(p => p.Id).ProjectTo<FruitTypeVm>(_mapper.ConfigurationProvider).ToList();

            }
            if (sectionId.HasValue)
            {
                fruitTypeList = _plantRepo.GetFruitTypes().Where(p => p.PlantTypeId == typeId && p.PlantGroupId == groupId && p.PlantSectionId == sectionId).OrderBy(p => p.Id).ProjectTo<FruitTypeVm>(_mapper.ConfigurationProvider).ToList();
            }

            return fruitTypeList;
        }
    }
}
