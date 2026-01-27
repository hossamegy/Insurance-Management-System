using InsuranceAdministration.Core.Entities.PoliceManEntities;
using InsuranceAdministration.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;


namespace InsuranceAdministration.Controllers
{
    public class PoliceManController : Controller
    {
        private readonly IPoliceManServices _services;
        private readonly ISettingsServices _settingsOptionsService;

        public PoliceManController(
           IPoliceManServices services,
           ISettingsServices settingsOptionsService
         )
        {
            _services = services;
            _settingsOptionsService = settingsOptionsService;
        }
        public async Task<IActionResult> Index(int pageNumber=1, int pageSize=10)
        {
            var soldiers = (await _services.GetPaginatedPoliceMan(pageNumber, pageSize + 1)).ToList();

            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            ViewBag.HasNext = soldiers.Count > pageSize;

            return View(soldiers.Take(pageSize));
        }

        // GET: Show form to add a new soldier
        public async Task<IActionResult> Add()
        {
        
            return View();
        }
        // POST: Add a new soldier
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNewPoliceManItem(PoliceMan policeMan)
        {
            if (!ModelState.IsValid)
                return View("AddNewPoliceManView", policeMan);

            await _services.AddNewPoliceMan(policeMan);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var soldier = await _services.DeletePoliceMan(id);
            if (soldier == null)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Edit(int id)
        {
           
            var policeMan = await _services.GetPoliceMan(id);
            if (policeMan == null)
                return NotFound();

            return View(policeMan);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PoliceMan policeMan)
        {
            if (!ModelState.IsValid)
                return View(policeMan);

            await _services.UpdateCurrentPoliceMan(policeMan);
      
            return RedirectToAction(nameof(Index));
        }

    }
}
