
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VFHCatalogMVC.Application.HelperClasses;
using VFHCatalogMVC.Application.Interfaces;
using VFHCatalogMVC.Application.ViewModels.Message;
using VFHCatalogMVC.Domain.Interface;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Application.Services
{
    public class HelperService : IHelperService
    {
        private readonly IPlantService _plantService;
        private readonly IUserService _userService;
        private readonly IMessageRepository _messageRepo;
        private readonly IMapper _mapper;
        public HelperService(IPlantService plantService, IUserService userService, IMessageRepository messageRepo, IMapper mapper)
        {
            _plantService = plantService;
            _userService = userService;
            _messageRepo= messageRepo;
            _mapper = mapper;
        }

        public List<SelectListItem> Cities(int regionId)
        {
            var cities = _userService.GetCities(regionId);
            var list = _userService.FillCityList(cities);
            return list;
        }

        public List<SelectListItem> Countries()
        {
            var countries = _userService.GetCountries();
            var list = _userService.FillCountryList(countries);
            return list;
        }

        public MessageDisplay MessagesToView(int type)
        {
            var messageDisplay = new MessageDisplay();

            if (type == 0)
            {
                messageDisplay.Received = false;
                messageDisplay.Sent = false;
                messageDisplay.ViewAll = true;
            }
            else
            {
                if (type == 1)
                {
                    messageDisplay.Received = true;
                    messageDisplay.Sent = false;
                    messageDisplay.ViewAll = false;
                }
                else
                {
                    messageDisplay.Received = false;
                    messageDisplay.Sent = true;
                    messageDisplay.ViewAll = false;
                }
       
            }

            return messageDisplay;
        }

        public List<SelectListItem> PlantColors()
        {
            var colors = _plantService.GetColors();
            var colorsList = _plantService.FillPropertyList(null, colors, null);
            return colorsList;
        }

        public List<SelectListItem> PlantGrowingSeaznos()
        {
            var growingSeaznos = _plantService.GetGrowingSeazons();
            var list = _plantService.FillPropertyList(null, null, growingSeaznos);
            return list;
        }

        public List<SelectListItem> PlantTypes()
        {
            var types = _plantService.GetPlantTypes();
            var plantTypes = _plantService.FillPropertyList(types, null, null);
            return plantTypes;
        }

        public List<SelectListItem> Regions(int countryId)
        {
            var regions = _userService.GetRegions(countryId);
            var list = _userService.FillRegionList(regions);
            return list;
        }
        public string UserAccountName(Task<ApplicationUser> user)
        {
            string userAccountName = null;

            if (user.Result.AccountName != null)
                userAccountName = user.Result.AccountName;
            if (user.Result.CompanyName != null)
                userAccountName = user.Result.CompanyName;
            return userAccountName;
        }
    }
}
