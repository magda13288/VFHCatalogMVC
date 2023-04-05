
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using VFHCatalogMVC.Application.HelperClasses;
using VFHCatalogMVC.Application.Interfaces;
using VFHCatalogMVC.Application.ViewModels.Adresses;
using VFHCatalogMVC.Application.ViewModels.Message;
using VFHCatalogMVC.Domain.Interface;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Application.Services
{
    public class HelperPlantService : IHelperPlantService
    {
        private readonly IPlantService _plantService;
        private readonly IUserService _userService;
        private readonly IMessageRepository _messageRepo;
        private readonly IHelperPlantRepository _helperRepo;
        private readonly IMapper _mapper;
        public HelperPlantService(IPlantService plantService, IUserService userService, IMessageRepository messageRepo, IMapper mapper, IHelperPlantRepository helperRepo)
        {
            _plantService = plantService;
            _userService = userService;
            _messageRepo = messageRepo;
            _mapper = mapper;
            _helperRepo = helperRepo;
        }   

        public IndexPlantType GetIndexPlantType(bool seeds, bool seedlings, bool newPlant)
        {
           var index = new IndexPlantType() {seeds = seeds, seedlings = seedlings, newPlant = newPlant };

            return index;        
        }

        public List<SelectListItem> PlantAdditionalFeatures()
        {
            var additionalFeatures = _plantService.GetAdditionalFeatures();
            var additionalFeaturesList = _plantService.FillPropertyList(null, null, null, null, null, additionalFeatures);
            return additionalFeaturesList;
        }

        public List<SelectListItem> PlantColors()
        {
            var colors = _plantService.GetColors();
            var colorsList = _plantService.FillPropertyList(null, colors, null,null,null,null);
            return colorsList;
        }

        public List<SelectListItem> PlantDestinations()
        {
            var destinations = _plantService.GetDestinations();
            var destinationsList = _plantService.FillPropertyList(null,null,null,destinations,null,null);
            return destinationsList;
        }

        public List<SelectListItem> PlantGrowingSeaznos()
        {
            var growingSeaznos = _plantService.GetGrowingSeazons();
            var list = _plantService.FillPropertyList(null, null, growingSeaznos,null,null,null);
            return list;
        }

        public List<SelectListItem> PlantPositions()
        {
          var positions = _plantService.GetPositions();
          var positionList = _plantService.FillPropertyList(null, null, null, null, positions, null);
          return positionList;
        }

        public List<SelectListItem> PlantTypes()
        {
            var types = _plantService.GetPlantTypes();
            var plantTypes = _plantService.FillPropertyList(types, null, null,null,null,null);
            return plantTypes;
        }

       
    }
}
