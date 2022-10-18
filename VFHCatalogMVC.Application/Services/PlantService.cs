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
using Microsoft.AspNetCore.Http;

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
                string direction = "plantGallery/searchPhoto";
                var fileName = UploadImage(model.Photo,model.FullName,direction);     
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

            if (model.PlantDetails.Images.Count > 0)
            {
                string direction = "plantGallery/plantDetailsGallery";

                foreach (var item in model.PlantDetails.Images)
                {
                    string fileName = UploadImage(item,model.FullName,direction);
                    _plantRepo.AddPlantDetailsImages(fileName, plantDetailId);
                }
            }

            return id;
        }

        private string UploadImage(IFormFile file,string name,string path)
        {
            string fileName = null;

            if (file != null)
            {
                try
                {
                    string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, path);
                    string extension = Path.GetExtension(file.FileName);
                    fileName = Guid.NewGuid().ToString() + "-" + name + extension;
                    string filePath = Path.Combine(uploadDir, fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return fileName;
        }

        private void DeleteImage(string path)
        {
            var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, path);
            if (System.IO.File.Exists(imagePath))
            {
                try
                {
                    System.IO.File.Delete(imagePath);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
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
            if (plantDetails != null)
            {              
                var plant = _plantRepo.GetPlantById(id);
                var plantVm = _mapper.Map<PlantForListVm>(plant);
                plantDetailsVm.ColorName = _plantRepo.GetPlantColorName(plantDetailsVm.Id);
                plantDetailsVm.FruitSizeName = _plantRepo.GetPlantFruitSizeName(plantDetailsVm.FruitSizeId);
                plantDetailsVm.FruitTypeName = _plantRepo.GetPlantFriutTypeName(plantDetailsVm.FruitTypeId);
                plantDetailsVm.Plant = plantVm;

                //GrowthTypesNames

                var propertyNames = new List<string>();
                propertyNames = GetGrowthTypesNames(plantDetailsVm.Id);
                plantDetailsVm.ListGrowthTypes = new ListGrowthTypesVm();
                plantDetailsVm.ListGrowthTypes.GrowthTypesNames = new List<string>();

                foreach (var item in propertyNames)
                {
                    plantDetailsVm.ListGrowthTypes.GrowthTypesNames.Add(item);

                }

                propertyNames.Clear();

                //DestiantionsNames

                propertyNames = GetDestinationsNames(plantDetailsVm.Id);

                plantDetailsVm.ListPlantDestinations = new ListPlantDestinationsVm();
                plantDetailsVm.ListPlantDestinations.DestinationsNames = new List<string>();
                foreach (var item in propertyNames)
                {
                    plantDetailsVm.ListPlantDestinations.DestinationsNames.Add(item);

                }

                propertyNames.Clear();

                //GrowingSeazonsNames

                propertyNames = GetGrowingSeaznosNames(plantDetailsVm.Id);

                plantDetailsVm.ListGrowingSeazons = new ListGrowingSeazonsVm();
                plantDetailsVm.ListGrowingSeazons.GrwoingSeazonsNames = new List<string>();
                foreach (var item in propertyNames)
                {
                    plantDetailsVm.ListGrowingSeazons.GrwoingSeazonsNames.Add(item);
                }

                propertyNames.Clear();

                //PlantGallery
                var plantGallery = new List<PlantDetailsImagesVm>();

                plantGallery = _plantRepo.GetPlantDetailsImages(plantDetails.Id).ProjectTo<PlantDetailsImagesVm>(_mapper.ConfigurationProvider).ToList();

                if (plantGallery.Count > 0)
                {
                    foreach (var image in plantGallery)
                    {
                        plantDetailsVm.PlantDetailsImages.Add(image);
                    }
                }
            }

            //add Opinions in PlantOpinnion after adding logic for users

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
            }

            foreach (var i in growthTypesForPlant)
            {
                propertyNames.Add(i.Name);
                propertyNames.Add(",");
            }
            propertyNames = propertyNames.Take(propertyNames.Count - 1).ToList();

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
            }
            foreach (var i in destinationsForPlants)
            {
                propertyNames.Add(i.Name);
                propertyNames.Add(",");  
            }

             propertyNames = propertyNames.Take(propertyNames.Count - 1).ToList();

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
            }
            foreach (var i in growingSeaznosForPlant)
            {
                propertyNames.Add(i.Name);
                propertyNames.Add(",");
            }

            propertyNames = propertyNames.Take(propertyNames.Count - 1).ToList();

            return propertyNames;
        }

        public List<PlantGroupsVm> GetPlantGroups(int? typeId)
        {
            var groups = _plantRepo.GetAllGroups().Where(e => e.PlantTypeId == typeId).ProjectTo<PlantGroupsVm>(_mapper.ConfigurationProvider).ToList();

            return groups;
        }

        public List<PlantTypesVm> GetPlantTypes()
        {
            var types = _plantRepo.GetAllTypes().OrderBy(p=>p.Id).ProjectTo<PlantTypesVm>(_mapper.ConfigurationProvider).ToList();

            return types;
        }

        public List<PlantSectionsVm> GetPlantSections(int? groupId)
        {
            var sections = _plantRepo.GetAllSections().Where(e => e.PlantGroupId == groupId).ProjectTo<PlantSectionsVm>(_mapper.ConfigurationProvider).ToList();

            return sections;
        }

        public List<GrowthTypeVm> GetGrowthTypes(int typeId, int groupId, int? sectionId)
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

        public NewPlantVm GetPlantToEdit(int id)
        {
            var plant = _plantRepo.GetPlantById(id);
            var plantVm = _mapper.Map<NewPlantVm>(plant);
            var plantDetails = _plantRepo.GetPlantDetails(id);
            var plantDetailsVm = _mapper.Map<PlantDetailsVm>(plantDetails);

            var plantDetailsImages = _plantRepo.GetPlantDetailsImages(plantDetailsVm.Id).ProjectTo<PlantDetailsImagesVm>(_mapper.ConfigurationProvider).ToList();

            plantVm.PlantDetails = plantDetailsVm;
            foreach (var image in plantDetailsImages)
            {
                plantVm.PlantDetails.PlantDetailsImages.Add(image);
            }

            var growthTypes = _plantRepo.GetPlantGrowthTypes(plantDetails.Id).ProjectTo<PlantGrowthTypeVm>(_mapper.ConfigurationProvider).ToList();

            plantDetailsVm.ListGrowthTypes = new ListGrowthTypesVm();
            plantDetailsVm.ListGrowthTypes.GrowthTypesIds = new int[growthTypes.Count];
            for (int i=0;i<growthTypes.Count;i++)
            {
                plantDetailsVm.ListGrowthTypes.GrowthTypesIds[i] = growthTypes[i].GrowthTypeId; 
            }
           
            var growingSeazons = _plantRepo.GetPlantGrowingSeazons(plantDetails.Id).ProjectTo<PlantGrowingSeazonsVm>(_mapper.ConfigurationProvider).ToList();
            plantDetailsVm.ListGrowingSeazons = new ListGrowingSeazonsVm();
            plantDetailsVm.ListGrowingSeazons.GrowingSeaznosIds = new int[growingSeazons.Count];
            for (int i = 0; i < growingSeazons.Count; i++)
            {
                plantDetailsVm.ListGrowingSeazons.GrowingSeaznosIds[i] = growingSeazons[i].GrowingSeazonId;
            }

            var destinations = _plantRepo.GetPlantDestinations(plantDetails.Id).ProjectTo<PlantDestinationsVm>(_mapper.ConfigurationProvider).ToList();
            plantDetailsVm.ListPlantDestinations = new ListPlantDestinationsVm();
            plantDetailsVm.ListPlantDestinations.DestinationsIds = new int[destinations.Count];
            for (int i = 0; i < destinations.Count; i++)
            {
                plantDetailsVm.ListPlantDestinations.DestinationsIds[i] = destinations[i].DestinationId;
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
                var fileName = UploadImage(model.Photo, model.FullName, direction);
                model.PhotoFileName = fileName;

                string imagePath = "plantGallery/searchPhoto/" + PhotoName;

                DeleteImage(imagePath);

            }
            else
            {
                model.PhotoFileName = getPhotoName.Photo;
            }

            var plant = _mapper.Map<Plant>(model);
            _plantRepo.UpdatePlant(plant);

            var plantDetails = _mapper.Map<PlantDetail>(model.PlantDetails);
            _plantRepo.UpdatePlantDetails(plantDetails);

            if (model.PlantDetails.Images.Count > 0)
            {
                
                foreach (var item in model.PlantDetails.Images)
                {
                    direction = "plantGallery/plantDetailsGallery";
                    string fileName = UploadImage(item, model.FullName, direction);
                    _plantRepo.AddPlantDetailsImages(fileName, plantDetails.Id);
                }
            }

            if (model.PlantDetails.PlantDetailsImages.Count > 0)
            {
                foreach (var image in model.PlantDetails.PlantDetailsImages)
                {
                    if (image.IsChecked == true)
                    {
                        string imagePath = direction +"/"+ image.ImageURL;
                        DeleteImage(imagePath);
                        _plantRepo.DeleteImageFromGallery(image.Id);
                    }
                }
            }

            //Update Destinations
            UpdatePlantDestinations(model);

            //Update GrowingSeazons
            UpdatePlantGrowingSeazons(model);

            //UpdateGrowthTypes

            UpdatePlantGrowthTypes(model);
            
        }

        private void UpdatePlantDestinations(NewPlantVm model)
        {
            var plantDestinations = new List<PlantDestination>();

            for (int i = 0; i < model.PlantDetails.ListPlantDestinations.DestinationsIds.Length; i++)
            {
                plantDestinations.Add(new PlantDestination { DestinationId = model.PlantDetails.ListPlantDestinations.DestinationsIds[i], PlantDetailId = model.PlantDetails.Id });
            }

            if (model.PlantDetails.ListPlantDestinations.DestinationsIds.Length == plantDestinations.Count)
            {
                _plantRepo.UpdatePlantDestiantions(plantDestinations);
            }
            else
            {
                _plantRepo.DeletePlantDestinations(model.PlantDetails.Id);
                _plantRepo.AddPlantDestinations(model.PlantDetails.ListPlantDestinations.DestinationsIds,model.PlantDetails.Id);
            }
        }

        private void UpdatePlantGrowingSeazons(NewPlantVm model)
        {
            var plantGrowingSeaznos = new List<PlantGrowingSeazon>();
            for (int i = 0; i < model.PlantDetails.ListGrowingSeazons.GrowingSeaznosIds.Length; i++)
            {
                plantGrowingSeaznos.Add(new PlantGrowingSeazon { GrowingSeazonId = model.PlantDetails.ListGrowingSeazons.GrowingSeaznosIds[i], PlantDetailId = model.PlantDetails.Id });
            }

            if (model.PlantDetails.ListGrowingSeazons.GrowingSeaznosIds.Length == plantGrowingSeaznos.Count)
            {
                _plantRepo.UpdatePlantGrowingSeazons(plantGrowingSeaznos);
            }
            else
            {
                _plantRepo.DeletePlantGrowingSeazons(model.PlantDetails.Id);
                _plantRepo.AddPlantGrowingSeazons(model.PlantDetails.ListGrowingSeazons.GrowingSeaznosIds, model.PlantDetails.Id);
            }
        }

        private void UpdatePlantGrowthTypes(NewPlantVm model)
        {
            var plantGrowthTypes = new List<PlantGrowthType>();

            for (int i = 0; i < model.PlantDetails.ListGrowthTypes.GrowthTypesIds.Length; i++)
            {
                plantGrowthTypes.Add(new PlantGrowthType { GrowthTypeId = model.PlantDetails.ListGrowthTypes.GrowthTypesIds[i], PlantDetailId = model.PlantDetails.Id });
            }

            if (model.PlantDetails.ListGrowthTypes.GrowthTypesIds.Length == plantGrowthTypes.Count)
            {
                _plantRepo.UpdatePlantGrowthTypes(plantGrowthTypes);
            }
            else
            {
                _plantRepo.DeletePlantGrowthTypes(model.PlantDetails.Id);
                _plantRepo.AddPlantGrowthTypes(model.PlantDetails.ListGrowthTypes.GrowthTypesIds, model.PlantDetails.Id);
            }

        }
    }
}
