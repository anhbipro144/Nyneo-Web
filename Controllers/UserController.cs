using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Nyneo_Web.Models;

namespace Nyneo_Web.Controllers;

public class UserController : Controller
{

    private UserManager<User> _userManager;

    public UserController(UserManager<User> userManager)

    {
        _userManager = userManager;
    }


    public IActionResult Index() => View(_userManager.Users);


}
