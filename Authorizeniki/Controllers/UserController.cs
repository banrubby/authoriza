using System;
using System.Linq;
using Authorizeniki.Datalayer.Repositories;
using Authorizeniki.Datalayer.Tables;
using Authorizeniki.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Authorizeniki.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly IRoleRepository roleRepository;

        public UserController(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
        }

        [Authorize(Roles = "admin")]
        public IActionResult Index()
        {
            var users = userRepository.GetUsers(0, 100);

            return View(new UserPageDto
            {
                UserList = users
            });
        }

        [Authorize(Roles = "admin")]
        public IActionResult Add()
        {
            var selectListItems = roleRepository.GetRoles().Select(item => new SelectListItem()
            {
                Text = item.Name,
                Value = item.Id.ToString()
            }).ToList();
            return View(selectListItems);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult Create([FromForm] UserModel userCreationModel)
        {
            userRepository.Add(new User()
            {
                Login = userCreationModel.Login,
                Password = userCreationModel.Password,
                Surname = userCreationModel.Surname,
                FirstName = userCreationModel.FirstName,
                LastName = userCreationModel.LastName,
                RoleId = Guid.Parse(userCreationModel.SelectedRoleId)
            });

            return Redirect("/User/Add");
        }

        [HttpGet]
        public IActionResult GetUserById([FromQuery] Guid userId)
        {
            var user = userRepository.GetUserById(userId);
            var result = user is null ? (IActionResult) NotFound() : Ok(user);
            return result;
        }
    }
}