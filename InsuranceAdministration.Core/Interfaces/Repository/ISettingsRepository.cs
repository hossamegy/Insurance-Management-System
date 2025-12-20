using InsuranceAdministration.Core.Entities.Settings;

namespace InsuranceAdministration.Core.Interfaces.Repository
{
    public interface ISettingsRepository
    {
        // Assignment
        ValueTask<AssignmentOptions> AddNewAssignmentOptions(AssignmentOptions entity);
        ValueTask<IEnumerable<AssignmentOptions>> GetAllAssignmentOptions();
        ValueTask<AssignmentOptions> UpdateCurrentAssignmentOptions(AssignmentOptions entity);
        ValueTask<AssignmentOptions> DeleteAssignmentOptions(int id);

        // Education Level
        ValueTask<EducationLevelOptions> AddNewEducationLevelOptions(EducationLevelOptions entity);
        ValueTask<IEnumerable<EducationLevelOptions>> GetAllEducationLevelOptions();
        ValueTask<EducationLevelOptions> UpdateCurrentEducationLevelOptions(EducationLevelOptions entity);
        ValueTask<EducationLevelOptions> DeleteEducationLevelOptions(int id);

   
    }
}
