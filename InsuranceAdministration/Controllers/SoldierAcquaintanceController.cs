using InsuranceAdministration.Core.Entities.SoldierEntities;
using InsuranceAdministration.Core.Entities.SoldierEntities.Acquaintance;
using InsuranceAdministration.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceAdministration.Controllers
{
    public class SoldierAcquaintanceController : Controller
    {
        private readonly ISoldierServices _soldierService;
        private readonly ISettingsServices _settingsOptionsService;
        private readonly ILogger<SoldierController> _logger;
        public SoldierAcquaintanceController(
            ISoldierServices soldierService,
            ISettingsServices settingsOptionsService,
            ILogger<SoldierController> logger)
        {
            _soldierService = soldierService;
            _settingsOptionsService = settingsOptionsService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var soldiers = (await _soldierService.GetPaginatedSoldiersByActive(pageNumber, pageSize + 1, true)).ToList();
            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            ViewBag.HasNext = soldiers.Count > pageSize;
            return View(soldiers.Take(pageSize));
        }

        [HttpGet]
        public async Task<IActionResult> Print(int id)
        {
            try
            {
                var soldier = await _soldierService.GetSoldier(id);
                if (soldier == null)
                {
                    TempData["Error"] = "لم يتم العثور على المجند";
                    return RedirectToAction(nameof(AcquaintanceDocument));
                }

                ViewBag.SoldierName = soldier.Name;
                return View(soldier);
            }
            catch (KeyNotFoundException)
            {
                TempData["Error"] = "لم يتم العثور على المجند";
                return RedirectToAction(nameof(AcquaintanceDocument));
            }
        }
        [HttpGet]
        public async Task<IActionResult> Add(int id)
        {
            var soldier = await _soldierService.GetSoldier(id);
            if (soldier == null) return NotFound();

            var model = new AcquaintanceDocument
            {
                SoldierId = soldier.Id,
                BaseFamily = new List<BaseFamily>(),
                Family = new List<Family>()
            };

            ViewBag.SoldierName = soldier.Name;
            return View(model);
        }

        // ===== CRITICAL FIX: Changed parameter name from "model" to match form binding =====
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAcquaintanceDocument(
            [FromForm] int SoldierId,
            [FromForm] List<BaseFamily> BaseFamily,
            [FromForm] List<Family> Family)
        {
            _logger.LogInformation(
                "Controller POST: SoldierId={SoldierId}, BaseFamily={BaseCount}, Family={FamilyCount}",
                SoldierId,
                BaseFamily?.Count ?? 0,
                Family?.Count ?? 0
            );

            // Create the model manually
            var model = new AcquaintanceDocument
            {
                SoldierId = SoldierId,
                BaseFamily = BaseFamily ?? new List<BaseFamily>(),
                Family = Family ?? new List<Family>()
            };

            // Validate manually since we're not using model binding directly
            if (SoldierId <= 0)
            {
                _logger.LogWarning("Controller: Invalid SoldierId");
                TempData["Error"] = "معرف المجند غير صحيح";
                return RedirectToAction(nameof(AcquaintanceDocument));
            }

            try
            {
                // Clean empty entries
                if (model.BaseFamily != null)
                {
                    model.BaseFamily = model.BaseFamily
                        .Where(f => !string.IsNullOrWhiteSpace(f.Name) ||
                                   !string.IsNullOrWhiteSpace(f.NationalId))
                        .ToList();
                }

                if (model.Family != null)
                {
                    model.Family = model.Family
                        .Where(f => !string.IsNullOrWhiteSpace(f.FullName) ||
                                   !string.IsNullOrWhiteSpace(f.NationalId))
                        .ToList();
                }

                _logger.LogInformation(
                    "Controller: After cleanup - BaseFamily={BaseCount}, Family={FamilyCount}",
                    model.BaseFamily?.Count ?? 0,
                    model.Family?.Count ?? 0
                );

                // Save the document
                var result = await _soldierService.AddAcquaintanceDocument(model.SoldierId, model);

                _logger.LogInformation("Controller: Successfully saved document ID={DocumentId}", result.Id);

                TempData["Success"] = "تم إضافة وثيقة التعارف بنجاح";
                return RedirectToAction(nameof(Show), new { id = model.SoldierId });
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex, "Controller: Soldier not found");
                TempData["Error"] = "لم يتم العثور على المجند";
                return RedirectToAction(nameof(AcquaintanceDocument));
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Controller: Document already exists");
                TempData["Error"] = ex.Message;
                return RedirectToAction(nameof(Show), new { id = model.SoldierId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Controller: Error saving document");
                TempData["Error"] = "حدث خطأ أثناء حفظ البيانات: " + ex.Message;

                try
                {
                    var soldier = await _soldierService.GetSoldier(model.SoldierId);
                    ViewBag.SoldierName = soldier?.Name;
                }
                catch
                {
                    ViewBag.SoldierName = "غير معروف";
                }
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Show(int id)
        {
            try
           {
                var soldier = await _soldierService.GetSoldier(id);
                if (soldier == null)
                 {
                    TempData["Error"] = "لم يتم العثور على المجند";
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.SoldierName = soldier.Name;
                return View(soldier);
            }
            catch (KeyNotFoundException)
            {
                TempData["Error"] = "لم يتم العثور على المجند";
                return RedirectToAction(nameof(Index));
            }
        }


        // GET: Edit Acquaintance Document
        [HttpGet]
        public async Task<IActionResult> Edit(int soldierId)
        {
            try
            {
                _logger.LogInformation("Controller: Loading edit form for soldier {SoldierId}", soldierId);

                var soldier = await _soldierService.GetSoldier(soldierId);
                if (soldier == null)
                {
                    TempData["Error"] = "لم يتم العثور على المجند";
                    return RedirectToAction(nameof(AcquaintanceDocument));
                }

                if (soldier.AcquaintanceDocument == null)
                {
                    TempData["Error"] = "لا توجد وثيقة تعارف لهذا المجند";
                    return RedirectToAction(nameof(Show), new { id = soldierId });
                }

                ViewBag.SoldierName = soldier.Name;
                return View(soldier.AcquaintanceDocument);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Controller: Error loading edit form");
                TempData["Error"] = "حدث خطأ أثناء تحميل البيانات";
                return RedirectToAction(nameof(AcquaintanceDocument));
            }
        }

        // POST: Update Acquaintance Document
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAcquaintanceDocument(

            [FromForm] int Id,
            [FromForm] int SoldierId,
            [FromForm] List<BaseFamily> BaseFamily,
            [FromForm] List<Family> Family)
        {
            _logger.LogInformation(
                "Controller: Updating document {DocumentId} for soldier {SoldierId}",
                Id, SoldierId
            );

            try
            {
                var model = new AcquaintanceDocument
                {
                    Id = Id,
                    SoldierId = SoldierId,
                    BaseFamily = BaseFamily ?? new List<BaseFamily>(),
                    Family = Family ?? new List<Family>()
                };

                // Clean empty entries
                if (model.BaseFamily != null)
                {
                    model.BaseFamily = model.BaseFamily
                        .Where(f => !string.IsNullOrWhiteSpace(f.Name) ||
                                   !string.IsNullOrWhiteSpace(f.NationalId))
                        .ToList();
                }

                if (model.Family != null)
                {
                    model.Family = model.Family
                        .Where(f => !string.IsNullOrWhiteSpace(f.FullName) ||
                                   !string.IsNullOrWhiteSpace(f.NationalId))
                        .ToList();
                }

                _logger.LogInformation(
                    "Controller: After cleanup - BaseFamily={BaseCount}, Family={FamilyCount}",
                    model.BaseFamily?.Count ?? 0,
                    model.Family?.Count ?? 0
                );

                // Update the document
                await _soldierService.UpdateAcquaintanceDocument(model);

                TempData["Success"] = "تم تحديث وثيقة التعارف بنجاح";
                return RedirectToAction(nameof(Show), new { id = SoldierId });
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex, "Controller: Document not found");
                TempData["Error"] = "لم يتم العثور على وثيقة التعارف";
                return RedirectToAction(nameof(AcquaintanceDocument));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Controller: Error updating document");
                TempData["Error"] = "حدث خطأ أثناء تحديث البيانات: " + ex.Message;

                try
                {
                    var soldier = await _soldierService.GetSoldier(SoldierId);
                    ViewBag.SoldierName = soldier?.Name;

                    var document = new AcquaintanceDocument
                    {
                        Id = Id,
                        SoldierId = SoldierId,
                        BaseFamily = BaseFamily,
                        Family = Family
                    };

                    return View(document);
                }
                catch
                {
                    return RedirectToAction(nameof(AcquaintanceDocument));
                }
            }
        }
    }
}
