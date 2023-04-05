using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VFHCatalogMVC.Domain.Interface;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Infrastructure.Repositories
{
    public class HelperPlantRepository:IHelperPlantRepository
    {
        private Context _context;

        public HelperPlantRepository(Context context)
        {
            _context = context;
        }

       

        public string GetFruitSizeValue(int id)
        {
            var value = _context.FruitSizes.FirstOrDefault(p => p.Id == id);
            return value.Name;
        }

        public string GetFruitTypeValue(int id)
        {
            var value = _context.FruitTypes.FirstOrDefault(p => p.Id == id);
            return value.Name;

        }

        public string GetGrowingSezaonValue(int id)
        {
            var value = _context.GrowingSeazons.FirstOrDefault(p => p.Id == id);
            return value.Name;
        }

        public string GetGrowthTypeValue(int id)
        {
            var value = _context.GrowthTypes.FirstOrDefault(p => p.Id == id);
            return value.Name;
        }

        public string GetHeightValue(int id)
        {
            var value = _context.Heights.FirstOrDefault(p => p.Id == id);
            return value.Name;
        }

        public string GetPollinationValue(int id)
        {
            var value = _context.Pollinations.FirstOrDefault(p => p.Id == id);
            return value.Name;
        }
    }
}
