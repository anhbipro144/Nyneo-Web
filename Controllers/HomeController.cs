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
                content = diary.content,
                created_at = diary.created_at,
                title = diary.title,
                userName = user?.UserName
            };
        });



        return View(result);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [Authorize]
    [HttpGet]
    public IActionResult CreateDiary()
    {
        var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var model = new CreateDiary()
        {
            userId = userID
        };
        return View(model);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateDiary(Diary model)
    {

        if (!ModelState.IsValid)
        {
            return View();
        }

        var diary = new Diary()
        {
            content = model.content,
            title = model.title,
            userId = model.userId
        };


        await _diaryRepository.CreateAsync(diary);

        return RedirectToAction("List", "Home");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
