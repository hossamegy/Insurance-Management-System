using InsuranceAdministration.Core.Entities.SoldierEntities;
using InsuranceAdministration.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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

        [HttpGet]
        public async Task<IActionResult> Leave(int soldierId, string actionType, string soldierName)
        {
            try
            {
                var soldier = await _soldierService.GetSoldier(soldierId);

                var model = new SoldierLeave
                {
                    SoldierId = soldierId,
                    Type = actionType
                };

                ViewBag.ActionType = actionType;
                ViewBag.SoldierName = soldierName ?? soldier.Name;

                return View(model);
            }
            catch (KeyNotFoundException)
            {
                _logger.LogWarning("Soldier with ID {SoldierId} not found.", soldierId);
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LeaveConfirmation(SoldierLeave soldierLeave)
        {
            if (!ModelState.IsValid)
            {
                try
                {
                    var soldier = await _soldierService.GetSoldier(soldierLeave.SoldierId);
                    ViewBag.SoldierName = soldier.Name;
                }
                catch
                {
                    ViewBag.SoldierName = "";
                }

                ViewBag.ActionType = soldierLeave.Type;
                return View("Leave", soldierLeave);
            }

            try
            {
                var addedLeave = await _soldierService.AddSoldierLeave(soldierLeave);

                var soldier = await _soldierService.GetSoldier(soldierLeave.SoldierId);
                soldier.CurrentIsLeave = true;
                await _soldierService.UpdateCurrentSoldier(soldier);

                _logger.LogInformation("Leave added successfully for Soldier ID {SoldierId}", soldierLeave.SoldierId);

                return Content("<script>window.opener.location.reload(); window.close();</script>", "text/html");
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Soldier with ID {SoldierId} not found.", soldierLeave.SoldierId);
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding leave for Soldier ID {SoldierId}", soldierLeave.SoldierId);
                ModelState.AddModelError("", "حدث خطأ أثناء إضافة الإجازة");

                try
                {
                    var soldier = await _soldierService.GetSoldier(soldierLeave.SoldierId);
                    ViewBag.SoldierName = soldier.Name;
                }
                catch
                {
                    ViewBag.SoldierName = "";
                }

                ViewBag.ActionType = soldierLeave.Type;
                return View("Leave", soldierLeave);
            }
        }

        [HttpGet]
        public async Task<IActionResult> ShowSoldierLeaves(int soldierId)
        {
            try
            {
                var soldier = await _soldierService.GetSoldier(soldierId);
                return View(soldier);
            }
            catch (KeyNotFoundException)
            {
                _logger.LogWarning("Soldier with ID {SoldierId} not found.", soldierId);
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Attendance(int soldierId, string actionType, string soldierName)
        {
            try
            {
                var soldier = await _soldierService.GetSoldier(soldierId);
                var lastSoldierLeave = await _soldierService.GetLastSoldierLeave(soldierId);

                // التحقق من وجود إجازة سابقة
                if (lastSoldierLeave == null)
                {
                    _logger.LogWarning("No leave found for soldier ID {SoldierId}", soldierId);
                    TempData["ErrorMessage"] = "لا توجد إجازة سابقة لهذا المجند";
                    return RedirectToAction("Index");
                }

                ViewBag.ActionType = actionType;
                ViewBag.SoldierName = soldierName ?? soldier.Name;
                return View(lastSoldierLeave);
            }
            catch (KeyNotFoundException)
            {
                _logger.LogWarning("Soldier with ID {SoldierId} not found.", soldierId);
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AttendanceConfirmation(SoldierLeave soldierLeave)
        {
            if (!ModelState.IsValid)
            {
                try
                {
                    var soldier = await _soldierService.GetSoldier(soldierLeave.SoldierId);
                    ViewBag.SoldierName = soldier.Name;
                }
                catch
                {
                    ViewBag.SoldierName = "";
                }

                ViewBag.ActionType = soldierLeave.Type;
                // FIX: تصحيح اسم الـ View
                return View("Attendance", soldierLeave);
            }

            try
            {
                var updatedLeave = await _soldierService.UpdateSoldierLeave(soldierLeave);

                var soldier = await _soldierService.GetSoldier(soldierLeave.SoldierId);
                soldier.CurrentIsLeave = false;
                await _soldierService.UpdateCurrentSoldier(soldier);

                _logger.LogInformation("Attendance recorded successfully for Soldier ID {SoldierId}", soldierLeave.SoldierId);

                return Content("<script>window.opener.location.reload(); window.close();</script>", "text/html");
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Soldier with ID {SoldierId} not found.", soldierLeave.SoldierId);
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error recording attendance for Soldier ID {SoldierId}", soldierLeave.SoldierId);
                ModelState.AddModelError("", "حدث خطأ أثناء تسجيل الحضور");

                try
                {
                    var soldier = await _soldierService.GetSoldier(soldierLeave.SoldierId);
                    ViewBag.SoldierName = soldier.Name;
                }
                catch
                {
                    ViewBag.SoldierName = "";
                }

                ViewBag.ActionType = soldierLeave.Type;
          
                return View("Attendance", soldierLeave);
            }
        }
    }
}