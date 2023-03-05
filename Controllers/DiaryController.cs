using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Nyneo_Web.Models;
using Nyneo_Web.Services;

namespace Nyneo_Web.Controllers
{
    // [Route("diary")]
    public class DiaryController : Controller
    {
        private readonly IGoogleCloudService _googleCloudService;
        private readonly IDiaryRepository _diaryRepository;
        private readonly UserManager<User> _userManager;

        public DiaryController(IDiaryRepository diaryRepository, UserManager<User> userManager, IGoogleCloudService googleCloudService)
        {
            _googleCloudService = googleCloudService;
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
                userId = model.userId,
            };

            if (model.ImgFile != null)
            {
                diary.savedImgName = _googleCloudService.GenerateFileNameToSave(model.ImgFile.FileName);
                await _googleCloudService.UploadFileAsync(model.ImgFile, diary.savedImgName);

            }
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
        public IActionResult UpdateDiary(string? diaryId)

        {
            Diary diary = _diaryRepository.GetById(diaryId).Result;

            var model = new UpdateDiaryVM()
            {
                content = diary.content,
                id = diary.Id,
                savedImgName = diary.savedImgName,
                signedUrl = _googleCloudService.GetSignedUrlAsync(diary.savedImgName).Result,
                title = diary.title,
                userId = diary.userId
            };

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateDiary(UpdateDiaryVM updateDiary)
        {

            if (!ModelState.IsValid)
            {
                return View(updateDiary);
            }

            // DTO
            var model = new Diary()
            {
                content = updateDiary.content,
                Id = updateDiary.id,
                title = updateDiary.title,
                userId = updateDiary.userId
            };


            if (!string.IsNullOrWhiteSpace(updateDiary.savedImgName))
            {
                await _googleCloudService.DeleteFileAsync(updateDiary.savedImgName);
            }

            // Update if img is uploaded 
            if (updateDiary.ImgFile != null)
            {

                model.savedImgName = _googleCloudService.GenerateFileNameToSave(updateDiary.ImgFile.FileName);
                await _googleCloudService.UploadFileAsync(updateDiary.ImgFile, model.savedImgName);

            }

            // Update
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