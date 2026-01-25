

namespace InsuranceAdministration.Core.Entities.Settings
{
    public class SettingsOptions
    {
        public int Id { get;  set; }

        ICollection<SoldierLeaveOptions> SoldierLeaveOptions = new List<SoldierLeaveOptions>();
        ICollection<AssignmentOptions> AssignmentOptions = new List<AssignmentOptions>();
        ICollection<EducationLevelOptions> EducationLevelOptions = new List<EducationLevelOptions>();
    }
}
