using InsuranceAdministration.Core.Entities.SoldierEntities;

namespace InsuranceAdministration.Core.DTOs.Soldier
{
    public class SoldierLeavesDto
    {
        public int SoldierId { get; set; }
        public string SoldierName { get; set; }
        public string Assignment { get; set; }
        public ICollection<SoldierLeave>? Leaves { get; set; }
       
    }
}
