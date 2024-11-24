﻿using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Application.ViewModels.Plant;

namespace VFHCatalogMVC.Application.Interfaces.PlantInterfaces
{
    public interface IPlantItemProcessor<TVm> where TVm : PlantItemVm
    {
        List<TVm> ProcessItems(List<TVm> items, int detailId, bool isCompany, int pageSize, int? pageNo);
    }
}
