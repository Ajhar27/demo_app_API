using demo_app_API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace demo_app_API.Controllers
{
    
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserDataRepository userdataRepository;
        public UserController(IUserDataRepository userRepo)
        {
            userdataRepository = userRepo;
        }

        [HttpPost]
        [Route("addUserDetails")]
        public UserModel AddUser([FromBody] UserModel user)
        {
            return userdataRepository.AddUser(user);
        }

        [HttpGet]
        [Route("GetUsers")]
        public IEnumerable<UserModel> GetUsers()
        {
            return userdataRepository.GetAllUsers().ToList();
        }

        [HttpGet]
        [Route("GetUserByID")]
        public UserModel GetUserByID(int id)
        {
            return userdataRepository.GetUserById(id);
        }

        [HttpPost]
        [Route("DeleteUser")]
        public void DeleteDept(int id)
        {
            userdataRepository.DeletUser(id);
        }
        [HttpPost]
        [Route("UpdateUser")]
        public void UpdateUser(UserModel user)
        {
            userdataRepository.UpdateUser(user);
        }
    }
}
