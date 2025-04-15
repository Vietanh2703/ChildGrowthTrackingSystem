using ChildGTS_Group02.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildGTS_Group02.DAL.Repositories
{
    public class UserRepository
    {
        private ChildGrowthTrackingSystemDBContext? _context;

        public User? GetAccount(string email, string password)
        {
            _context = new ChildGrowthTrackingSystemDBContext();
            return _context.Users
                .FirstOrDefault(u => u.Email == email && u.Password == password);
        }
    }
}
