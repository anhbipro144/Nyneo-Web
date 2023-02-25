using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Nyneo_Web.Models;
using Nyneo_Web.Services.Implementations;

namespace Nyneo_Web.Controllers
{
    // [Route("diary")]
    public class DiaryController : Controller
    {
        private readonly DiaryRepositoryService _diaryRepository;
        private readonly UserManager<User> _userManager;

        public DiaryController(DiaryRepositoryService diaryRepository, UserManager<User> userManager)
        {
            _diaryRepository = diaryRepository;
            _userManager = userManager;
        }


        #region Add
        [Authorize]
        [HttpGet("diary/add")]
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
        [HttpPost("diary/add")]
        public async Task<IActionResult> CreateDiary(CreateDiary model)
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


            await _diaryRepository.AddAsync(diary);

            return RedirectToAction("List", "Home");
        }

        #endregion

        #region  Delete


        [HttpPost]
        public async Task<IActionResult> Delete(string diaryId)
        {
            await _diaryRepository.DeleteAsync(diaryId);

            return RedirectToAction("List", "Home");
        }
        #endregion

        #region Update

        [HttpGet]
        public IActionResult UpdateDiary(string diaryId)

        {
            Diary model = _diaryRepository.GetById(diaryId).Result;

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateDiary(Diary model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            await _diaryRepository.UpdateAsync(model);


            return RedirectToAction("List", "Home");
        }

        #endregion


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}