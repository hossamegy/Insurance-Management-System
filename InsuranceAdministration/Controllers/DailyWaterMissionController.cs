using InsuranceAdministration.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InsuranceAdministration.Controllers
{
    public class DailyWaterMissionController : Controller
    {
        private readonly IMissionServices _missionServices;
        private readonly ISoldierServices _soldierServices;
        private readonly IPoliceManServices _policeManServices;
        public DailyWaterMissionController(
            IMissionServices missionServices,
            ISoldierServices soldierServices,
            IPoliceManServices policeManServices
            )
        {
            _missionServices = missionServices;
            _soldierServices = soldierServices;
            _policeManServices = policeManServices;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Add()
        {

            var soldiers = await _soldierServices.GetSoldierByAssignment("مسطح مائى");
            var policeMen = await _policeManServices.GetAllPoliceMan();

            ViewBag.Missions = await _missionServices.GetAllMissions();

            // Format: "Name|PhoneNumber"
            ViewBag.Soldiers = new SelectList(
                soldiers.Select(s => new
                {
                    Value = s.Name,
                    Text = $"{s.Name}|{s.PhoneNumber ?? ""}"
                }),
                "Value",
                "Text"
            );

            // Format: "Name|Rank|PhoneNumber"
            ViewBag.PoliceMen = new SelectList(
                policeMen.Select(p => new
                {
                    Value = p.Name,
                    Text = $"{p.Name}|{p.PhoneNumber ?? ""}"
                }),
                "Value",
                "Text"
            );

            return View();
        }
    }
}
