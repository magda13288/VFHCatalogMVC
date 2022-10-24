using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Domain.Interface;
using VFHCatalogMVC.Domain.Model;
using System.Linq;

namespace VFHCatalogMVC.Infrastructure.Repositories
{
    public class PrivateUserRepository : IPrivateUserRepository
    {
        private Context _context;

        public PrivateUserRepository(Context context)
        {
            _context = context;
        }

        public PrivateUser GetPrivateUser(int id)
        {
            var user = _context.PrivateUsers.FirstOrDefault(p => p.Id == id);
            return user;
        }
    }
}
