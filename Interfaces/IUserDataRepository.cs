using demo_app_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace demo_app_API.Interfaces
{
    public interface IUserDataRepository
    {
        IEnumerable<UserModel> GetAllUsers();
        UserModel GetUserById(int id);
        UserModel AddUser(UserModel user);

        void UpdateUser(UserModel user);
        void DeletUser(int id);
    }
}
