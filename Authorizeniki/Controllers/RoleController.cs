using Authorizeniki.Datalayer.Repositories;
using Authorizeniki.Datalayer.Tables;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Authorizeniki.Controllers
{
    public class RoleController : Controller
    {
        private readonly IRoleRepository roleRepository;

        public RoleController(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult Create([FromForm] Role role)
        {
            roleRepository.Add(new Role()
            {
                Name = role.Name,
                Salary = role.Salary
            });

            return Redirect("/Role/Add");
        }
        
        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult Add()
        {
            return View();
        }
    }
}