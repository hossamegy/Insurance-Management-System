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
            ViewBag.Missions =
               await _missionServices.GetAllMissions();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNewMission(Mission mission)
        {
            if (!ModelState.IsValid)
                return RedirectToAction(nameof(Mission));

            await _missionServices.AddNewMission(mission);
            return RedirectToAction(nameof(Mission));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteMission(int id)
        {

            if (!ModelState.IsValid)
                return RedirectToAction(nameof(Mission));
            await _missionServices.DeleteMission(id);
            return RedirectToAction(nameof(Mission));
        }
        
        [HttpPost]
        public async Task<IActionResult> UpdateMission(Mission mission)
        {
            if (!ModelState.IsValid)
                return RedirectToAction(nameof(Mission));
            await _missionServices.UpdateCurrentMission(mission);
            return RedirectToAction(nameof(Mission));
        }
    }
}
