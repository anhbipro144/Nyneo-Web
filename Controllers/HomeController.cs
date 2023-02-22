using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nyneo_Web.Models;
using Nyneo_Web.Services.Implementations;

namespace Nyneo_Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private readonly DiaryRepositoryService _diaryRepository;

    public HomeController(ILogger<HomeController> logger, DiaryRepositoryService diaryRepository)
    {
        _diaryRepository = diaryRepository;
        _logger = logger;
    }


    public IActionResult Index() => View();

    [Authorize]
    public IActionResult List()
    {
        IEnumerable<Diary> result = _diaryRepository.GetAll().Result;
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
        return View();
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateDiary(Diary diary)
    {

        if (!ModelState.IsValid)
        {
            return View();
        }

        await _diaryRepository.CreateAsync(diary);

        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
