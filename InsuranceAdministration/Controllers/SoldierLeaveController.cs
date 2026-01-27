using AutoMapper;
using InsuranceAdministration.Core.DTOs.Soldiers;
using InsuranceAdministration.Core.Entities.SoldierEntities;
using InsuranceAdministration.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace InsuranceAdministration.Controllers
{
    public class SoldierLeaveController : Controller
    {
        private readonly ISoldierServices _soldierService;
        private readonly ISettingsServices _settingsOptionsService;
        private readonly ILogger<SoldierLeaveController> _logger;
        private readonly IMapper _mapper;
        public SoldierLeaveController(
            ISoldierServices soldierService,
            ISettingsServices settingsOptionsService,
            IMapper mapper,
            ILogger<SoldierLeaveController> logger)
        {
            _soldierService = soldierService;
            _settingsOptionsService = settingsOptionsService;
            _mapper = mapper;
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
            var soldierLeaveOptions = await _settingsOptionsService.GetAllSoldierLeaveOptions(); 
            ViewBag.soldierLeaveOptions = new SelectList(soldierLeaveOptions, "LeaveType", "LeaveType");

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

                return View("Leave", soldierLeave);
            }

            try
            {
                var addedLeave = await _soldierService.AddSoldierLeave(soldierLeave);

                var soldier = await _soldierService.GetSoldier(soldierLeave.SoldierId);

                if (soldierLeave.Type == "اجازة" || soldierLeave.Type == "هروب" || soldierLeave.Type == "غياب" || soldierLeave.Type == "مرضي" || soldierLeave.Type == "فرق تعليمية")
                    soldier.CurrentIsLeave = true;
                else
                {
                    soldier.CurrentIsLeave = false;

                }
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
                var soldierLeavesDto = _mapper.Map<SoldierLeavesDto>(soldier);

               

                return View(soldierLeavesDto);
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

                // FIX: تصحيح اسم الـ View
                return View("Attendance", soldierLeave);
            }

            try
            {
                soldierLeave.Type = "حضور";
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

                return View("Attendance", soldierLeave);
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteLeave(int soldierLeaveId, int soldierId)
        {
            try
            {
                var soldierLeave = await _soldierService.GetSoldierLeaveById(soldierLeaveId);

                if (soldierLeave == null)
                {
                    _logger.LogWarning("No leave found with ID {SoldierLeaveId}", soldierLeaveId);
                    TempData["ErrorMessage"] = "لا توجد إجازة بهذا المعرف";
                    return RedirectToAction("ShowSoldierLeaves", new { soldierId });
                }

                await _soldierService.DeleteSoldierLeave(soldierLeave);

                _logger.LogInformation("Leave with ID {SoldierLeaveId} deleted successfully", soldierLeaveId);

                TempData["SuccessMessage"] = "تم حذف السجل بنجاح";
                return RedirectToAction("ShowSoldierLeaves", new { soldierId });
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Leave with ID {SoldierLeaveId} not found", soldierLeaveId);
                TempData["ErrorMessage"] = "السجل غير موجود";
                return RedirectToAction("ShowSoldierLeaves", new { soldierId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting leave with ID {SoldierLeaveId}", soldierLeaveId);
                TempData["ErrorMessage"] = "حدث خطأ أثناء الحذف";
                return RedirectToAction("ShowSoldierLeaves", new { soldierId });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>UpdateLeave(int soldierLeaveId, int soldierId)
        {
            try
            {
                var soldierLeave = await _soldierService.GetSoldierLeaveById(soldierLeaveId);

                if (soldierLeave == null)
                {
                    _logger.LogWarning("No leave found with ID {SoldierLeaveId}", soldierLeaveId);
                    TempData["ErrorMessage"] = "لا توجد إجازة بهذا المعرف";
                    return RedirectToAction("ShowSoldierLeaves", new { soldierId });
                }

                await _soldierService.DeleteSoldierLeave(soldierLeave);

                _logger.LogInformation("Leave with ID {SoldierLeaveId} deleted successfully", soldierLeaveId);

                TempData["SuccessMessage"] = "تم حذف السجل بنجاح";
                return RedirectToAction("ShowSoldierLeaves", new { soldierId });
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Leave with ID {SoldierLeaveId} not found", soldierLeaveId);
                TempData["ErrorMessage"] = "السجل غير موجود";
                return RedirectToAction("ShowSoldierLeaves", new { soldierId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting leave with ID {SoldierLeaveId}", soldierLeaveId);
                TempData["ErrorMessage"] = "حدث خطأ أثناء الحذف";
                return RedirectToAction("ShowSoldierLeaves", new { soldierId });
            }
        }
    }
}