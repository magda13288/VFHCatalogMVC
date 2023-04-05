using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VFHCatalogMVC.Application.HelperClasses;
using VFHCatalogMVC.Application.Interfaces;
using VFHCatalogMVC.Application.ViewModels.Adresses;
using VFHCatalogMVC.Domain.Interface;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Application.Services
{
    public class HelperUserService: IHelperUserService
    {
        //private readonly IUserService _userService;
        private readonly IMessageRepository _messageRepo;
        private readonly IHelperUserRepository _helperRepo;
        private readonly IMapper _mapper;

        public HelperUserService( IMessageRepository messageRepo, IHelperUserRepository helperRepo, IMapper mapper)
        {        
            _messageRepo = messageRepo;
            _helperRepo = helperRepo;
            _mapper = mapper;
        }

        public List<SelectListItem> Cities(int regionId)
        {
            var cities = GetCities(regionId);
            var list = FillCityList(cities);
            return list;
        }

        public List<SelectListItem> Countries()
        {
            var countries = GetCountries();
            var list = FillCountryList(countries);
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
        public List<SelectListItem> Regions(int countryId)
        {
            var regions = GetRegions(countryId);
            var list = FillRegionList(regions);
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

        public List<SelectListItem> FillCountryList(List<CountryVm> countries)
        {
            List<SelectListItem> propertyList = new List<SelectListItem>();

            if (countries != null)
            {
                propertyList.Add(new SelectListItem { Text = "-Wybierz-", Value = 0.ToString() });

                foreach (var type in countries)
                {
                    propertyList.Add(new SelectListItem { Text = type.Name, Value = type.Id.ToString() });
                }
            }
            else
            {
                propertyList.Add(new SelectListItem { Text = "-Wybierz-", Value = 0.ToString() });
            }
            return propertyList;
        }

        public List<SelectListItem> FillRegionList(List<RegionVm> regions)
        {
            List<SelectListItem> propertyList = new List<SelectListItem>();

            if (regions != null)
            {
                propertyList.Add(new SelectListItem { Text = "-Wybierz-", Value = 0.ToString() });

                foreach (var type in regions)
                {
                    propertyList.Add(new SelectListItem { Text = type.Name, Value = type.Id.ToString() });
                }
            }
            else
            {
                propertyList.Add(new SelectListItem { Text = "-Wybierz-", Value = 0.ToString() });
            }
            return propertyList;
        }

        public List<SelectListItem> FillCityList(List<CityVm> city)
        {
            List<SelectListItem> propertyList = new List<SelectListItem>();

            if (city != null)
            {
                propertyList.Add(new SelectListItem { Text = "-Wybierz-", Value = 0.ToString() });

                foreach (var type in city)
                {
                    propertyList.Add(new SelectListItem { Text = type.Name, Value = type.Id.ToString() });
                }
            }
            else
            {
                propertyList.Add(new SelectListItem { Text = "-Wybierz-", Value = 0.ToString() });
            }
            return propertyList;
        }
        public List<CityVm> GetCities(int regionId)
        {
            var cities = _helperRepo.GetCities(regionId).ProjectTo<CityVm>(_mapper.ConfigurationProvider).ToList();
            return cities;
        }

        public List<CountryVm> GetCountries()
        {
            var countries = _helperRepo.GetCountries().ProjectTo<CountryVm>(_mapper.ConfigurationProvider).ToList();
            return countries;
        }

        public List<RegionVm> GetRegions(int countryId)
        {
            var regions = _helperRepo.GetRegions(countryId).ProjectTo<RegionVm>(_mapper.ConfigurationProvider).ToList();
            return regions;
        }
    }
}
