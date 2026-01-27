using InsuranceAdministration.Core.DTOs.Soldiers;
using InsuranceAdministration.Core.Interfaces.Services;
using InsuranceAdministration.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace InsuranceAdministration.Controllers
{
    public class MissionController : Controller
    {
        private readonly IMissionServices _missionService;
        private readonly ISettingsServices _settingsOptionsService;
        public MissionController(IMissionServices missionService, ISettingsServices settingsOptionsService)
        {
            _missionService = missionService;
            _settingsOptionsService = settingsOptionsService;
        }

        // GET: MissionController
        public IActionResult Index()
        {
            return View();
        }

        // GET: MissionController/Details/5
        public async Task<IActionResult> Details()
        {
            var missions = await _missionService.GetAllMissions();
            return Content(string.Join(", ", missions.Select(m => m.Name)));
        }


        // GET: MissionController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Mission mission)
        {
            if (!ModelState.IsValid)
                return View("Index", mission);

            await _missionService.AddNewMission(mission);
            return RedirectToAction(nameof(Index));
        }  // POST: MissionController/Create


        // GET: MissionController/Edit/5
        public IActionResult Edit(int id)
        {
            return View();
        }

        // POST: MissionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MissionController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MissionController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> SoldierDailyMission()
        {
            var missions = await _missionService.GetAllDailyMissions();
            return View(missions);
        }

        // Updated Controller Method
        [HttpGet]
        public async Task<IActionResult> ShowDailyMission(int id)
        {
            var dailyMission = await _missionService.GetDailyMission(id);

            // Filter missions NOT of type "مسطح مائى" (MissionType != 1)
            var soldierMissionNotRiver = dailyMission.SoldierMissions
                .Where(m => m.Mission.MissionType != 1)
                .Select(sm => new
                {
                    sm.Soldier.Id,
                    sm.Soldier.Name,
                    MissionName = sm.Mission.Name,
                    sm.Notes
                })
                .ToList();

            // Filter missions of type "مسطح مائى" (MissionType == 1)
            var soldierMissionRiver = dailyMission.SoldierMissions
                .Where(m => m.Mission.MissionType == 1)
                .Select(sm => new
                {
                    sm.Soldier.Id,
                    sm.Soldier.Name,
                    MissionName = sm.Mission.Name,
                    sm.Notes
                })
                .ToList();

            // ViewBag data (set once, not twice)
            ViewBag.DepartmentName = await _settingsOptionsService.GetMainSettingsByDepartmentName();
            ViewBag.DepartmentDirectorName = await _settingsOptionsService.GetMainSettingsByDepartmentDirectorName();
            ViewBag.SoldierMissionNotRiver = soldierMissionNotRiver;
            ViewBag.SoldierMissionRiver = soldierMissionRiver;
            ViewBag.MissionDate = dailyMission.Date;

            return View();
        }

    }
}
