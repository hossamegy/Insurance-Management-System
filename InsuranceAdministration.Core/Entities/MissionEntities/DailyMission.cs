
using InsuranceAdministration.Core.Entities.SoldierEntities;

namespace InsuranceAdministration.Core.Entities.MissionEntities
{
    public class DailyMission
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        // أضف هذا
        public ICollection<SoldierMission> SoldierMissions { get; set; } = new List<SoldierMission>();
    }
}
