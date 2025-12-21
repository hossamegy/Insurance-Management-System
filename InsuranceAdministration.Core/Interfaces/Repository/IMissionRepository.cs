
namespace InsuranceAdministration.Core.Interfaces.Repository
{
    public interface IMissionRepository
    {
        ValueTask<Mission> AddNewMission(Mission mission);
        ValueTask<Mission?> GetMission(int id);
        ValueTask<IEnumerable<Mission>> GetAllMissionsByActive(bool IsActive);

        ValueTask<IEnumerable<Mission>> GetAllMissions();
        ValueTask<Mission?> DeleteMission(int id);
        ValueTask<Mission> UpdateCurrentMission(Mission mission);
    }
}
