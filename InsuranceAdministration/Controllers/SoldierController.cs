using InsuranceAdministration.Core.Entities.SoldierEntities;
using InsuranceAdministration.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace InsuranceAdministration.Controllers
{
    public class SoldierController : Controller
    {
        private readonly ISoldierServices _soldierService;
        private readonly ISettingsServices _settingsOptionsService;


        public SoldierController(
            ISoldierServices soldierService,
            ISettingsServices settingsOptionsService
            )
        {
            _soldierService = soldierService;
            _settingsOptionsService = settingsOptionsService;
        }

        // GET: List of soldiers with pagination
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var soldiers = (await _soldierService.GetPaginatedSoldiersByActive(pageNumber, pageSize + 1, true)).ToList();
            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            ViewBag.HasNext = soldiers.Count > pageSize;

            return View(soldiers.Take(pageSize));
        }

        // GET: Show form to add a new soldier
        public async Task<IActionResult> Add()
        {
            var assignmentOptions = await _settingsOptionsService.GetAllAssignmentOptions();

            var educationLevelOptions = await _settingsOptionsService.GetAllEducationLevelOptions();

            ViewBag.AssignmentOptions = new SelectList(assignmentOptions, "Name", "Name");
            ViewBag.educationLevelOptions = new SelectList(educationLevelOptions, "Name", "Name");

            return View();
        }

        // POST: Add a new soldier
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNewSoldierItem(Soldier soldier)
        {
            if (!ModelState.IsValid)
                return View("AddNewSoldierView", soldier);

            await _soldierService.AddNewSoldier(soldier);
            return RedirectToAction(nameof(Index));
        }

        // GET: Show form to edit an existing soldier
        public async Task<IActionResult> Edit(int id)
        {
            var soldier = await _soldierService.GetSoldier(id);
            if (soldier == null)
                return NotFound();

            return View(soldier);
        }

        // POST: Update an existing soldier
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Soldier soldier)
        {
            if (!ModelState.IsValid)
                return View(soldier);

            await _soldierService.UpdateCurrentSoldier(soldier);
            return RedirectToAction(nameof(Index));
        }

        // GET: Show soldier details
        public async Task<IActionResult> Show(int id)
        {
            var soldier = await _soldierService.GetSoldier(id);
            if (soldier == null)
                return NotFound();

            return View(soldier);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var soldier = await _soldierService.DeleteSoldier(id);
            if (soldier == null)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }

    }
}
