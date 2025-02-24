﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Application.Mapping;
using VFHCatalogMVC.Application.ViewModels.Common;

namespace VFHCatalogMVC.Application.ViewModels.Plant.PlantDetails
{
    public class DestinationsVm : SelectListItemVm, IMapFrom<Domain.Model.Destination>
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Model.Destination, DestinationsVm>();
        }

    }
}
