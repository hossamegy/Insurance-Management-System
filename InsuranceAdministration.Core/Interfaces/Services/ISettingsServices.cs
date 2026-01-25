using InsuranceAdministration.Core.Entities.Settings;

namespace InsuranceAdministration.Core.Interfaces.Services
{
    public interface ISettingsServices
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


        // Soldier Leave Options
        ValueTask<SoldierLeaveOptions> AddNewSoldierLeaveOptions(SoldierLeaveOptions entity);
        ValueTask<IEnumerable<SoldierLeaveOptions>> GetAllSoldierLeaveOptions();
        ValueTask<SoldierLeaveOptions> UpdateCurrentSoldierLeaveOptions(SoldierLeaveOptions entity);
        ValueTask<SoldierLeaveOptions> DeleteSoldierLeaveOptions(int id);
    }
}
