

namespace InsuranceAdministration.Core.Entities.Settings
{
    public class SettingsOptions
    {
        public int Id { get;  set; }

        ICollection<AssignmentOptions> AssignmentOptions = new List<AssignmentOptions>();
        ICollection<EducationLevelOptions> EducationLevelOptions = new List<EducationLevelOptions>();
    }
}
