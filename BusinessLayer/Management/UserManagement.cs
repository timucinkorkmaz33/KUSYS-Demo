using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using DataAccessLayer.Repositories;
using EntityLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Management
{
    public class UserManagement : IUserService
    {
        IUserDal _userDal;

        public UserManagement(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public void AddUser(User user)
        {
            _userDal.Add(user);
        }

        public void DeleteUser(User user)
        {
            _userDal.Delete(user);
        }

        public List<User> GetAllUsers()
        {
            return _userDal.GetList();
        }

        public User GetUserById(int id)
        {
            return _userDal.GetById(id);
        }

        public void UpdateUser(User user)
        {
            _userDal.Update(user);
        }
    }
}
