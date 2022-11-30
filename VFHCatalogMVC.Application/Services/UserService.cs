using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using VFHCatalogMVC.Application.Interfaces;
using VFHCatalogMVC.Application.ViewModels.Adresses;
using VFHCatalogMVC.Application.ViewModels.Plant;
using VFHCatalogMVC.Domain.Interface;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(IUserRepository userRepo, IMapper mapper, UserManager<ApplicationUser> userManager )
        {
            _userRepo = userRepo;
            _mapper = mapper;
            _userManager = userManager;
        }

        public void AddAddress(AddressVm address)
        {
            var addressToSave = _mapper.Map<Address>(address);
            _userRepo.AddAddress(addressToSave);
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
        public AddressVm GetAddress(string userId)
        {
            var address = _userRepo.GetAddressInfo(userId);
            var addressVm = _mapper.Map<AddressVm>(address);

            return addressVm;
        }

        public List<CityVm> GetCities(int regionId)
        {
            var cities = _userRepo.GetCities(regionId).ProjectTo<CityVm>(_mapper.ConfigurationProvider).ToList();
            return cities;
        }

        public List<CountryVm> GetCountries()
        {
            var countries = _userRepo.GetCountries().ProjectTo<CountryVm>(_mapper.ConfigurationProvider).ToList();
            return countries;
        }

        public List<RegionVm> GetRegions(int countryId)
        {
            var regions = _userRepo.GetRegions(countryId).ProjectTo<RegionVm>(_mapper.ConfigurationProvider).ToList();
            return regions;
        }

        public List<string> FilterUsers(int countryId, int regionId, int cityId, List<PlantSeedVm> seeds, List<PlantSeedlingVm> seedlings)
        {
            var usersList = new List<string>();
            var address = new Address();
            //var genericList = typeof(List<>);
            //var dataType = new Type[] { typeof(PlantSeedVm), typeof(PlantSeedlingVm) };     

            if (seeds is null)
            {
                foreach (var item in seedlings)
                {
                    address = _userRepo.GetAddressInfo(item.UserId);

                    if (countryId != 0)
                    {
                        if (regionId == 0 && cityId == 0)
                        {
                            if (address.CountryId == countryId)
                            {
                                usersList.Add(item.UserId);
                            }
                        }
                        if (regionId != 0 && cityId == 0)
                        {
                            if (address.CountryId == countryId && address.RegionId == regionId)
                            {
                                usersList.Add(item.UserId);
                            }
                        }
                        if (regionId != 0 && cityId != 0)
                        {
                            if (address.CountryId == countryId && address.RegionId == regionId && address.CityId == cityId)
                            {
                                usersList.Add(item.UserId);
                            }
                        }

                    }
                }
            }
            else
            {
                foreach (var item in seeds)
                {
                    address = _userRepo.GetAddressInfo(item.UserId);

                    if (countryId != 0)
                    {
                        if (regionId == 0 && cityId == 0)
                        {
                            if (address.CountryId == countryId)
                            {
                                usersList.Add(item.UserId);
                            }
                        }
                        if (regionId != 0 && cityId == 0)
                        {
                            if (address.CountryId == countryId && address.RegionId == regionId)
                            {
                                usersList.Add(item.UserId);
                            }
                        }
                        if (regionId != 0 && cityId != 0)
                        {
                            if (address.CountryId == countryId && address.RegionId == regionId && address.CityId == cityId)
                            {
                                usersList.Add(item.UserId);
                            }
                        }

                    }
                }
            }

            return usersList;
        }
    }
}
