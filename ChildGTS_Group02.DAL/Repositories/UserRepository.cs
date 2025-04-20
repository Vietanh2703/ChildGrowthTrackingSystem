using ChildGTS_Group02.DAL.Entities;
using Microsoft.EntityFrameworkCore;
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

        public List<User> GetAllUsers()
        {
            _context = new ChildGrowthTrackingSystemDBContext();
            return _context.Users.ToList();
        }

        public List<User> GetAllUsersByRoleId(int roleId)
        {
            _context = new ChildGrowthTrackingSystemDBContext();
            return _context.Users
                .Where(u => u.RoleId == roleId).Include(u => u.Position)
                .ToList();
        }


        public List<User> SearchUsersByRoleId(int roleId, string searchText)
        {
            _context = new ChildGrowthTrackingSystemDBContext();
            return _context.Users
                .Where(u => u.RoleId == roleId &&
                            (u.FullName.ToLower().Contains(searchText) ||
                             u.Email.ToLower().Contains(searchText) ||
                             u.Phone.Contains(searchText)))
                .ToList();
        }

        public User? GetAccount(string email, string password)
        {
            _context = new ChildGrowthTrackingSystemDBContext();
            return _context.Users
                .FirstOrDefault(u => u.Email == email && u.Password == password);
        }

        public User? GetUserById(int userId)
        {
            _context = new ChildGrowthTrackingSystemDBContext();
            return _context.Users
                .FirstOrDefault(u => u.UserId == userId);
        }

        public bool AddUser(User user)
        {
            _context = new ChildGrowthTrackingSystemDBContext();
            _context.Users.Add(user);
            return _context.SaveChanges() > 0;
        }

        public bool UpdateUser(User user)
        {
            _context = new ChildGrowthTrackingSystemDBContext();
            _context.Users.Update(user);
            return _context.SaveChanges() > 0;
        }

        public void DeleteUser(User user)
        {
            _context = new ChildGrowthTrackingSystemDBContext();
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public bool EmailExists(string email)
        {
            _context = new ChildGrowthTrackingSystemDBContext();
            return _context.Users.Any(u => u.Email == email);
        }

      

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
        }

    }
}
