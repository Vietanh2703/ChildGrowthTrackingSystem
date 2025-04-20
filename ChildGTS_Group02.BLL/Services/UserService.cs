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

        public List<User> GetAllUsers()
        {
            return _userRepository.GetAllUsers();
        }
        public List<User> GetAllUsersByRoleId(int roleId)
        {
            return _userRepository.GetAllUsersByRoleId(roleId);
        }
        public List<User> SearchUsersByRoleId(int roleId, string searchText)
        {
            return _userRepository.SearchUsersByRoleId(roleId, searchText.ToLower());
        }

        public User? GetAccount(string email, string password)
        {
            return _userRepository.GetAccount(email, password);
        }

        public User? GetUserById(int userId)
        {
            return _userRepository.GetUserById(userId);
        }

        public bool RegisterUser(string email, string password, string fullName, string phone, string address, bool isTrial, out string errorMessage)
        {
            errorMessage = string.Empty;
            // Check if the email already exists
            if (_userRepository.EmailExists(email))
            {
                errorMessage = "A user with this email already exists.";
                return false;
            }

            // Create a new user object
            var user = new User
            {
                Email = email,
                Password = password,
                FullName = fullName,
                Phone = phone,
                Address = address,
                IsTrial = isTrial,
                RegistrationDate = DateTime.Now,
                RoleId = 3
            };

            // Add user to the database
            return _userRepository.AddUser(user);
        }

        public bool CreateDoctor(string doctorCode, string email, string password, string fullName, string phone, string address, int positionId)
        {
            var user = new User
            {
                DoctorCode = doctorCode,
                Email = email,
                Password = password,
                FullName = fullName,
                Phone = phone,
                Address = address,
                PositionId = positionId,
                RegistrationDate = DateTime.Now,
                RoleId = 2
            };
            return _userRepository.AddUser(user);
        }

        public bool UpdateDoctor(int userId, string doctorCode, string email, string password, string fullName, string phone, string address, int positionId)
        {
            var user = _userRepository.GetUserById(userId);
            if (user != null)
            {
                user.DoctorCode = doctorCode;
                user.Email = email;
                user.Password = password;
                user.FullName = fullName;
                user.Phone = phone;
                user.Address = address;
                user.PositionId = positionId;
            }
            return _userRepository.UpdateUser(user);
        }

        public void DeleteDoctor(int userId)
        {
            var user = _userRepository.GetUserById(userId);
            if (user != null)
            {
                _userRepository.DeleteUser(user);
            }
        }


        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _userRepository.GetUserByIdAsync(userId);
        }
    }
}
