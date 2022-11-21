using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VFHCatalogMVC.Application.Interfaces;
using VFHCatalogMVC.Application.ViewModels.Adresses;
using VFHCatalogMVC.Application.ViewModels.Plant;
using VFHCatalogMVC.Domain.Interface;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Application.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressesRepository _addressesRepository;
        private readonly IMapper _mapper;

        public AddressService(IAddressesRepository addressesRepository, IMapper mapper)
        {
            _addressesRepository = addressesRepository;
            _mapper = mapper;
        }

        public void AddAddress(AddressVm address)
        {
            var addressToSave = _mapper.Map<Address>(address);
            _addressesRepository.AddAddress(addressToSave);
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

        public List<SelectListItem> FillVoivodeshipList(List<VoivodeshipVm> voivodeships)
        {
            List<SelectListItem> propertyList = new List<SelectListItem>();

            if (voivodeships != null)
            {
                propertyList.Add(new SelectListItem { Text = "-Wybierz-", Value = 0.ToString() });

                foreach (var type in voivodeships)
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
        public AddressVm GetAddres(int id)
        {
            var address = _addressesRepository.GetAddress(id);
            var addressVm = _mapper.Map<AddressVm>(address);

            return addressVm;
        }

        public List<CityVm> GetCities(int voivodeshipId)
        {
            var cities = _addressesRepository.GetCities(voivodeshipId).ProjectTo<CityVm>(_mapper.ConfigurationProvider).ToList();
            return cities;
        }

        public List<CountryVm> GetCountries()
        {
            var countries = _addressesRepository.GetCountries().ProjectTo<CountryVm>(_mapper.ConfigurationProvider).ToList();
            return countries;
        }

        public List<VoivodeshipVm> GetVoivodeships(int countryId)
        {
            var voivodeships = _addressesRepository.GetVoivodeships(countryId).ProjectTo<VoivodeshipVm>(_mapper.ConfigurationProvider).ToList();
            return voivodeships;
        }

        public List<string> FilterUsers(int countryId, int voivodeshipId, int cityId, List<PlantSeedVm> seeds)
        {
            var usersList = new List<string>();
            var address = new Address();

            foreach (var item in seeds)
            {
                address = _addressesRepository.GetAddressInfo(item.UserId);

                if (countryId != 0)
                {
                    if (voivodeshipId == 0 && cityId == 0)
                    {
                        if (address.CountryId == countryId)
                        {
                            usersList.Add(item.UserId);
                        }
                    }
                    if (voivodeshipId != 0 && cityId == 0)
                    {
                        if (address.CountryId == countryId && address.VoivodeshipId == voivodeshipId)
                        {
                            usersList.Add(item.UserId);
                        }
                    }
                    if (voivodeshipId != 0 && cityId != 0)
                    {
                        if (address.CountryId == countryId && address.VoivodeshipId == voivodeshipId && address.CityId == cityId)
                        {
                            usersList.Add(item.UserId);
                        }
                    }

                }
            }
            return usersList;
        }
    }
}
