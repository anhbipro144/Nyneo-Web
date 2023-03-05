using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Nyneo_Web.Models;

namespace Nyneo_Web.Controllers;

public class RoleController : Controller
{
    private UserManager<User> _userManager;
    private RoleManager<Role> _roleManager;

    public RoleController(RoleManager<Role> roleManager, UserManager<User> userManager)

    {
        _userManager = userManager;
        _roleManager = roleManager;
    }


    public IActionResult Index() => View(_roleManager.Roles);


    #region Update User Role
    [HttpGet]
    public async Task<IActionResult> UpdateUserRole(string userId)
    {

        var user = await _userManager.FindByIdAsync(userId);
        var model = new UpdateUserRoleVM()
        {
            roleList = _roleManager.Roles,
            user = user
        };

        return View(model);
    }


    [HttpPost]
    public async Task<IActionResult> UpdateUserRole(string? roleName, string? userId)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        var user = await _userManager.FindByIdAsync(userId);



        if (user != null)
        {
            await _userManager.AddToRoleAsync(user, roleName);
            ViewBag.Message = "Update user role successful!";

            return RedirectToAction("Index", "User");
        }

        ViewBag.Message = "Update user role failed!";

        return View();
    }

    #endregion





    #region Add Role
    [HttpGet]
    public IActionResult AddRole() => View();

    [HttpPost]
    public async Task<IActionResult> AddRole(string name)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        IdentityResult result = await _roleManager.CreateAsync(new Role() { Name = name });
        if (result.Succeeded)
            ViewBag.Message = "Role Created Successfully";
        else
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }
        return RedirectToAction("Index", "Home");
    }
    #endregion


}
