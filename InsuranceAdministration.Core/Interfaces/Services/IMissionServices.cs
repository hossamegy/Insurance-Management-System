using InsuranceAdministration.Core.Entities.MissionEntities;

namespace InsuranceAdministration.Core.Interfaces.Services
{
    public interface IMissionServices
    {
        ValueTask<Mission> AddNewMission(Mission mission);
        ValueTask<IEnumerable<Mission>> GetAllMissions();
        ValueTask<IEnumerable<Mission>> GetAllMissionsByActive(bool IsActive);
        ValueTask<IEnumerable<Mission>> GetAllMissionsByActiveAndType(bool IsActive, int MissionType);
        ValueTask<Mission> GetMission(int id);
        ValueTask<Mission> UpdateCurrentMission(Mission mission);
        ValueTask<Mission> DeleteMission(int id);

        ValueTask<DailyMission> AddNewDailyMission(DateTime date, IEnumerable<int> soldierMissionIds);
        ValueTask<DailyMission?> GetDailyMission(int id);
        ValueTask<IEnumerable<Mission>> GetAllMissionsByType();
        ValueTask<IEnumerable<DailyMission>> GetAllDailyMissions();
        ValueTask<DailyMission?> DeletDailyMission(int id);
        ValueTask<DailyMission> UpdateCurrentDailyMission(DailyMission mission);

    }
}
