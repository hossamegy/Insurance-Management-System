using InsuranceAdministration.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceAdministration.Controllers
{
    public class MissionController : Controller
    {
        private readonly IMissionServices _missionService;
        public MissionController(IMissionServices missionService)
        {
            _missionService = missionService;
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
    }
}
