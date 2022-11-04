using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Domain.Interface;
using VFHCatalogMVC.Domain.Model;
using System.Linq;

namespace VFHCatalogMVC.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private Context _context;

        public CustomerRepository(Context context)
        {
            _context = context;
        }

        public Customer GetCustomer(string id)
        {
           var customer = _context.Customers.FirstOrDefault(x => x.Id == id);
            return customer;
        }
    }
}
