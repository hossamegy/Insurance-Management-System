using InsuranceAdministration.Core.Entities.Settings;

namespace InsuranceAdministration.Core.Interfaces.Services
{
    public interface ISettingsServices
    {
        /* ================= Assignment ================= */
        ValueTask<AssignmentOptions> AddNewAssignmentOption(AssignmentOptions entity);
        ValueTask<IEnumerable<AssignmentOptions>> GetAllAssignmentOptions();
        ValueTask<AssignmentOptions> UpdateAssignmentOption(AssignmentOptions entity);
        ValueTask<AssignmentOptions> DeleteAssignmentOption(int id);

        /* ================= Education Level ================= */
        ValueTask<EducationLevelOptions> AddNewEducationLevelOption(EducationLevelOptions entity);
        ValueTask<IEnumerable<EducationLevelOptions>> GetAllEducationLevelOptions();
        ValueTask<EducationLevelOptions> UpdateEducationLevelOption(EducationLevelOptions entity);
        ValueTask<EducationLevelOptions> DeleteEducationLevelOption(int id);

        
    }
}
