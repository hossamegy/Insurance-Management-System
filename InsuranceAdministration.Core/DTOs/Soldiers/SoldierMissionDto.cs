
using InsuranceAdministration.Core.Entities.SoldierEntities;

namespace InsuranceAdministration.Core.DTOs.Soldiers

{
    public class SoldierMissionDto
    {
        public ICollection<Soldier>? SoldiersNotRiver { get; set; }
        public ICollection<Soldier>? SoldiersRiver { get; set; }
        public SoldierDailyMealDto? DailyMeal { get; set; }
    }
}
