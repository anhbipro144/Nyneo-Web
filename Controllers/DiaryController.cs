using Microsoft.AspNetCore.Mvc;
using Nyneo_Web.Models;
using Nyneo_Web.Services.Implementations;

namespace Nyneo_Web.Controllers
{
    public class DiaryController : Controller
    {
        private readonly DiaryRepositoryService _diaryService;

        public DiaryController(DiaryRepositoryService diaryService) =>
            _diaryService = diaryService;


        [HttpGet]
        public IActionResult CreateDiary()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateDiary(Diary model)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            return View();
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}