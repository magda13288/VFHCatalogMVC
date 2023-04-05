using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Domain.Interface
{
    public interface IHelperPlantRepository
    {
        string GetFruitSizeValue(int id);
        string GetFruitTypeValue(int id);
        string GetGrowingSezaonValue(int id);
        string GetGrowthTypeValue(int id);
        string GetHeightValue(int id);
        string GetPollinationValue(int id);
       
    }
}
