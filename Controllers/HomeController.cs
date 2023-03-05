using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Nyneo_Web.Models;
using Nyneo_Web.Services;
using Nyneo_Web.Services.Implementations;

namespace Nyneo_Web.Controllers;

public class HomeController : Controller
{

    private readonly IDiaryRepository _diaryRepository;
    private IGoogleCloudService _googleCloudService;
    private UserManager<User> _userManager;


    public HomeController(IDiaryRepository diaryRepository, UserManager<User> userManager, IGoogleCloudService googleCloudService)

    {
        _googleCloudService = googleCloudService;
        _userManager = userManager;
        _diaryRepository = diaryRepository;
    }


    public IActionResult Index() => View();

    [Authorize]
    public IActionResult List()
    {
        IEnumerable<IndexDiaryVM> result = _diaryRepository.GetAll().Result.Select(diary =>
            {
                var user = _userManager.FindByIdAsync(diary.userId).Result;

                var diaryVM = new IndexDiaryVM()
                {
                    id = diary.Id,
                    content = diary.content,
                    created_at = diary.created_at,
                    title = diary.title,
                    userName = user?.UserName,
                    userId = diary.userId,

                };

                if (!string.IsNullOrWhiteSpace(diary.savedImgName))
                {
                    diaryVM.signedUrl = _googleCloudService.GetSignedUrlAsync(diary.savedImgName).Result;

                }

                return diaryVM;
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
