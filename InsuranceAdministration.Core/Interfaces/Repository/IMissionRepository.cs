
using InsuranceAdministration.Core.Entities.MissionEntities;

namespace InsuranceAdministration.Core.Interfaces.Repository
{
    public interface IMissionRepository
    {
        ValueTask<Mission> AddNewMission(Mission mission);
        ValueTask<Mission?> GetMission(int id);
        ValueTask<IEnumerable<Mission>> GetAllMissionsByActive(bool IsActive);
        ValueTask<IEnumerable<Mission>> GetAllMissionsByActiveAndType(bool IsActive, int MissionType);
        ValueTask<IEnumerable<Mission>> GetAllMissions();
        ValueTask<Mission?> DeleteMission(int id);
        ValueTask<Mission> UpdateCurrentMission(Mission mission);

        ValueTask<DailyMission> AddNewDailyMission(DateTime date, IEnumerable<int> missionIds);
        ValueTask<DailyMission?> GetDailyMission(int id);
        ValueTask<IEnumerable<Mission>> GetAllMissionsByType();
        ValueTask<IEnumerable<DailyMission>> GetAllDailyMissions();
        ValueTask<DailyMission?> DeletDailyMission(int id);
        ValueTask<DailyMission> UpdateCurrentDailyMission(DailyMission mission);



    }
}
