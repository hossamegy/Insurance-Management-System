// ============================================
// SoldierController.cs - FINAL CORRECTED VERSION
// ============================================
using AutoMapper;
using InsuranceAdministration.Core.DTOs.Soldiers;
using InsuranceAdministration.Core.Entities.SoldierEntities;

using InsuranceAdministration.Core.Interfaces.Services;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InsuranceAdministration.Controllers
{
    public class SoldierController : Controller
    {
        private readonly ISoldierServices _soldierService;
        private readonly IMissionServices _missionService;
        private readonly ISettingsServices _settingsOptionsService;
        private readonly IMapper _mapper;
        private readonly ILogger<SoldierController> _logger;

        public SoldierController(
            ISoldierServices soldierService,
            IMissionServices missionService,
            ISettingsServices settingsOptionsService,
            ILogger<SoldierController> logger)
        {
            _soldierService = soldierService;
            _missionService = missionService;
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

        [HttpGet]
        public async Task<IActionResult> Ornik114(int pageNumber = 1, int pageSize = 10)
        {
            var soldiers = (await _soldierService.GetPaginatedSoldiersByActive(pageNumber, pageSize + 1, true)).ToList();
            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            ViewBag.HasNext = soldiers.Count > pageSize;
            return View(soldiers.Take(pageSize));
        }
        [HttpGet]
        public async Task<IActionResult> PrintOrnik114(int id)
        {
            var soldier = await _soldierService.GetSoldier(id);

            ViewBag.DepartmentName = await _settingsOptionsService.GetMainSettingsByDepartmentName();
            ViewBag.DepartmentDirectorName = await _settingsOptionsService.GetMainSettingsByDepartmentDirectorName();
            ViewBag.ConscriptsAffairsPoliceManName = await _settingsOptionsService.GetMainSettingsByConscriptsAffairsPoliceManName();
            ViewBag.ConscriptsAffairsOfficerName = await _settingsOptionsService.GetMainSettingsByConscriptsAffairsOfficerName();
            return View(soldier);
        }

        [HttpGet]
        public async Task<IActionResult> DailyMeal()
            {
                // Get all counts from database
                var soldierCounts = await _soldierService.GetSoldiersCountsIsActive();
                var soldiersLeaveCounts = await _soldierService.GetSoldiersLeaveCounts();
                var soldiersLeaveCountsMamoria = await _soldierService.GetSoldiersLeaveCountsByType("مأمورية");
                var soldiersLeaveCountsIllness = await _soldierService.GetSoldiersLeaveCountsByType("مرضي");
                var soldiersLeaveCountsTraining = await _soldierService.GetSoldiersLeaveCountsByType("فرق تعليمية");
                var soldiersLeaveCountsEscape = await _soldierService.GetSoldiersLeaveCountsByType("هروب");
                var soldiersLeaveCountsAbsence = await _soldierService.GetSoldiersLeaveCountsByType("غياب");
                var soldiersLeaveCountsPrison = await _soldierService.GetSoldiersLeaveCountsByType("سجن خارجي");

                // Get department meal options
                var departments = await _settingsOptionsService.GetAllDailyMealOptions();

                // Create ViewModel
                var viewModel = new SoldierDailyMealDto
                {
                    SoldierCounts = soldierCounts,
                    SoldiersLeaveCounts = soldiersLeaveCounts,
                    SoldiersLeaveCountsMamoria = soldiersLeaveCountsMamoria,
                    SoldiersLeaveCountsIllness = soldiersLeaveCountsIllness,
                    SoldiersLeaveCountsTraining = soldiersLeaveCountsTraining,
                    SoldiersLeaveCountsEscape = soldiersLeaveCountsEscape,
                    SoldiersLeaveCountsAbsence = soldiersLeaveCountsAbsence,
                    SoldiersLeaveCountsPrison = soldiersLeaveCountsPrison,
                    SoldiersLeaveCountsReplacement = 0,
                    SoldiersLeaveCountsStrikeOff = 0,
                    ReportDate = DateTime.Now.AddDays(1),
                    Notes = "",
                    DepartmentMeals = departments.Select(d => new DepartmentMealCount
                    {
                        DepartmentId = d.Id,
                        DepartmentName = d.Name,
                        Count = 0
                    }).ToList()
                };

                // Calculate totals
                viewModel.CalculateTotals();

                // Set ViewBag data
                ViewBag.DepartmentName = await _settingsOptionsService.GetMainSettingsByDepartmentName();
                ViewBag.DepartmentDirectorName = await _settingsOptionsService.GetMainSettingsByDepartmentDirectorName();
                ViewBag.allDepartmentsNeedMeal = departments;

                return View(viewModel);
         }

        [HttpGet]
        public async Task<IActionResult> DailyMission()
        {
            // Get all counts from database
            var soldierCounts = await _soldierService.GetSoldiersCountsIsActive();
            var soldiersLeaveCounts = await _soldierService.GetSoldiersLeaveCounts();
            var soldiersLeaveCountsMamoria = await _soldierService.GetSoldiersLeaveCountsByType("مأمورية");
            var soldiersLeaveCountsIllness = await _soldierService.GetSoldiersLeaveCountsByType("مرضي");
            var soldiersLeaveCountsTraining = await _soldierService.GetSoldiersLeaveCountsByType("فرق تعليمية");
            var soldiersLeaveCountsEscape = await _soldierService.GetSoldiersLeaveCountsByType("هروب");
            var soldiersLeaveCountsAbsence = await _soldierService.GetSoldiersLeaveCountsByType("غياب");
            var soldiersLeaveCountsPrison = await _soldierService.GetSoldiersLeaveCountsByType("سجن خارجي");

            // Get department meal options
            var departments = await _settingsOptionsService.GetAllDailyMealOptions();

            // Create ViewModel
            var soldierDailyMeal = new SoldierDailyMealDto
            {
                SoldierCounts = soldierCounts,
                SoldiersLeaveCounts = soldiersLeaveCounts,
                SoldiersLeaveCountsMamoria = soldiersLeaveCountsMamoria,
                SoldiersLeaveCountsIllness = soldiersLeaveCountsIllness,
                SoldiersLeaveCountsTraining = soldiersLeaveCountsTraining,
                SoldiersLeaveCountsEscape = soldiersLeaveCountsEscape,
                SoldiersLeaveCountsAbsence = soldiersLeaveCountsAbsence,
                SoldiersLeaveCountsPrison = soldiersLeaveCountsPrison,
                SoldiersLeaveCountsReplacement = 0,
                SoldiersLeaveCountsStrikeOff = 0,
                ReportDate = DateTime.Now.AddDays(1),
                Notes = "",
                DepartmentMeals = departments.Select(d => new DepartmentMealCount
                {
                    DepartmentId = d.Id,
                    DepartmentName = d.Name,
                    Count = 0
                }).ToList()
            };

            // Calculate totals
            soldierDailyMeal.CalculateTotals();

            // Get missions
            var dailyMissionRiver = await _missionService.GetAllMissionsByActiveAndType(true, 0);
            var dailyMissionDepartment = await _missionService.GetAllMissionsByActiveAndType(true, 1);

            // Get soldiers
            var allAttendanceSoldierNotRiver = await _soldierService.GetAllSoldierAttendenceNotRiver();
            var allAttendanceSoldierRiver = await _soldierService.GetAllSoldierAttendenceRiver();

            // Create ViewModel
            var soldierMission = new SoldierMissionDto
            {
                SoldiersNotRiver = allAttendanceSoldierNotRiver.ToList(),
                SoldiersRiver = allAttendanceSoldierRiver.ToList(),
                DailyMeal = soldierDailyMeal
            };

            // ViewBag data (set once, not twice)
            ViewBag.DepartmentName = await _settingsOptionsService.GetMainSettingsByDepartmentName();
            ViewBag.DepartmentDirectorName = await _settingsOptionsService.GetMainSettingsByDepartmentDirectorName();
            ViewBag.DailyMissionDepartment = new SelectList(dailyMissionDepartment, "Id", "Name");
            ViewBag.dailyMissionRiver = new SelectList(dailyMissionRiver, "Id", "Name");
            ViewBag.allDepartmentsNeedMeal = departments;

            return View(soldierMission);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveDailyMission(IFormCollection form)
        {
            try
            {
                _logger.LogInformation("Starting to save daily missions");

                DateTime assignedDate = DateTime.Now.AddDays(1).Date;

                if (form.ContainsKey("MissionDate") && !string.IsNullOrEmpty(form["MissionDate"]))
                {
                    var dateString = form["MissionDate"].ToString();
                    if (DateTime.TryParse(dateString, out DateTime parsedDate))
                    {
                        assignedDate = parsedDate.Date;
                        _logger.LogInformation($"Successfully parsed date: {assignedDate:yyyy-MM-dd}");
                    }
                }

                var soldierMissions = new List<SoldierMission>();

                var missionKeys = form.Keys.Where(k => k.StartsWith("Mission_")).ToList();

                foreach (var key in missionKeys)
                {
                    var soldierIdStr = key.Replace("Mission_", "");
                    if (!int.TryParse(soldierIdStr, out int soldierId))
                        continue;

                    var missionIdStr = form[key].ToString();
                    if (string.IsNullOrEmpty(missionIdStr) || !int.TryParse(missionIdStr, out int missionId))
                        continue;

                    var notesKey = $"Notes_{soldierId}";
                    var notes = form[notesKey].ToString();

                    var soldierMission = new SoldierMission
                    {
                        SoldierId = soldierId,
                        MissionId = missionId,
                        Notes = string.IsNullOrWhiteSpace(notes) ? null : notes,
                        AssignedAt = assignedDate
                    };

                    soldierMissions.Add(soldierMission);
                }

                if (soldierMissions.Any())
                {
                    // Save soldier missions
                    var savedSoldierMissions = await _soldierService.AddDailyMissionsToSoldiers(soldierMissions);
                    _logger.LogInformation($"Saved {savedSoldierMissions.Count()} soldier missions");

                    // Get IDs of saved soldier missions
                    var soldierMissionIds = savedSoldierMissions.Select(sm => sm.Id).ToList();

                    // Create/Update DailyMission
                    var dailyMission = await _missionService.AddNewDailyMission(assignedDate, soldierMissionIds);

                    var uniqueMissions = savedSoldierMissions.Select(sm => sm.MissionId).Distinct().Count();

                    TempData["SuccessMessage"] = $"تم حفظ {soldierMissions.Count} تعيين بنجاح ({uniqueMissions} مهمة مختلفة) بتاريخ {assignedDate:dd/MM/yyyy}";
                }
                else
                {
                    TempData["WarningMessage"] = "لم يتم اختيار أي مهام للحفظ";
                }

                return RedirectToAction(nameof(DailyMission));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving daily missions");
                TempData["ErrorMessage"] = "حدث خطأ أثناء حفظ البيانات";
                return RedirectToAction(nameof(DailyMission));
            }
        }
    }
}
