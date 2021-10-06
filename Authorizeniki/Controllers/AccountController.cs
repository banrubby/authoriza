using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Authorizeniki.Datalayer.Repositories;
using Authorizeniki.Datalayer.Tables;
using Authorizeniki.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace Authorizeniki.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly string homePage = "/";

        public AccountController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public IActionResult Login([FromQuery] string returnUrl)
        {
            return View(new UserLoginModel() {ReturnUrl = returnUrl ?? homePage});
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(UserLoginModel userLoginModel)
        {
            var user = userRepository.GetUserByLogin(userLoginModel.Login);

            if (user == null || user.Password != userLoginModel.Password) return Redirect("/Account/AccessDenied");

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, CreateClaims(user));
            return Redirect(userLoginModel.ReturnUrl ?? homePage);
        }

        public IActionResult AccessDenied([FromQuery] string returnUrl)
        {
            ViewData["returnUrl"] = returnUrl ?? homePage;
            return View();
        }

        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/Account/Login");
        }

        private ClaimsPrincipal CreateClaims(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Login),
                new Claim(ClaimTypes.Role, user.UserRole.Name),
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            return new ClaimsPrincipal(claimsIdentity);
        }
    }
}