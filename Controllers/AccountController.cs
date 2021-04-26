using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mvcEntities.CustomModel;
using mvcEntities.Entities;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;


namespace FoodOrderingApp.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly IFoodOrderingComponent _foodOrderingComponent;
        public AccountController(IFoodOrderingComponent foodOrderingComponent)
        {
            _foodOrderingComponent = foodOrderingComponent;
        }
        public IActionResult Login()
        {
            var user = HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme).Result;
            if (user.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            return View("Login", new Login());
        }
        [HttpPost]
        public async Task<IActionResult> Login(Login login)
        {

            if (ModelState.IsValid)
            {
                bool isValid = _foodOrderingComponent.Login(login);
                if (isValid)
                {
                    var user = _foodOrderingComponent.GetUserDetailByUserName(login.UserName);
                    var Claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.Role, user.RoleId.ToString()),
                        new Claim("LoggedInUserId", user.UserId.ToString())
                    };
                    var claimsIdentity = new ClaimsIdentity(Claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimsPrinciple = new ClaimsPrincipal(claimsIdentity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrinciple, new AuthenticationProperties
                    {
                        IsPersistent = login.RememberMe
                    });
                    return RedirectToAction("Index", "Home");
                }
            }
            ModelState.AddModelError("", "Invalid username or password");
            return View();
        }
        public IActionResult Signup()
        {
            List<UserRole> roles = _foodOrderingComponent.GetRole();

            List<string> role = new List<string>();

            foreach (var item in roles)
            {
                role.Add(item.RoleName);
            }
            ViewBag.UserRole = role;
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(Registration registration)
        {
            if (ModelState.IsValid)
            {
                var res=_foodOrderingComponent.Register(registration);
                if(res==2)
                {
                    TempData["Error"] = "User Already Exists";
                    return RedirectToAction("SignUp");
                }
                return RedirectToAction("Login");
            }
            else
            {
                return View();
            }
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
