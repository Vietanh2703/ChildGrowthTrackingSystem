using ChildGTS_Group02.DAL.Entities;
using ChildGTS_Group02.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildGTS_Group02.BLL.Services
{
    public class UserService
    {
        private UserRepository _userRepository = new UserRepository();

        public User? GetAccount(string email, string password)
        {
            return _userRepository.GetAccount(email, password);
        }
    }
}
