
using InsuranceAdministration.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceAdministration.Controllers
{
    public class SoldierLeaveController : Controller
    {
        private readonly ISoldierServices _soldierService;
        private readonly ISettingsServices _settingsOptionsService;
        private readonly ILogger<SoldierLeaveController> _logger;

        public SoldierLeaveController(
            ISoldierServices soldierService,
            ISettingsServices settingsOptionsService,
            ILogger<SoldierLeaveController> logger)
        {
            _soldierService = soldierService;
            _settingsOptionsService = settingsOptionsService;
            _logger = logger;
        }
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var soldiers = (await _soldierService.GetPaginatedSoldiersByActive(pageNumber, pageSize + 1, true)).ToList();
            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            ViewBag.HasNext = soldiers.Count > pageSize;
            return View(soldiers.Take(pageSize));
        }
        public async Task<IActionResult> Leave()
        {
           
            return View();
        }
    }
}
