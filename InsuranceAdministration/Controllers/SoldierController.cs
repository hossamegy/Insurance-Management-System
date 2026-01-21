// ============================================
// SoldierController.cs - FINAL CORRECTED VERSION
// ============================================
using InsuranceAdministration.Core.Entities.SoldierEntities;
using InsuranceAdministration.Core.Entities.SoldierEntities.Acquaintance;
using InsuranceAdministration.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InsuranceAdministration.Controllers
{
    public class SoldierController : Controller
    {
        private readonly ISoldierServices _soldierService;
        private readonly ISettingsServices _settingsOptionsService;
        private readonly ILogger<SoldierController> _logger;

        public SoldierController(
            ISoldierServices soldierService,
            ISettingsServices settingsOptionsService,
            ILogger<SoldierController> logger)
        {
            _soldierService = soldierService;
            _settingsOptionsService = settingsOptionsService;
            _logger = logger;
        }

        [Route("api/soldier/acquaintance/{id}")]
        public async Task<IActionResult> get(int id)
        {
            var soldier = await _soldierService.GetSoldier(id);
            return Ok(soldier);
        }

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var soldiers = (await _soldierService.GetPaginatedSoldiersByActive(pageNumber, pageSize + 1, true)).ToList();
            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            ViewBag.HasNext = soldiers.Count > pageSize;
            return View(soldiers.Take(pageSize));
        }

        public async Task<IActionResult> Add()
        {
            var assignmentOptions = await _settingsOptionsService.GetAllAssignmentOptions();
            var educationLevelOptions = await _settingsOptionsService.GetAllEducationLevelOptions();
            ViewBag.AssignmentOptions = new SelectList(assignmentOptions, "Name", "Name");
            ViewBag.EducationLevelOptions = new SelectList(educationLevelOptions, "Name", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNewSoldierItem(Soldier soldier)
        {
            if (!ModelState.IsValid)
            {
                var assignmentOptions = await _settingsOptionsService.GetAllAssignmentOptions();
                var educationLevelOptions = await _settingsOptionsService.GetAllEducationLevelOptions();
                ViewBag.AssignmentOptions = new SelectList(assignmentOptions, "Name", "Name");
                ViewBag.EducationLevelOptions = new SelectList(educationLevelOptions, "Name", "Name");
                return View("Add", soldier);
            }
            await _soldierService.AddNewSoldier(soldier);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var soldier = await _soldierService.GetSoldier(id);
            if (soldier == null) return NotFound();
            var assignmentOptions = await _settingsOptionsService.GetAllAssignmentOptions();
            var educationLevelOptions = await _settingsOptionsService.GetAllEducationLevelOptions();
            ViewBag.AssignmentOptions = new SelectList(assignmentOptions, "Name", "Name", soldier.Assignment);
            ViewBag.EducationLevelOptions = new SelectList(educationLevelOptions, "Name", "Name", soldier.EducationLevel);
            return View(soldier);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Soldier soldier)
        {
            if (!ModelState.IsValid)
            {
                var assignmentOptions = await _settingsOptionsService.GetAllAssignmentOptions();
                var educationLevelOptions = await _settingsOptionsService.GetAllEducationLevelOptions();
                ViewBag.AssignmentOptions = new SelectList(assignmentOptions, "Name", "Name", soldier.Assignment);
                ViewBag.EducationLevelOptions = new SelectList(educationLevelOptions, "Name", "Name", soldier.EducationLevel);
                return View(soldier);
            }
            await _soldierService.UpdateCurrentSoldier(soldier);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Show(int id)
        {
            var soldier = await _soldierService.GetSoldier(id);
            if (soldier == null) return NotFound();
            return View(soldier);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var soldier = await _soldierService.DeleteSoldier(id);
            if (soldier == null) return NotFound();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Ornik114(int pageNumber = 1, int pageSize = 10)
        {
            var soldiers = (await _soldierService.GetPaginatedSoldiersByActive(pageNumber, pageSize + 1, true)).ToList();
            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            ViewBag.HasNext = soldiers.Count > pageSize;
            return View(soldiers.Take(pageSize));
        }
        

        public async Task<IActionResult> PrintOrnik114()
        {
        
            return View();
        }
    }
}
