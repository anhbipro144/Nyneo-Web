using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Nyneo_Web.Models;

namespace Nyneo_Web.Controllers
{
    public class OperationsController : Controller
    {

        private UserManager<User> _userManager;
        private RoleManager<Role> _roleManager;
        private SignInManager<User> _signInManager;

        public OperationsController(UserManager<User> userManager, RoleManager<Role> roleManager, SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
            _roleManager = roleManager;
            _userManager = userManager;
        }


        #region Register
        [HttpGet]
        public ViewResult Register() => View();

        // [HttpPost]
        public async Task<IActionResult> RegisterAdmin()
        {

            var admin = new User
            {
                Email = "admin@gmail.com",
                UserName = "admin",
            };

            IdentityResult result = await _userManager.CreateAsync(admin, "admin123");

            if (result.Succeeded)
                ViewBag.Message = "User Created Successfully";
            else
            {
                foreach (IdentityError error in result.Errors)
                    ModelState.AddModelError("", error.Description);
            }

            await _userManager.AddToRoleAsync(admin, "Admin");


            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUser model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = new User
            {
                Email = model.Email,
                UserName = model.Username,
            };

            IdentityResult result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
                ViewBag.Message = "User Created Successfully";
            else
            {
                foreach (IdentityError error in result.Errors)
                    ModelState.AddModelError("", error.Description);
            }

            await _userManager.AddToRoleAsync(user, "User");


            return RedirectToAction("Index", "Home");
        }
        #endregion


        #region Login
        [HttpGet]
        public ViewResult Login(string ReturnUrl)
        {
            var model = new LoginUser()
            {
                returnUrl = ReturnUrl
            };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginUser model)
        {
            if (ModelState.IsValid)
            {
                User appUser = await _userManager.FindByEmailAsync(model.Email);
                if (appUser != null)
                {
                    Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(appUser, model.Password, false, false);
                    if (result.Succeeded)
                    {
                        return Redirect(model.returnUrl ?? "/");
                    }
                }
                ModelState.AddModelError(nameof(model.Email), "Login Failed: Invalid Email or Password");
            }

            return View();
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        #endregion





        [Authorize]
        public IActionResult Authenticated()
        {
            return View();
        }
    }
}