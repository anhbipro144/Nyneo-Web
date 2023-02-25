using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Nyneo_Web.Models;
using Nyneo_Web.Services.Implementations;

namespace Nyneo_Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private readonly DiaryRepositoryService _diaryRepository;
    private UserManager<User> _userManager;


    public HomeController(ILogger<HomeController> logger, DiaryRepositoryService diaryRepository, UserManager<User> userManager)

    {
        _userManager = userManager;
        _diaryRepository = diaryRepository;
        _logger = logger;
    }


    public IActionResult Index() => View();

    [Authorize]
    public IActionResult List()
    {
        IEnumerable<IndexDiaryVM> result = _diaryRepository.GetAll().Result.Select(diary =>
        {
            var user = _userManager.FindByIdAsync(diary.userId).Result;

            return new IndexDiaryVM()
            {
                id = diary.Id,
                content = diary.content,
                created_at = diary.created_at,
                title = diary.title,
                userName = user?.UserName,
                userId = diary.userId

            };
        });



        return View(result);
    }

    public IActionResult Privacy()
    {
        return View();
    }



    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
