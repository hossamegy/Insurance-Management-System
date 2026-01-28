using InsuranceAdministration.Core.Entities.Settings;

namespace InsuranceAdministration.Core.Interfaces.Repository
{
    public interface ISettingsRepository
    {

        /* ================= Main ================= */
        ValueTask<MainSettings> AddNewMainSettings(MainSettings entity);
        ValueTask<IEnumerable<MainSettings>> GetAllMainSettings();
        ValueTask<MainSettings> GetMainSettings(int id);

        ValueTask<MainSettings> UpdateMainSettings(MainSettings entity);
        ValueTask<MainSettings> DeleteMainSettings(int id);
        ValueTask<string> GetMainSettingsByDepartmentName();
        ValueTask<string> GetMainSettingsByDepartmentDirectorName();
        ValueTask<string> GetMainSettingsByConscriptsAffairsPoliceManName();
        ValueTask<string> GetMainSettingsByConscriptsAffairsOfficerName();

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


        // Soldier Leave Options
        ValueTask<SoldierLeaveOptions> AddNewSoldierLeaveOptions(SoldierLeaveOptions entity);
        ValueTask<IEnumerable<SoldierLeaveOptions>> GetAllSoldierLeaveOptions();
        ValueTask<SoldierLeaveOptions> UpdateCurrentSoldierLeaveOptions(SoldierLeaveOptions entity);
        ValueTask<SoldierLeaveOptions> DeleteSoldierLeaveOptions(int id);

        // Daily Meal Options
        ValueTask<DailyMealOptions> AddNewDailyMealOptions(DailyMealOptions entity);
        ValueTask<IEnumerable<DailyMealOptions>> GetAllDailyMealOptions();
        ValueTask<DailyMealOptions> DeleteDailyMealOptions(int id) ;
    }
}
