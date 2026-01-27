using InsuranceAdministration.Core.Entities.Settings;
using InsuranceAdministration.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace InsuranceAdministration.Controllers
{
    public class AdminController : Controller
    {
   
        private readonly ISettingsServices _settingsOptionsService;
        private readonly IMissionServices _missionServices;


        public AdminController(
           ISettingsServices settingsOptionsService, 
           IMissionServices missionServices
        )
        {
            _settingsOptionsService = settingsOptionsService;
            _missionServices = missionServices;
        }

        /* ===================== Soldier ===================== */

        public async Task<IActionResult> Main()
        {
            var mainSettings = await _settingsOptionsService.GetAllMainSettings();
            var model = mainSettings?.FirstOrDefault();

            if (model == null)
            {
                // Create new settings if none exist
                model = new MainSettings();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateMainSettings(MainSettings mainSetting)
        {
            if (!ModelState.IsValid)
                return View("Main", mainSetting);

            var existingSettings = await _settingsOptionsService.GetAllMainSettings();

            if (existingSettings == null || !existingSettings.Any())
            {
                // Add new settings if none exist
                await _settingsOptionsService.AddNewMainSettings(mainSetting);
            }
            else
            {
                // Update existing settings
                await _settingsOptionsService.UpdateMainSettings(mainSetting);
            }

            TempData["SuccessMessage"] = "تم حفظ التعديلات بنجاح";
            return RedirectToAction(nameof(Main));
        }

        /* ===================== Soldier ===================== */

        public async Task<IActionResult> Soldier()
        {
            ViewBag.AssignmentOptions =
                await _settingsOptionsService.GetAllAssignmentOptions();

            ViewBag.EducationLevelOptions =
                await _settingsOptionsService.GetAllEducationLevelOptions();
            ViewBag.SoldierLeaveOption =
               await _settingsOptionsService.GetAllSoldierLeaveOptions();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddAssignmentOption(AssignmentOptions option)
        {
            if (!ModelState.IsValid)
                return RedirectToAction(nameof(Soldier));

            await _settingsOptionsService.AddNewAssignmentOption(option);
            return RedirectToAction(nameof(Soldier));
        }

        [HttpPost]
        public async Task<IActionResult> AddEducationLevelOption(EducationLevelOptions option)
        {
            if (!ModelState.IsValid)
                return RedirectToAction(nameof(Soldier));

            await _settingsOptionsService.AddNewEducationLevelOption(option);
            return RedirectToAction(nameof(Soldier));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAssignmentOption(int id)
        {
            await _settingsOptionsService.DeleteAssignmentOption(id);
            return RedirectToAction(nameof(Soldier));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteEducationLevelOption(int id)
        {
            await _settingsOptionsService.DeleteEducationLevelOption(id);
            return RedirectToAction(nameof(Soldier));
        }


        /* ===================== Mission ===================== */

        public async Task<IActionResult> Mission()
        {
            ViewBag.Missions = await _missionServices.GetAllMissions();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNewMission(Mission mission)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "البيانات المدخلة غير صحيحة";
                return RedirectToAction(nameof(Mission));
            }

            try
            {
                await _missionServices.AddNewMission(mission);
                TempData["SuccessMessage"] = "تم إضافة الخدمة بنجاح";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "حدث خطأ أثناء إضافة الخدمة";
            }

            return RedirectToAction(nameof(Mission));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteMission(int id)
        {
            try
            {
                var result = await _missionServices.DeleteMission(id);

                if (result != null)
                {
                    TempData["SuccessMessage"] = "تم حذف الخدمة بنجاح";
                }
                else
                {
                    TempData["ErrorMessage"] = "لم يتم العثور على الخدمة";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "حدث خطأ أثناء حذف الخدمة";
            }

            return RedirectToAction(nameof(Mission));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateMission(Mission mission)
        {
            if (!ModelState.IsValid)
            {
                // Log validation errors
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Model Error: {error.ErrorMessage}");
                }
                TempData["ErrorMessage"] = "البيانات المدخلة غير صحيحة";
                return RedirectToAction(nameof(Mission));
            }

            try
            {
                await _missionServices.UpdateCurrentMission(mission);
                TempData["SuccessMessage"] = "تم تحديث الخدمة بنجاح";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Update Error: {ex.Message}");
                TempData["ErrorMessage"] = "حدث خطأ أثناء تحديث الخدمة";
            }

            return RedirectToAction(nameof(Mission));
        }

        [HttpPost]
        public async Task<IActionResult> AddSoldierLeaveOption(SoldierLeaveOptions option)
        {
            if (!ModelState.IsValid)
                return RedirectToAction(nameof(Soldier));

            await _settingsOptionsService.AddNewSoldierLeaveOptions(option);
            return RedirectToAction(nameof(Soldier));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSoldierLeaveOption(SoldierLeaveOptions option)
        {
            if (!ModelState.IsValid)
                return RedirectToAction(nameof(Soldier));

            await _settingsOptionsService.UpdateCurrentSoldierLeaveOptions(option);
            return RedirectToAction(nameof(Soldier));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSoldierLeaveOption(int id)
        {
            await _settingsOptionsService.DeleteSoldierLeaveOptions(id);
            return RedirectToAction(nameof(Soldier));
        }
        // ===================== Daily Meal ===================== //

        // ===================== Daily Meal ===================== //
        [HttpGet]
        public async Task<IActionResult> DailyMeal()
        {
            ViewBag.DailyMealOptions = await _settingsOptionsService.GetAllDailyMealOptions();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddDailyMealOption(DailyMealOptions dailyMeal)
        {
            if (ModelState.IsValid)
            {
                await _settingsOptionsService.AddNewDailyMealOptions(dailyMeal);
                TempData["SuccessMessage"] = "تم إضافة الخيار بنجاح";
            }
            else
            {
                TempData["ErrorMessage"] = "حدث خطأ أثناء الإضافة";
            }

            return RedirectToAction(nameof(DailyMeal));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteDailyMealOption(int id)
        {
            var result = await _settingsOptionsService.DeleteDailyMealOptions(id);

            return RedirectToAction(nameof(DailyMeal));
        }
    }
}
